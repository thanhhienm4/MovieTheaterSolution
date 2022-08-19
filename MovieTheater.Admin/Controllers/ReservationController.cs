using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Invoice;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ReservationController : BaseController
    {
        private readonly ReservationApiClient _reservationApiClient;
        private readonly InvoiceApiClient _invoiceApiClient;

        public ReservationController(ReservationApiClient reservationApiClient, InvoiceApiClient invoiceApiClient)
        {
            _reservationApiClient = reservationApiClient;
            _invoiceApiClient = invoiceApiClient;
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
            var reservation = _reservationApiClient.GetReservationByIdAsync(id).Result.ResultObj;
            if (reservation.Employee != GetUserId())
                return new ApiErrorResult<bool>();

            var result = await _reservationApiClient.DeleteAsync(id);
            TempData["Result"] = result.Message;
            return result;
        }

        [HttpPost]
        public bool DeletePost(int id)
        {
            var reservation = _reservationApiClient.DeleteAsync(id).Result.ResultObj;
            return reservation;
        }

        [HttpGet]
        public async Task<IActionResult> WaitPayment(int id)
        {
            var reservation = _reservationApiClient.GetReservationByIdAsync(id).Result.ResultObj;
            await _reservationApiClient.UpdatePaymentAsync(new ReservationUpdatePaymentRequest()
            {
                Id = id,
                Status = PaymentStatusType.Inprogress
            });
            return View(reservation);
        }

        [HttpGet]
        public async Task<IActionResult> CompletePayment(int id)
        {
            var reservation = _reservationApiClient.GetReservationByIdAsync(id).Result.ResultObj;
            if (reservation == null)
                Redirect("Home");
            await _invoiceApiClient.CreateAsync(new InvoiceCreateRequest()
            {
                Date = DateTime.Now,
                PaymentId = PaymentType.CASH,
                Price = reservation.TotalPrice,
                ReservationId = id
            });
            return Redirect($"/Reservation/Detail/{id}");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _reservationApiClient.GetReservationByIdAsync(id);

            if (result.IsSuccessed)
            {
                return View(result.ResultObj);
            }

            return RedirectToAction("Error", "Home");
        }


    }
}