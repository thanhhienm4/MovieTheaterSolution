using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Price.Time;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TimeController : Controller
    {
        private readonly TimeApiClient _timeApiClient;

        public TimeController(TimeApiClient timeApiClient)
        {
            _timeApiClient = timeApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 15)
        {
            var request = new TimePagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            ViewBag.SuccessMsg = TempData["Result"];
            ViewBag.KeyWord = keyword;
            var result = await _timeApiClient.GetPagingAsync(request);
            if (result.IsReLogin == true)
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
        public async Task<IActionResult> Create(TimeCreateRequest request)
        {
            SetViewBagAsync();
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _timeApiClient.CreateAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Tạo mới thành công";
                return RedirectToAction("Index", "Time");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            SetViewBagAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _timeApiClient.GetByIdAsync(id);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            if (result.IsSuccessed)
            {
                var time = result.ResultObj;
                var updateRequest = new TimeUpdateRequest()
                {
                    TimeId = time.TimeId,
                    Name = time.Name,
                    DateEnd = time.DateEnd,
                    DateStart = time.DateStart,
                    HourEnd = time.HourEnd,
                    HourStart = time.HourStart
                };
                return View(updateRequest);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TimeUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagAsync();
                ViewBag.IsEdit = true;
                return View(request);
            }

            var result = await _timeApiClient.UpdateAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index", "SeatRow");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<ApiResult<bool>> Delete(string id)
        {
            var result = await _timeApiClient.DeleteAsync(id);
            if (result.IsSuccessed)
                TempData["Result"] = result.Message;
            return result;
        }

        private void SetViewBagAsync()
        {
            ViewBag.DateOfWeek = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Thứ 2", Value = "0" },
                new SelectListItem() { Text = "Thứ 3", Value = "1" },
                new SelectListItem() { Text = "Thứ 4", Value = "2" },
                new SelectListItem() { Text = "Thứ 5", Value = "3" },
                new SelectListItem() { Text = "Thứ 6", Value = "4" },
                new SelectListItem() { Text = "Thứ 7", Value = "5" },
                new SelectListItem() { Text = "Chủ nhật", Value = "6" },
            };
        }
    }
}