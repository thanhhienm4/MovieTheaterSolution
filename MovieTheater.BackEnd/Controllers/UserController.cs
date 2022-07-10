using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.User;
using System;
using System.Threading.Tasks;
using MovieTheater.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        //private readonly IUserService _userService;

        public UserController(IUserService userService):base(userService)
        {
            _userService = userService;
        }

       
      

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateStaff")]
        public async Task<ApiResult<Guid>> CreateStaffAsync([FromBody] UserCreateRequest request)
        {
            var result = await _userService.CreateStaffAsync(request);
            return result;
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("UpdateStaff")]
        public async Task<ApiResult<bool>> UpdateStaffAsync([FromBody] UserUpdateRequest request)
        {
            var result = await _userService.UpdateStaffAsync(request);
            return result;
        }

      

        [HttpPost("CreateCustomer")]
        public async Task<ApiResult<bool>> CreateCustomerAsync([FromBody] UserCreateRequest request)
        {
            var result = await _userService.CreateCustomerAsync(request);
            return result;
        }

        [Authorize]
        [HttpPut("UpdateCustomer")]
        public async Task<ApiResult<bool>> UpdateCustomerAsync([FromBody] UserUpdateRequest request)
        {
            var result = await _userService.UpdateCustomerAsync(request);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            return result;
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<ApiResult<bool>> ChangePasswordAsync([FromBody] ChangePwRequest request)
        {
            var result = await _userService.ChangePasswordAsync(request);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("GetUserPaging")]
        public async Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request)
        {
            var result = await _userService.GetUserPagingAsync(request);
            return result;
        }

        [Authorize]
        [HttpGet("GetUserById/{id}")]
        public async Task<ApiResult<UserVMD>> GetUserById(Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("RoleAssign")]
        public async Task<ApiResult<bool>> RoleAssignAsync([FromBody] RoleAssignRequest request)
        {
            var result = await _userService.RoleAssignAsync(request);
            return result;
        }

        [Authorize]
        [HttpGet("GetCustomerById/{id}")]
        public async Task<ApiResult<UserVMD>> GetCustomerById(Guid id)
        {
            var result = await _userService.GetCustomerByIdAsync(id);
            return result;
        }

        [AllowAnonymous]
        [HttpPost("ForgotStaffPassword")]
        public async Task<ApiResult<bool>> ForgotStaffPassword([FromBody] string mail)
        {
            var result =  await _userService.ForgotStaffPasswordAsync(mail);
            return result;
        }

        [AllowAnonymous]
        [HttpPost("ForgotCustomerPassword")]
        public async Task<ApiResult<bool>> ForgotCustomerPassword([FromBody] string mail)
        {
            var result = await _userService.ForgotCustomerPasswordAsync(mail);
            return result;
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<ApiResult<bool>> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            var result = await _userService.ResetPasswordAsync(request);
            return result;
        }
        [AllowAnonymous]
        [HttpGet("GetCustomer")]
        public Customer GetCustomer()
        {
            var result =  _userService.GetCustomer();
            return result;
        }
    }
    
}