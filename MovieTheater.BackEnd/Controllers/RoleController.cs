using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using MovieTheater.Application.RoleService;
using MovieTheater.Application.TimeServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Price.Time;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Identity.Role;

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

        [HttpGet(APIConstant.RoleGetAll)]
        public async Task<ApiResult<List<RoleVMD>>> GetAllAsync()
        {
            var result = await _roleService.GetAllAsync();
            return result;
        }
    }
}
