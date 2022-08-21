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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MovieTheater.Admin.Helpers;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatiticController : Controller
    {
        private readonly StatisticApiClient _statisticApiClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StatiticController(StatisticApiClient statisticApiClient, IWebHostEnvironment webHostEnvironment)
        {
            _statisticApiClient = statisticApiClient;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        [HttpGet]
        public async Task<IActionResult> Index(CalRevenueRequest? request)
        {
            if (request != null && request.StartDate == DateTime.MinValue && request.EndDate == DateTime.MinValue)
            {
                request.EndDate = DateTime.Now;
                request.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

            var result = (await _statisticApiClient.GetTopRevenueFilmAsync(request));
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            var topRevenueFilm = result.ResultObj;

            var revenue = (await _statisticApiClient.GetRevenueTypeAsync(request)).ResultObj;
            ViewData["TopRevenueFilm"] = topRevenueFilm;
            ViewData["Revenue"] = revenue;
            ViewData["RevenueDataInWeek"] = (await _statisticApiClient.GetRevenueInWeek(request.StartDate, request.EndDate)).ResultObj;

            return View(request);
        }

        public IActionResult DownloadMovieRevenue(DateTime fromDate, DateTime toDate)
        {
            var data = _statisticApiClient.GetTopRevenueFilmAsync(new CalRevenueRequest()
            {
                EndDate = toDate,
                StartDate = fromDate
            }).Result;
            var templateFileInfo = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "Template", "MovieRevenue.xlsx"));
            var stream = Utils.GetExcelMovieRevenue(fromDate, toDate, data.ResultObj, templateFileInfo);
            string timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToUpper().Replace(':', '_').Replace('.', '_').Replace(' ', '_').Trim();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MovieRevenue" + timestamp + ".xlsx");
        }

        [HttpGet]
        public async Task<ChartData> GetTopRevenueFilm(CalRevenueRequest request)
        {
            if (request.StartDate == DateTime.MinValue && request.EndDate == DateTime.MinValue)
            {
                request = new CalRevenueRequest
                {
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month,1),
                    EndDate = DateTime.Now
                };
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

        public IActionResult DownloadRawData(DateTime fromDate, DateTime toDate)
        {
            var data =  _statisticApiClient.GetRawData(fromDate, toDate).Result;
            var templateFileInfo = new FileInfo(Path.Combine(_webHostEnvironment.WebRootPath, "Template", "RawData.xlsx"));
            var stream = Utils.GetExcelRawData(fromDate, toDate, data.ResultObj.ToList(), templateFileInfo);
            string timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToUpper().Replace(':', '_').Replace('.', '_').Replace(' ', '_').Trim();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RevenueReport" + timestamp + ".xlsx");
        }
    }
}