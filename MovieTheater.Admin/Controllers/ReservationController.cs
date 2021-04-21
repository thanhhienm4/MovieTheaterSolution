using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Data.Enums;
using MovieTheater.Models.Catalog.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly ReservationApiClient _reservationApiClient;
        public ReservationController(ReservationApiClient ReservationApiClient)
        {
            _reservationApiClient = ReservationApiClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new ReservationPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            ViewBag.KeyWord = keyword;
            var result = await _reservationApiClient.GetReservationPagingAsync(request);
            return View(result.ResultObj);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            //can fix
            request.EmployeeId = GetUserId();
            request.ReservationTypeId = 1;
    
            var result = await _reservationApiClient.CreateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Tạo mới thành công";
                return RedirectToAction("Index", "Reservation");
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
            var result = await _reservationApiClient.GetReservationByIdAsync(id);

            if (result.IsSuccessed)
            {
                var updateRequest = new ReservationUpdateRequest()
                {
                    Id = result.ResultObj.Id,                 
                    Paid = result.ResultObj.Paid,
                    Active = result.ResultObj.Active
                   
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReservationUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsEdit = true;
                return View(request);
            }
            var result = await _reservationApiClient.UpdateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index", "Reservation");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        
    }
}
