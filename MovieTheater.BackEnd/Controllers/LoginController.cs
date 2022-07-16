﻿using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTheater.Application.CustomerServices;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller

    {
        private readonly CustomerService _loginService;
        public LoginController(CustomerService loginService)
        {
            _loginService = loginService;
        }
        //[HttpPost("LoginStaff")]
        //public async Task<ApiResult<string>> LoginStaffAsync([FromBody] LoginRequest request)
        //{
        //    var result = await _loginService.LoginStaffAsync(request);
        //    return result;
        //}

        [HttpPost("LoginCustomer")]
        public async Task<ApiResult<string>> LoginCustomerAsync([FromBody] LoginRequest request)
        {
            var result = await _loginService.LoginAsync(request);
            return result;
        }
    }
}
