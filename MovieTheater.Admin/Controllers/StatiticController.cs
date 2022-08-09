using AspNetCore.Reporting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Common.ChartTable;
using MovieTheater.Models.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatiticController : Controller
    {
        private readonly StatiticApiClient _statisticApiClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StatiticController(StatiticApiClient statisticApiClient, IWebHostEnvironment webHostEnvironment)
        {
            _statisticApiClient = statisticApiClient;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        [HttpGet]
        public async Task<IActionResult> Index(CalRevenueRequest? request)
        {
            if (request.StartDate == DateTime.MinValue && request.EndDate == DateTime.MinValue)
            {
                request.EndDate = DateTime.Now;
            }

            var result = (await _statisticApiClient.GetTopRevenueFilmAsync(request));
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            var topRevenueFilm = result.ResultObj;

            var Revenue = (await _statisticApiClient.GetRevenueTypeAsync(request)).ResultObj;
            ViewData["TopRevenueFilm"] = topRevenueFilm;
            ViewData["Revenue"] = Revenue;

            return View(request);
        }

        private async Task<DataTable> GetDataReport(CalRevenueRequest request)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Film");
            dt.Columns.Add("Revenue");
            dt.Columns.Add("Proportion");


            var topRevenueFilm = (await _statisticApiClient.GetTopRevenueFilmAsync(request)).ResultObj;
            DataRow row;
            for (int i = 0; i < topRevenueFilm.Lables.Count; i++)
            {
                row = dt.NewRow();
                row["Film"] = topRevenueFilm.Lables[i];
                row["Revenue"] = topRevenueFilm.DataRows[1][i];
                dt.Rows.Add(row);
            }

            return dt;
        }

        public async Task<string> FilmRevenueReport(FilmReportRequest request)
        {
            string mimetype = "";
            int extenstion = 1;
            CalRevenueRequest calRevenueRequest = new CalRevenueRequest()
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            DataTable dt = await GetDataReport(calRevenueRequest);
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\RprtFilm.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm",
                $"Thống kê kết quả từ ngày {request.StartDate.ToString("dd/MM/yyyy")} đến ngày {request.EndDate.ToString("dd/MM/yyyy")}");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("TopRevenueFilm", dt);
            ReportResult report;
            FileContentResult file;

            switch (request.RenderType)
            {
                case RenderType.Pdf:
                {
                    report = localReport.Execute(RenderType.Pdf, extenstion, parameters, mimetype);
                    file = File(report.MainStream, "application/pdf");
                    break;
                }
                    ;
                case RenderType.Excel:
                {
                    report = localReport.Execute(RenderType.Excel, extenstion, parameters, mimetype);
                    file = File(report.MainStream, "application/excel");
                    break;
                }
                default:
                {
                    report = localReport.Execute(RenderType.Pdf, extenstion, parameters, mimetype);
                    file = File(report.MainStream, "application/pdf");
                    break;
                }
            }

            var data = Convert.ToBase64String(file.FileContents);
            return data;
        }

        [HttpGet]
        public async Task<ChartData> GetTopRevenueFilm(CalRevenueRequest? request)
        {
            if (request.StartDate == DateTime.MinValue && request.EndDate == DateTime.MinValue)
            {
                request = new CalRevenueRequest();
                request.StartDate = DateTime.Now.AddMonths(-1);
                request.EndDate = DateTime.Now;
            }

            var result = (await _statisticApiClient.GetTopRevenueFilmAsync(request)).ResultObj;
            return result;
        }

        [HttpGet]
        public async Task<long> GetRevenueByMonth(DateTime date)
        {
            date = DateTime.Now;
            var request = new CalRevenueRequest()
            {
                StartDate = new DateTime(date.Year, date.Month, 1),
                EndDate = new DateTime(date.AddMonths(1).Year, date.AddMonths(1).Month, 1).AddDays(-1)
            };

            var result = (await _statisticApiClient.GetRevenueAsync(request)).ResultObj;
            return result;
        }

        [HttpGet]
        public async Task<long> GetRevenueByYear(DateTime date)
        {
            date = DateTime.Now;
            var request = new CalRevenueRequest()
            {
                StartDate = new DateTime(date.Year, 1, 1),
                EndDate = new DateTime(date.AddYears(1).Year, 1, 1).AddDays(-1)
            };

            var result = (await _statisticApiClient.GetRevenueAsync(request)).ResultObj;
            return result;
        }

        [HttpGet]
        public async Task<ChartData> GetRevenueType(DateTime date)
        {
            date = DateTime.Now;
            var request = new CalRevenueRequest()
            {
                StartDate = new DateTime(date.Year, 1, 1),
                EndDate = new DateTime(date.AddYears(1).Year, 1, 1).AddDays(-1)
            };
            var result = (await _statisticApiClient.GetRevenueTypeAsync(request)).ResultObj;
            return result;
        }
    }
}