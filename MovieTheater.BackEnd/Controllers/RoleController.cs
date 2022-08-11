using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.RoleService;
using MovieTheater.Application.UserServices;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IUserService userService, IRoleService roleService) : base(userService)
        {
            _roleService = roleService;
        }

        [HttpGet(ApiConstant.RoleGetAll)]
        public async Task<ApiResult<List<RoleVMD>>> GetAllAsync()
        {
            var result = await _roleService.GetAllAsync();
            return result;
        }
    }
}