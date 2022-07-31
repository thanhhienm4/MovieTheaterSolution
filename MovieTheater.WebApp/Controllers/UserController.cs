using Microsoft.AspNetCore.Authentication;
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

        public UserController(UserApiClient userApiClient, RoleApiClient roleApiClient,
            ReservationApiClient reservationApiClient)
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
        public async Task<IActionResult> Create(UserRegisterRequest request)
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
        public async Task<IActionResult> Edit(string id)
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
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangPassword(string id)
        {
            var result = (await _userApiClient.GetCustomerByIdAsync(id));
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            ViewBag.UserName = result.ResultObj.UserName;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangPassword(ChangePwRequest request)
        {
            if (User.Identity != null) request.UserName = User.Identity.Name;
            ViewBag.UserName = request.UserName;
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var res = (await _userApiClient.ChangePasswordAsync(request));
            if (res.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (res.IsSuccessed)
            {
                TempData["Result"] = res.Message;
                var userId = GetUserId();
                return Redirect($"/");
            }
            else
            {
                ModelState.AddModelError("", res.Message);
                return View(request);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Order()
        {
            var res = await _reservationApiClient.GetReservationByUserIdAsync(GetUserId());
            if (res.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            else return View(res.ResultObj);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Forbident()
        {
            return View();
        }
    }
}