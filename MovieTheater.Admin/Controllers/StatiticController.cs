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
        public async Task<ChartData> GetTopGossingFilm(TopGrossingFilmRequest request)
        {
            request = new TopGrossingFilmRequest();
            request.StartDate = DateTime.Now.AddMonths(-1);
            request.EndDate = DateTime.Now;
            var result = (await _statiticApiClient.GetTopGrossingFilmAsync(request)).ResultObj;
            return result;
        }
    }
}
