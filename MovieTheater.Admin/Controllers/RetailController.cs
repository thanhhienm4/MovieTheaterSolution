using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Reservation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class RetailController : BaseController
    {
        private readonly ScreeningApiClient _screeningApiClient;
        private readonly ReservationApiClient _reservationApiClient;

        public RetailController(ScreeningApiClient screeningApiClient, ReservationApiClient reservationApiClient)
        {
            _screeningApiClient = screeningApiClient;
            _reservationApiClient = reservationApiClient;
        }

        public async Task<IActionResult> ChooseSeat(int id)
        {
            var result = (await _screeningApiClient.GetScreeningMDByIdAsync(id));
            if (result.ResultObj.StartTime.AddMinutes(GgTheaterConstant.MinuteLate) < DateTime.Now)
            {
                return RedirectToAction("Error", "Home");
            }
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            var reservation = _reservationApiClient.CreateAsync(new ReservationCreateRequest()
            {
                ScreeningId = id,
                Paid = PaymentStatusType.None,
                Active = true,
                EmployeeId = GetUserId(),
                ReservationTypeId = ReservationType.Offline,

            }).Result;

            return Redirect($"/Retail/Create/{reservation.ResultObj}");
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            ViewBag.SuccessMsg = TempData["Result"];
            var result = (await _screeningApiClient.GetScreeningInDateAsync(date));
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            return View(result.ResultObj);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var reservation = _reservationApiClient.GetReservationByIdAsync(id).Result.ResultObj;
            return View(reservation);
        }
    }
}