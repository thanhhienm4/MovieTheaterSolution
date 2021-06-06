﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.Infra.RoomModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public RoleController(IRoleService roleService, IUserService userService) : base(userService)
        { 
            _roleService = roleService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync(RoleCreateRequest model)
        {
            var result = await _roleService.CreateAsync(model);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(RoleUpdateRequest request)
        {
            var result = await _roleService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            var result = await _roleService.DeleteAsync(id);
            return result;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResult<List<RoleVMD>>> GetAll()
        {
            var result = await _roleService.GetAllRoles();
            return result;
        }
       
       
    }
}