using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Screening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    public class RetailController : Controller
    {
        private readonly ScreeningApiClient _screeningApiClient;
        public RetailController(ScreeningApiClient screeningApiClient)
        {
            _screeningApiClient = screeningApiClient;
        }

        public async Task<IActionResult> ChooseSeat(int id)
        {
            var screening = (await _screeningApiClient.GetScreeningMDByIdAsync(id)).ResultObj;
            return View(screening);
        }
        public async Task<IActionResult> Index(DateTime? date)
        {
            var listFlimScreening = (await _screeningApiClient.GetFilmScreeningIndateAsync(date)).ResultObj;
            return View(listFlimScreening);
        }
           
    }
}
