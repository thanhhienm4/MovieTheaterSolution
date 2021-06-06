﻿using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class FilmController : Controller
    {
        private readonly FilmApiClient _filmApiClient;
        private readonly ScreeningApiClient _screeningApiClient;

        public FilmController(FilmApiClient filmApiClient, ScreeningApiClient screeningApiClient)
        {
            _filmApiClient = filmApiClient;
            _screeningApiClient = screeningApiClient;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = (await _filmApiClient.GetFilmVMDByIdAsync(id)).ResultObj;
            return View(result);
        }

        public async Task<IActionResult> ScreeningInWeek(int id)
        {
            var res = (await _screeningApiClient.GetListCreeningOfFilmInWeekAsync(id)).ResultObj;
            return View(res);
        }
    }
}