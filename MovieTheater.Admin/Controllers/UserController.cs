using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Data.Enums;
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
        public UserController(UserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword,Status? status,string roleId,int  pageIndex = 1,int pageSize = 10)
        {
            var request = new UserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                RoleId = roleId,
                Status = status
            };
            var result =await _userApiClient.GetUserPaging(request);
            return View(result.ResultObj);
        }
    }
}
