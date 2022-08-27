using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using System;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class FilmController : Controller
    {
        private readonly MovieApiClient _filmApiClient;
        private readonly ScreeningApiClient _screeningApiClient;

        public FilmController(MovieApiClient filmApiClient, ScreeningApiClient screeningApiClient)
        {
            _filmApiClient = filmApiClient;
            _screeningApiClient = screeningApiClient;
        }

        public async Task<IActionResult> Detail(string id)
        {
            var result = (await _filmApiClient.GetFilmVMDByIdAsync(id)).ResultObj;
            if(result == null)
                return NotFound();
            return View(result);
        }

        public async Task<IActionResult> ScreeningInWeek(string id)
        {
            var res = (await _screeningApiClient.GetListScreeningOfFilmInWeekAsync(id)).ResultObj;
            return View(res);
        }

        public async Task<IActionResult> Schedule(DateTime? date)
        {
            var result = (await _screeningApiClient.GetScreeningInDateAsync(date));
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            return View(result.ResultObj);
        }
    }
}