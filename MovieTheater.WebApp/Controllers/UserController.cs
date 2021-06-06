﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.User;
using System;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserApiClient _userApiClient;
        private readonly RoleApiClient _roleApiClient;
        private readonly ReservationApiClient _reservationApiClient;

        public UserController(UserApiClient userApiClient, RoleApiClient roleApiClient, ReservationApiClient reservationApiClient)
        {
            _userApiClient = userApiClient;
            _roleApiClient = roleApiClient;
            _reservationApiClient = reservationApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _userApiClient.CreateCustomerAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userApiClient.GetCustomerByIdAsync(id);

            if (result.IsSuccessed)
            {
                var updateRequest = new UserUpdateRequest()
                {
                    Id = id,
                    Dob = result.ResultObj.Dob,
                    Email = result.ResultObj.Email,
                    FirstName = result.ResultObj.FirstName,
                    PhoneNumber = result.ResultObj.PhoneNumber,
                    LastName = result.ResultObj.LastName,
                    Status = result.ResultObj.Status
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsEdit = true;
                return View(request);
            }
            var result = await _userApiClient.UpdateCustomerAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Order()
        {
            Guid id = GetUserId();
            var response = _reservationApiClient.GetReservationByUserIdAsync(id).Result;
            return View(response);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Forbident()
        {
            return View();
        }
    }
}