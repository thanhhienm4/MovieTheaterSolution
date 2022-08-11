using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;
using MovieTheater.Models.Price.TicketPrice;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TicketPriceController : Controller
    {
        private readonly TicketPriceApiClient _ticketPriceApiClient;

        public TicketPriceController(TicketPriceApiClient ticketPriceApiClient)
        {
            _ticketPriceApiClient = ticketPriceApiClient;
        }

        public async Task<IActionResult> Index(DateTime? fromDate,DateTime? toDate, int pageIndex = 1, int pageSize = 15)
        {
            DateTime now = DateTime.Now;

            fromDate = fromDate ?? new DateTime(now.Year,1,1);
            toDate = toDate ?? new DateTime(now.Year + 1, 1, 1).AddDays(-1);

            var request = new TicketPricePagingRequest()
            {
                FromTime = fromDate,
                ToTime = toDate,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            ViewBag.SuccessMsg = TempData["Result"];
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            var result = await _ticketPriceApiClient.GetPagingAsync(request);
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
        public async Task<IActionResult> Create(TicketPriceCreateRequest request)
        {
            SetViewBagAsync();
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _ticketPriceApiClient.CreateAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Tạo mới thành công";
                return RedirectToAction("Index", "TicketPrice");
            }

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

            var result = await _ticketPriceApiClient.GetByIdAsync(id);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            if (result.IsSuccessed)
            {
                var ticketPrice = result.ResultObj;
                var updateRequest = new TicketPriceUpdateRequest()
                {
                    Id = ticketPrice.Id,
                    AuditoriumFormat = ticketPrice.AuditoriumFormat,
                    Price = ticketPrice.Price,
                    CustomerType = ticketPrice.CustomerType,
                    ToTime = ticketPrice.ToTime,
                    FromTime = ticketPrice.FromTime,
                    TimeId = ticketPrice.TimeId
                };
                return View(updateRequest);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TicketPriceUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagAsync();
                ViewBag.IsEdit = true;
                return View(request);
            }

            var result = await _ticketPriceApiClient.UpdateAsync(request);
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
            var result = await _ticketPriceApiClient.DeleteAsync(id);
            if (result.IsSuccessed)
                TempData["Result"] = result.Message;
            return result;
        }

        private void SetViewBagAsync()
        {
           
        }
    }
}