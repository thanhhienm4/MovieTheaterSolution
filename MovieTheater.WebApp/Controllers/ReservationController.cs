using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly ReservationApiClient _reservationApiClient;

        public ReservationController(ReservationApiClient ReservationApiClient)
        {
            _reservationApiClient = ReservationApiClient;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new ReservationPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            request.userId = GetUserId();

            ViewBag.KeyWord = keyword;
            ViewBag.SuccessMsg = TempData["Result"];
            var result = await _reservationApiClient.GetReservationPagingAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            return View(result.ResultObj);
        }

        [HttpPost]
        public async Task<ApiResult<int>> Create(ReservationCreateRequest request)
        {
            //can fix
            request.CustomerId = GetUserId();
            request.ReservationTypeId = "Web";

            var result = await _reservationApiClient.CreateAsync(request);
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _reservationApiClient.GetReservationByIdAsync(id);

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
    }
}