using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.User;
using System;
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

        [HttpPost("LoginStaff")]
        public async Task<ApiResult<string>> LoginStaffAsync([FromBody] LoginRequest request)
        {
            var result = await _userService.LoginStaffAsync(request);
            return result;
        }

        [HttpPost("CreateStaff")]
        public async Task<ApiResultLite> CreateStaffAsync([FromBody] UserCreateRequest request)
        {
            var result = await _userService.CreateStaffAsync(request);
            return result;
        }

        [HttpPut("UpdateStaff")]
        public async Task<ApiResultLite> UpdateStaffAsync([FromBody] UserUpdateRequest request)
        {
            var result = await _userService.UpdateStaffAsync(request);
            return result;
        }

        [HttpPost("LoginCustomer")]
        public async Task<ApiResult<string>> LoginCustomerAsync([FromBody] LoginRequest request)
        {
            var result = await _userService.LoginCustomerAsync(request);
            return result;
        }

        [HttpPost("CreateCustomer")]
        public async Task<ApiResultLite> CreateCustomerAsync([FromBody] UserCreateRequest request)
        {
            var result = await _userService.CreateCustomerAsync(request);
            return result;
        }

        [HttpPut("UpdateCustomer")]
        public async Task<ApiResultLite> UpdateCustomerAsync([FromBody] UserUpdateRequest request)
        {
            var result = await _userService.UpdateCustomerAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResultLite> DeleteAsync(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
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
        public async Task<ApiResult<UserVMD>> GetUserById(Guid id)
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

        [HttpGet("GetCustomerById/{id}")]
        public async Task<ApiResult<UserVMD>> GetCustomerById(Guid id)
        {
            var result = await _userService.GetCustomerByIdAsync(id);
            return result;
        }
    }
}