using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Models.Price.Surcharge;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SurchargeController : Controller
    {
        private readonly SurchargeApiClient _surchargeApiClient;
        private readonly SeatApiClient _seatApiClient;
        private readonly AuditoriumApiClient _auditoriumApiClient;

        public SurchargeController(SurchargeApiClient surchargeApiClient,
            AuditoriumApiClient auditoriumApiClient, SeatApiClient seatApiClient)
        {
            _surchargeApiClient = surchargeApiClient;
            _auditoriumApiClient = auditoriumApiClient;
            _seatApiClient = seatApiClient;
        }

        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate, int pageIndex = 1,
            int pageSize = 15)
        {
            DateTime now = DateTime.Now;

            fromDate ??= new DateTime(now.Year, 1, 1);
            toDate ??= new DateTime(now.Year + 1, 1, 1).AddDays(-1);

            var request = new SurchargePagingRequest()
            {
                FromTime = fromDate,
                ToTime = toDate,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            ViewBag.SuccessMsg = TempData["Result"];
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            var result = await _surchargeApiClient.GetPagingAsync(request);
            if (result.IsReLogin)
                return RedirectToAction("Index", "Login");
            return View(result.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SetViewBagAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SurchargeCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagAsync();
                return View(request);
            }

            var result = await _surchargeApiClient.CreateAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Tạo mới thành công";
                return RedirectToAction("Index", "Surcharge");
            }

            SetViewBagAsync();
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            SetViewBagAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _surchargeApiClient.GetByIdAsync(id);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            if (result.IsSuccessed)
            {
                var surcharge = result.ResultObj;
                var updateRequest = new SurchargeUpdateRequest()
                {
                    Id = surcharge.Id,
                    Price = surcharge.Price,
                    AuditoriumFormatId = surcharge.AuditoriumFormatId,
                    EndDate = surcharge.EndDate,
                    SeatType = surcharge.SeatType,
                    StartDate = surcharge.StartDate
                };
                return View(updateRequest);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SurchargeUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagAsync();
                ViewBag.IsEdit = true;
                return View(request);
            }

            var result = await _surchargeApiClient.UpdateAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index", "Surcharge");
            }

            SetViewBagAsync();
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<ApiResult<bool>> Delete(string id)
        {
            var result = await _surchargeApiClient.DeleteAsync(id);
            if (result.IsSuccessed)
                TempData["Result"] = result.Message;
            return result;
        }

        private void SetViewBagAsync()
        {
            ViewBag.SeatTypes = _seatApiClient.GetAllKindOfSeatAsync().Result.ResultObj.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id
            });

            ViewBag.AuditoriumFormats = _auditoriumApiClient.GetAllRoomFormatAsync().Result.ResultObj.Select(x =>
                new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id
                });
        }
    }
}