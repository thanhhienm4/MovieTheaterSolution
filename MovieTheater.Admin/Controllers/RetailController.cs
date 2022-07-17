using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Reservation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class RetailController : Controller
    {
        private readonly ScreeningApiClient _screeningApiClient;
        private readonly MovieApiClient _filmApiClient;
        private readonly ReservationApiClient _reservationApiClient;

        public RetailController(ScreeningApiClient screeningApiClient, ReservationApiClient reservationApiClient,
            MovieApiClient filmApiClient)
        {
            _screeningApiClient = screeningApiClient;
            _reservationApiClient = reservationApiClient;
            _filmApiClient = filmApiClient;
        }

        public async Task<IActionResult> ChooseSeat(int id)
        {
            var result = (await _screeningApiClient.GetScreeningMDByIdAsync(id));
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            ViewBag.Film = (await _filmApiClient.GetFilmVMDByIdAsync(result.ResultObj.MovieId)).ResultObj;
            return View(result.ResultObj);
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            ViewBag.SuccessMsg = TempData["Result"];
            var result = (await _screeningApiClient.GetScreeningInDateAsync(date));
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            return View(result.ResultObj);
        }

        [HttpPost]
        public async Task<int> CalPrePrice(List<TicketCreateRequest> tickets)
        {
            if (tickets == null)
                return 0;
            return (await _reservationApiClient.CalPrePriceAsync(tickets)).ResultObj;
        }
    }
}