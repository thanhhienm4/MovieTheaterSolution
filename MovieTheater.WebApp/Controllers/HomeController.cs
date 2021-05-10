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
        private readonly FilmApiClient _filmApiClient;
        public HomeController(ScreeningApiClient screeningApiClient, FilmApiClient filmApiClient)
        {
            _screeningApiClient = screeningApiClient;
            _filmApiClient = filmApiClient;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["PlayingFilms"] = (await _filmApiClient.GetAllPlayingFilmAsync()).ResultObj;
            ViewData["UpcomingFilms"] = (await _filmApiClient.GetAllUpcomingFilmAsync()).ResultObj;
            return View();
        }





    }
}
