﻿using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.RoleService
{
    public interface IRoleService
    {
        Task<ApiResult<List<RoleVMD>>> GetAllAsync();
    }
}