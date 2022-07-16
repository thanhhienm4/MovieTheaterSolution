﻿using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScreeningApiClient _screeningApiClient;
        private readonly MovieApiClient _filmApiClient;

        public HomeController(ScreeningApiClient screeningApiClient, MovieApiClient filmApiClient)
        {
            _screeningApiClient = screeningApiClient;
            _filmApiClient = filmApiClient;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.SuccessMsg = TempData["Result"];
            ViewData["PlayingFilms"] = (await _filmApiClient.GetAllPlayingFilmAsync()).ResultObj;
            ViewData["UpcomingFilms"] = (await _filmApiClient.GetAllUpcomingFilmAsync()).ResultObj;
            return View();
        }
    }
}