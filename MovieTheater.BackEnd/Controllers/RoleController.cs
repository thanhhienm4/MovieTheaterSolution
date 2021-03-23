using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.UserService.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly RoleService _roleService;
        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<ApiResultLite> CreateAsync(RoleCreateRequest model)
        {
            var result = await _roleService.CreateAsync(model);
            return result;
        }
    }
}
