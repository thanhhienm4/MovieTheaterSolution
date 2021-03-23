using Microsoft.AspNetCore.Identity;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<ApiResultLite> CreateAsync(RoleCreateRequest model)
        {
            var role = new AppRole()
            {
                Name = model.Name,
                Description = model.Description
            };
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResultLite("Tạo mới thành công");
            }
            else
            {
                return new ApiErrorResultLite("Tạo mới thất bại");
            }
        }
        public async Task<ApiResultLite> Delete(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return new ApiSuccessResultLite("Cập nhật thành công");
                }
                else
                {
                    return new ApiErrorResultLite("Không thể xóa");
                }
            }
            else
            {
                return new ApiErrorResultLite("Role không tồn tại");
            }
        }
        public async Task<ApiResultLite> Update(RoleUpdateRequest model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id.ToString());
            if (role != null)
            {
                role.Name = model.Name;
                role.Description = model.Description;

                IdentityResult result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return new ApiSuccessResultLite("Cập nhật thành công");
                }
                else
                {
                    return new ApiErrorResultLite("Không thể cập nhật");
                }
            }
            else
            {
                return new ApiErrorResultLite("Role không tồn tại");
            }
        }

    }
}
