using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Reservation;
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
        private readonly FilmApiClient _filmApiClient;
        private readonly ReservationApiClient _reservationApiClient;
        public RetailController(ScreeningApiClient screeningApiClient,ReservationApiClient reservationApiClient,
            FilmApiClient filmApiClient)
        {
            _screeningApiClient = screeningApiClient;
            _reservationApiClient = reservationApiClient;
            _filmApiClient = filmApiClient;
        }

        public async Task<IActionResult> ChooseSeat(int id)
        {
            var screening = (await _screeningApiClient.GetScreeningMDByIdAsync(id)).ResultObj;
            ViewBag.Film = (await _filmApiClient.GetFilmVMDByIdAsync(screening.FilmId)).ResultObj;
            return View(screening);
        }
        public async Task<IActionResult> Index(DateTime? date)
        {
            var listFlimScreening = (await _screeningApiClient.GetFilmScreeningIndateAsync(date)).ResultObj;
          
            return View(listFlimScreening);
        }
        [HttpPost]
        public async Task<int> CalPrePrice(List<TicketCreateRequest> tickets)
        {
            if (tickets == null)
                return 0;
            return await _reservationApiClient.CalPrePriceAsync(tickets);
        }
    }
}
