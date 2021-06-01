﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movietheater.Application.UserServices
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ApiResult<bool>> CreateAsync(RoleCreateRequest model)
        {
            var role = new AppRole()
            {
                Name = model.Name,
                Description = model.Description
            };
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>(true);
            }
            else
            {
                return new ApiErrorResult<bool>("Tạo mới thất bại");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return new ApiSuccessResult<bool>(true);
                }
                else
                {
                    return new ApiErrorResult<bool>("Không thể xóa");
                }
            }
            else
            {
                return new ApiErrorResult<bool>("Role không tồn tại");
            }
        }

        public async Task<ApiResult<bool>> UpdateAsync(RoleUpdateRequest model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id.ToString());
            if (role == null)
                return new ApiErrorResult<bool>("Role không tồn tại");

            role.Name = model.Name;
            role.Description = model.Description;
            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
                return new ApiErrorResult<bool>("Cập nhật không thành công");
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<List<RoleVMD>>> GetAllRoles()
        {
            var roles = new List<RoleVMD>();
            roles = await _roleManager.Roles.Where(x => x.Id != new Guid("0417C463-9AF0-46D9-9FF7-D3E63321DFCC")).Select(x => new RoleVMD()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name
            }).ToListAsync();
            return new ApiSuccessResult<List<RoleVMD>>(roles);
        }
    }
}