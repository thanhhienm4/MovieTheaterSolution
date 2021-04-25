using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieTheater.Api;
using MovieTheater.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScreeningApiClient _screeningApiClient;
        public HomeController(ScreeningApiClient screeningApiClient)
        {
            _screeningApiClient = screeningApiClient;
        }
        public async Task<IActionResult> Index(DateTime? date)
        {
            var listFlimScreening = (await _screeningApiClient.GetFilmScreeningIndateAsync(date)).ResultObj;

            return View(listFlimScreening);
        }





    }
}
