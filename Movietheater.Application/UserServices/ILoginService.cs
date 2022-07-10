﻿using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.User;
using System;
using System.Threading.Tasks;

namespace MovieTheater.Application.UserServices
{
    public interface ILoginService 
    {
        Task<ApiResult<string>> LoginStaffAsync(LoginRequest request);
        Task<ApiResult<string>> LoginCustomerAsync(LoginRequest request);
    }

}