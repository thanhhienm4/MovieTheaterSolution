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
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserApiClient _userApiClient;
        private readonly RoleApiClient _roleApiClient;

        public UserController(UserApiClient userApiClient, RoleApiClient roleApiClient)
        {
            _userApiClient = userApiClient;
            _roleApiClient = roleApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Delete("Token");

            return RedirectToAction("Index", "Login");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index(string keyword, Status? status, string roleId, int pageIndex = 1,
            int pageSize = 15)
        {
            var request = new UserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                RoleId = roleId,
                Status = status
            };

            List<SelectListItem> roles = new List<SelectListItem>();
            roles.Add(new SelectListItem() { Text = "Tất cả", Value = "" });
            var response = await _roleApiClient.GetRolesAsync();
            if (response.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            var listRoles = response.ResultObj
                .Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = (!string.IsNullOrWhiteSpace(roleId)) && roleId == x.Id.ToString()
                }).ToList().OrderBy(x => x.Text);

            ViewBag.SuccessMsg = TempData["Result"];
            roles.AddRange(listRoles);
            ViewBag.Roles = roles;

            ViewBag.KeyWord = keyword;
            var result = await _userApiClient.GetUserPagingAsync(request);

            return View(result.ResultObj);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await SetViewBag();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                await SetViewBag();
                return View(request);
            }

            var result = await _userApiClient.CreateStaffAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Thêm thành công";
                return RedirectToAction("Index", "User");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            await SetViewBag();
            if (!ModelState.IsValid)
            {
                return View();
            }


            var result = await _userApiClient.GetUserByIdAsync(id);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            if (result.IsSuccessed)
            {
                var updateRequest = new UserUpdateRequest()
                {
                    UserName = result.ResultObj.UserName,
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

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            await SetViewBag();
            if (!ModelState.IsValid)
            {
                ViewBag.IsEdit = true;
                return View(request);
            }

            var result = await _userApiClient.UpdateStaffAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Cập nhật thành công";

                if (!User.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == "Admin"))
                {
                    return RedirectToAction("Index", "Retail");
                }

                return RedirectToAction("Index", "User");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<bool>> Delete(string id)
        {
            var result = await _userApiClient.DeleteAsync(id);
            TempData["Result"] = result.Message;
            return result;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangPassword(string id)
        {
            var result = (await _userApiClient.GetUserByIdAsync(id));
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            ViewBag.UserName = result.ResultObj.UserName;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangPassword(ChangePwRequest request)
        {
            request.UserName = User.Identity.Name;
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
                if (User.Claims.Where(x => x.Type == ClaimTypes.Role && x.Value == "Admin").Count() != 0)
                {
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index", "Retail");
            }
            else
            {
                ModelState.AddModelError("", res.Message);
                return View(request);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Forbident()
        {
            return View();
        }

        public async Task SetViewBag()
        {
            var roles = (await _roleApiClient.GetRolesAsync()).ResultObj;
            ViewBag.Roles = roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }
    }
}