using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Api;
using MovieTheater.Data.Enums;
using MovieTheater.Models.Common;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using MovieTheater.Models.Infra.Seat.SeatRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    [AllowAnonymous]
    public class SeatRowController : BaseController
    {
        private readonly SeatRowApiClient _seatRowApiClient;
        public SeatRowController(SeatRowApiClient seatRowApiClient, RoleApiClient roleApiClient)
        {
            _seatRowApiClient = seatRowApiClient;
        }

        [HttpGet]
        public async Task<List<SeatRowVMD>> GetAllSeatRows()
        {
            var result = (await _seatRowApiClient.GetAllSeatRowsAsync()).ResultObj;
            return result;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new SeatRowPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,

            };
            ViewBag.SuccessMsg = TempData["Result"];
            ViewBag.KeyWord = keyword;
            var result = await _seatRowApiClient.GetSeatRowPagingAsync(request);
            return View(result.ResultObj);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SeatRowCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _seatRowApiClient.CreateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Tạo mới thành công";
                return RedirectToAction("Index", "SeatRow");
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
            var result = await _seatRowApiClient.GetSeatRowByIdAsync(id);

            if (result.IsSuccessed)
            {
                var updateRequest = new SeatRowUpdateRequest()
                {
                    Id = result.ResultObj.Id,
                    Name = result.ResultObj.Name
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeatRowUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsEdit = true;
                return View(request);
            }
            var result = await _seatRowApiClient.UpdateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index", "SeatRow");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }


        [HttpPost]
        public async Task<ApiResultLite> Delete(int id)
        {
            var result = await _seatRowApiClient.DeleteAsync(id);
            if(result.IsSuccessed)
                TempData["Result"] = result.Message;
            return result;
        }
    }

}
