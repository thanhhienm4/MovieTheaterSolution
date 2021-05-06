using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Common.ChartTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    public class StatiticController : Controller
    {
        private readonly StatiticApiClient _statiticApiClient;
        public StatiticController(StatiticApiClient statiticApiClient)
        {
            _statiticApiClient = statiticApiClient;
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
