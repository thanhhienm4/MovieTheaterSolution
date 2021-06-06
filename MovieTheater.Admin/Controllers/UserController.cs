using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using MovieTheater.Api;
using MovieTheater.Data.Enums;
using MovieTheater.Models.Common;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Index(string keyword, Status? status, string roleId, int pageIndex = 1, int pageSize = 10)
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
            var listRoles = (await _roleApiClient.GetRolesAsync()).ResultObj
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
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            return View(result.ResultObj);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _userApiClient.CreateStaffAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                string url = $"{this.Request.Scheme}://{this.Request.Host}/User/RoleAssign/{result.ResultObj.ToString()}";

                return Redirect(url);
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
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
                    Id = id,
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
                TempData["Result"] = result.Message;
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var result = await _userApiClient.DeleteAsync(id);
            TempData["Result"] = result.Message;
            return result;
        }

        public async Task<IActionResult> RoleAssign(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var roleAssignRequest = await GetRoleAssignRequest(id);
            var result = (await _userApiClient.GetUserByIdAsync(id));
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            var userInfor = result.ResultObj;
            ViewBag.UserInfor = userInfor;

            return View(roleAssignRequest);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            var userInfor = await _userApiClient.GetUserByIdAsync(request.UserId);
            ViewBag.UserInfor = userInfor;
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _userApiClient.RoleAssignAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            if (result.IsSuccessed)
            {
                TempData["Result"] = "Gán quyền thành công";
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetRoleAssignRequest(request.UserId);
            return View(roleAssignRequest);
        }

        [Authorize(Roles = "Admin")]
        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userObject = await _userApiClient.GetUserByIdAsync(id);
            var result = await _roleApiClient.GetRolesAsync();
            var roleAssignRequest = new RoleAssignRequest();
            roleAssignRequest.UserId = id;
            foreach (var role in result.ResultObj)
            {
                roleAssignRequest.Roles.Add(new SelectedItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = userObject.ResultObj.Roles.Contains(role.Name)
                });
            }

            return roleAssignRequest;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangPassword(Guid id)
        {
            var result = (await _userApiClient.GetUserByIdAsync(id));
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            ViewBag.User = result.ResultObj;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangPassword(ChangePWRequest request)
        {
            request.UserName = User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var res = (await _userApiClient.ChangePasswordAsync(request));
            if (res.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (res.IsSuccessed)
            {
                var userId = GetUserId();
                return Redirect($"/user/Edit/{userId}");
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model )
        {
            if (!ModelState.IsValid)
                return View(model);
            else
            {
                var response = await _userApiClient.ForgotPasswordAsync(model.Email);
                if(response.IsSuccessed == true)
                {
                   
                    ViewBag.Message = "Đã gửi link xác nhận về địa chỉ mail của bạn";
                }else
                {
                    ViewBag.Message = response.Message;
                }    
                return View(model);
            }
               

        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }else
            {
                var response = await _userApiClient.ResetPasswordAsync(request);
                if (response.IsSuccessed == true)
                {

                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewBag.Message = response.Message;
                }
                return View(request);
            }
        }    

    }
}