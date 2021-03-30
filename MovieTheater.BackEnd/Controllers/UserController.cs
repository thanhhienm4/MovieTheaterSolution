using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<ApiResult<string>> LoginAsync([FromBody] LoginRequest request)
        {
            var result = await _userService.LoginAsync(request);
            return result;
        }

        [HttpPost("Create")]
        public async Task<ApiResultLite> CreateAsync ([FromBody] UserCreateRequest request)
        {
            var result = await _userService.CreateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResultLite> DeleteAsync(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResultLite> UpdateAsync([FromBody] UserUpdateRequest request)
        {
            var result = await _userService.UpdateAsync(request);
            return result;
        }

        [HttpPut("ChangePassword")]
        public async Task<ApiResultLite> ChangePasswordAsync([FromBody] ChangePWRequest request)
        {
            var result = await _userService.ChangePasswordAsync(request);
            return result;
        }

        [HttpPost("GetUserPaging")]
        public async Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request)
        {
            var result = await _userService.GetUserPagingAsync(request);
            return result;
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ApiResult<UserVMD>> GetUserById(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return result;
        }

        [HttpPut("RoleAssign")]
        public async Task<ApiResultLite> RoleAssignAsync([FromBody] RoleAssignRequest request)
        {
            var result = await _userService.RoleAssignAsync(request);
            return result;
        }

      
    }
}