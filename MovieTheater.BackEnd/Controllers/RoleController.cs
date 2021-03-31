using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
//[Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("Create")]
        public async Task<ApiResultLite> CreateAsync(RoleCreateRequest model)
        {
            var result = await _roleService.CreateAsync(model);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ApiResultLite> UpdateAsync(RoleUpdateRequest request)
        {
            var result = await _roleService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResultLite> DeleteAsync(string id)
        {
            var result = await _roleService.DeleteAsync(id);
            return result;
        }

        [HttpGet("GetAll")]
        public async Task<List<RoleVMD>> GetAll()
        {
            var result = await _roleService.GetAllRoles();
            return result;
        }



    }
}
