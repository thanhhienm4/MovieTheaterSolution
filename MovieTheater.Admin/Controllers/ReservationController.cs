using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ReservationController : BaseController
    {
        private readonly ReservationApiClient _reservationApiClient;

        public ReservationController(ReservationApiClient ReservationApiClient)
        {
            _reservationApiClient = ReservationApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 15)
        {
            var request = new ReservationPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            ViewBag.KeyWord = keyword;
            ViewBag.SuccessMsg = TempData["Result"];
            var result = await _reservationApiClient.GetReservationPagingAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            return View(result.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ApiResult<int>> Create(ReservationCreateRequest request)
        {
            //can fix
            request.EmployeeId = GetUserId();
            request.ReservationTypeId = "";

            var result = await _reservationApiClient.CreateAsync(request);

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _reservationApiClient.GetReservationByIdAsync(id);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            if (result.IsSuccessed)
            {
                ViewBag.Reservation = result.ResultObj;
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
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index", "Reservation");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<ApiResult<bool>> Delete(int id)
        {
            var result = await _reservationApiClient.DeleteAsync(id);

            TempData["Result"] = result.Message;
            return result;
        }
    }
}