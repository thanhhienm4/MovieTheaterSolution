using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller

    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost("LoginStaff")]
        public async Task<ApiResult<string>> LoginStaffAsync([FromBody] LoginRequest request)
        {
            var result = await _loginService.LoginStaffAsync(request);
            return result;
        }

        [HttpPost("LoginCustomer")]
        public async Task<ApiResult<string>> LoginCustomerAsync([FromBody] LoginRequest request)
        {
            var result = await _loginService.LoginCustomerAsync(request);
            return result;
        }
    }
}
