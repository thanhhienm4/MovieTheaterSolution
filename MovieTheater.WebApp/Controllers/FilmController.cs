using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IActionResult> PlayingFilm()
        {
            var playingFilms = (await _filmApiClient.GetAllPlayingFilmAsync()).ResultObj;
            return View(playingFilms);
        }

        public async Task<IActionResult> UpcomingFilm()
        {
            var upcomingFilms = (await _filmApiClient.GetAllUpcomingFilmAsync()).ResultObj;
            return View(upcomingFilms);
        }

        public async Task<IActionResult> ScreeningInWeek(int id)
        {
            var res =(await _screeningApiClient.GetListCreeningOfFilmInWeekAsync(id)).ResultObj;
            return View(res);
        }

    }
}
