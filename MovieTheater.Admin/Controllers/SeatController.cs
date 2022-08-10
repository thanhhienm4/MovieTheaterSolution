﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    public class SeatController : Controller
    {
        private readonly SeatApiClient _seatApiClient;

        public SeatController(SeatApiClient seatApiClient)
        {
            _seatApiClient = seatApiClient;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> KindOfSeat()
        {
            var res = await _seatApiClient.GetAllKindOfSeatAsync();
            if (res.IsReLogin == true)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.SuccessMsg = TempData["Result"];
                return View(res.ResultObj);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> CreateKindOfSeat()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateKindOfSeat(SeatTypeCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _seatApiClient.CreateKindOfSeatAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("KindOfSeat", "Seat");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditKindOfSeat(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _seatApiClient.GetKindOfSeatByIdAsync(id);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            if (result.IsSuccessed)
            {
                var updateRequest = new KindOfSeatUpdateRequest()
                {
                    Id = result.ResultObj.Id,
                    Name = result.ResultObj.Name,
                };

                return View(updateRequest);
            }

            return RedirectToAction("Error", "Home");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditKindOfSeat(KindOfSeatUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsEdit = true;

                return View(request);
            }

            var result = await _seatApiClient.UpdateKindOfSeatAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("KindOfSeat", "Seat");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<bool>> Delete(int id)
        {
            var result = await _seatApiClient.DeleteKindOfSeatAsync(id);

            TempData["Result"] = result.Message;
            return result;
        }
    }
}