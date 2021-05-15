using AspNetCore.Reporting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Common.ChartTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatiticController : Controller
    {
        private readonly StatiticApiClient _statiticApiClient;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StatiticController(StatiticApiClient statiticApiClient , IWebHostEnvironment webHostEnvironment)
        {
            _statiticApiClient = statiticApiClient;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        [HttpGet]
        public async Task<IActionResult> Index(CalRevenueRequest request)
        {

            var topGroosingFilm = (await _statiticApiClient.GetTopGrossingFilmAsync(request)).ResultObj;
            var groosing =  (await _statiticApiClient.GetGroosingTypeAsync(request)).ResultObj;
            ViewData["TopGroosingFilm"] = topGroosingFilm;
            ViewData["Groosing"] = groosing;

            
            return View(request);
        }
        private async Task<DataTable> GetDataReport()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Film");
            dt.Columns.Add("Grossing");
            dt.Columns.Add("Proportion");

           
            var request = new CalRevenueRequest();
            request.StartDate = DateTime.Now.AddMonths(-1);
            request.EndDate = DateTime.Now;
            var topGroosingFilm = (await _statiticApiClient.GetTopGrossingFilmAsync(request)).ResultObj;
            DataRow row;
            for(int i=0;i< topGroosingFilm.Lables.Count;i++)
            {
                row = dt.NewRow();
                row["Film"] = topGroosingFilm.Lables[i];
                row["Grossing"] = topGroosingFilm.DataRows[1][i];
                dt.Rows.Add(row);
            }
            return dt;
        }
        public async Task<IActionResult> Print()
        {
            string minetype = "";
            int extention = 1;
            DataTable dt = await GetDataReport();
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\RprtFilm.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("TopGrossingFilm", dt);
            var result = localReport.Execute(RenderType.Pdf, extention, parameters, minetype);
            return File(result.MainStream, "application/pdf");
        }

        [HttpGet]
        public async Task<ChartData> GetTopGrossingFilm(CalRevenueRequest request)
        {
            request = new CalRevenueRequest();
            request.StartDate = DateTime.Now.AddMonths(-1);
            request.EndDate = DateTime.Now;
            var result = (await _statiticApiClient.GetTopGrossingFilmAsync(request)).ResultObj;
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
        
            var result = (await _statiticApiClient.GetRevenueAsync(request)).ResultObj;
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

            var result = (await _statiticApiClient.GetRevenueAsync(request)).ResultObj;
            return result;
        }

        [HttpGet]
        public async Task<ChartData> GetGroosingType(DateTime date)
        {
            date = DateTime.Now;
            var request = new CalRevenueRequest()
            {
                StartDate = new DateTime(date.Year, 1, 1),
                EndDate = new DateTime(date.AddYears(1).Year, 1, 1).AddDays(-1)
            };
            var result = (await _statiticApiClient.GetGroosingTypeAsync(request)).ResultObj;
            return result;
        }
    }
}
