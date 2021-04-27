using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class TicketController : Controller
    {
        private readonly ScreeningApiClient _screeningApiClient;
        private readonly FilmApiClient _filmApiClient;
        private readonly ReservationApiClient _reservationApiClient;
        public TicketController(ScreeningApiClient screeningApiClient, ReservationApiClient reservationApiClient,
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
       
        [HttpPost] 
        public async Task<int> CalPrePrice(List<TicketCreateRequest> tickets)
        {
            if (tickets == null)
                return 0;
            return await _reservationApiClient.CalPrePriceAsync(tickets);
        }
    }
}
