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
using MovieTheater.Models.Infra.Seat.SeatRow;
using MovieTheater.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    [AllowAnonymous]
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
            return RedirectToAction("Index", "Login");
        }
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
            var listRoles = (await _roleApiClient.GetRolesAsync())
                .Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = (!string.IsNullOrWhiteSpace(roleId)) && roleId == x.Id.ToString()
                }).ToList().OrderBy(x => x.Text);

            roles.AddRange(listRoles);
            ViewBag.Roles = roles;


            ViewBag.KeyWord = keyword;
            var result = await _userApiClient.GetUserPagingAsync(request);
            return View(result.ResultObj);
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
            var result = await _userApiClient.CreateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Tạo mới thành công";
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userApiClient.GetUserByIdAsync(id);

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

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _userApiClient.UpdateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }



        [HttpPost]
        public async Task<ApiResultLite> Delete(Guid id)
        {

            var result = await _userApiClient.DeleteAsync(id);
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;


            }
            return result;
        }
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var roleAssignRequest = await GetRoleAssignRequest(id);
            return View(roleAssignRequest);
        }



        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _userApiClient.RoleAssignAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Gán quyền thành công";
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetRoleAssignRequest(request.UserId);
            return View(roleAssignRequest);
        }

        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userObject = await _userApiClient.GetUserByIdAsync(id);
            var result = await _roleApiClient.GetRolesAsync();
            var roleAssignRequest = new RoleAssignRequest();
            roleAssignRequest.UserId = id;
            foreach (var role in result)
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
        [AllowAnonymous]
        public IActionResult Forbident()
        {
            return View();
        }

        
    }

}