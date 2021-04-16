﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    public class ScreeningController : Controller
    {
        private readonly SeatApiClient _seatApiClient;
        private readonly ScreeningApiClient _screeningApiClient;
        public ScreeningController(SeatApiClient seatApiClient, ScreeningApiClient screeningApiClient)
        {
            _seatApiClient = seatApiClient;
            _screeningApiClient = screeningApiClient;

        }
        [HttpGet]
        public async Task<List<SeatVMD>> GetListSeatReserved(int id)
        {
            var result = (await _seatApiClient.GetListSeatReserved(id)).ResultObj;
            return result;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {

            var request = new ScreeningPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,

            };

            ViewBag.KeyWord = keyword;
            var result = (await _screeningApiClient.GetScreeningPagingAsync(request)).ResultObj;
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScreeningCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _screeningApiClient.CreateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Tạo mới thành công";
                return RedirectToAction("Index", "Screening");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _screeningApiClient.GetScreeningMDByIdAsync(id);

            if (result.IsSuccessed)
            {
                var updateRequest = new ScreeningUpdateRequest()
                {
                    Id = result.ResultObj.Id,
                    FilmId = result.ResultObj.FilmId,
                    KindOfScreeningId = result.ResultObj.KindOfScreeningId,
                    RoomId = result.ResultObj.RoomId,
                    TimeStart = result.ResultObj.TimeStart

                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ScreeningUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _screeningApiClient.UpdateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index", "Screening");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

    }
}
