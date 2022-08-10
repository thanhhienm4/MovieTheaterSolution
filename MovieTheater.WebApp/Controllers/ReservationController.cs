﻿using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MovieTheater.Common.Constants;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Invoice;
using MovieTheater.Models.Payment;

namespace MovieTheater.WebApp.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly ReservationApiClient _reservationApiClient;
        private readonly InvoiceApiClient _invoiceApiClient;

        public ReservationController(ReservationApiClient reservationApiClient, InvoiceApiClient invoiceApiClient)
        {
            _reservationApiClient = reservationApiClient;
            _invoiceApiClient = invoiceApiClient;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new ReservationPagingRequest
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                userId = GetUserId()
            };

            ViewBag.KeyWord = keyword;
            ViewBag.SuccessMsg = TempData["Result"];
            var result = await _reservationApiClient.GetReservationPagingAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            return View(result.ResultObj);
        }

        [HttpPost]
        public async Task<string> Payment(int id)
        {
            //can fix
            var result = await _reservationApiClient.PaymentAsync(id);
            return result.ResultObj;
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var reservation = _reservationApiClient.GetReservationByIdAsync(id).Result.ResultObj;
            return View(reservation);
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
                return View(result.ResultObj);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> CompletePayment(decimal vnp_Amount, string vnp_BankCode, string vnp_BankTranNo,
            string vnp_CardType, string vnp_OrderInfo, string vnp_PayDate,
            string vnp_ResponseCode, string vnp_TmnCode, int vnp_TransactionNo,
            string vnp_TransactionStatus, string vnp_TxnRef, string vnp_SecureHash)
        {
            var payment = new PaymentVMD();
            if (vnp_ResponseCode == "00")
            {
                payment.Message = "Thanh toán thành công";
                await  _invoiceApiClient.CreateAsync(new InvoiceCreateRequest()
                {
                    Date = DateTime.Now,
                    PaymentId = PaymentType.VNPAY,
                    Price = vnp_Amount/100,
                    ReservationId = Int32.Parse(vnp_TxnRef)
                });
            }

            else
            {
                payment.Message = "Thanh toán thất bại";
            }

            return View(payment);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var reservation = _reservationApiClient.DeleteAsync(id).Result.ResultObj;   
            return RedirectToAction("Index", "Reservation");
        }

        [HttpPost]
        public bool DeletePost(int id)
        {
            var reservation = _reservationApiClient.DeleteAsync(id).Result.ResultObj;
            return reservation;
        }
    }
}