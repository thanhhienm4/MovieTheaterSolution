using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.CustomerServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.User;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService, IUserService userService) : base(userService)
        {
            _customerService = customerService;
        }

        [HttpPost(ApiConstant.CustomerLogin)]
        public async Task<ApiResult<string>> LoginCustomerAsync([FromBody] LoginRequest request)
        {
            var result = await _customerService.LoginAsync(request);
            return result;
        }

        [HttpPost(ApiConstant.CustomerRegister)]
        public async Task<ApiResult<bool>> CreateStaffAsync([FromBody] UserRegisterRequest request)
        {
            var result = await _customerService.RegisterAsync(request);
            return result;
        }

        [HttpGet(ApiConstant.CustomerTypeGetAll)]
        public async Task<ApiResult<IList<CustomerTypeVmd>>> GetAllCustomerType()
        {
            var result = await _customerService.GetAllCustomerType();
            return result;
        }

        //[Authorize(Roles = "Admin,Employee")]
        //[HttpPut("UpdateStaff")]
        //public async Task<ApiResult<bool>> UpdateAsync([FromBody] UserUpdateRequest request)
        //{
        //    var result = await _customerService.UpdateAsync(request);
        //    return result;
        //}

        //[HttpPost("CreateCustomer")]
        //public async Task<ApiResult<bool>> CreateCustomerAsync([FromBody] UserCreateRequest request)
        //{
        //    var result = await _customerService.CreateCustomerAsync(request);
        //    return result;
        //}

        //[Authorize]
        //[HttpPut("UpdateCustomer")]
        //public async Task<ApiResult<bool>> UpdateCustomerAsync([FromBody] UserUpdateRequest request)
        //{
        //    var result = await _customerService.UpdateCustomerAsync(request);
        //    return result;
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpDelete("Delete/{id}")]
        //public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        //{
        //    var result = await _customerService.DeleteAsync(id);
        //    return result;
        //}

        //[Authorize]
        //[HttpPut("ChangePassword")]
        //public async Task<ApiResult<bool>> ChangePasswordAsync([FromBody] ChangePwRequest request)
        //{
        //    var result = await _customerService.ChangePasswordAsync(request);
        //    return result;
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpPost("GetUserPaging")]
        //public async Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request)
        //{
        //    var result = await _customerService.GetUserPagingAsync(request);
        //    return result;
        //}

        //[Authorize]
        //[HttpGet("GetUserById/{id}")]
        //public async Task<ApiResult<UserVMD>> GetUserById(Guid id)
        //{
        //    var result = await _customerService.GetUserByIdAsync(id);
        //    return result;
        //}


        //[Authorize]
        //[HttpGet("GetCustomerById/{id}")]
        //public async Task<ApiResult<UserVMD>> GetCustomerById(Guid id)
        //{
        //    var result = await _customerService.GetCustomerByIdAsync(id);
        //    return result;
        //}


        [AllowAnonymous]
        [HttpPost(ApiConstant.CustomerForgotPassword)]
        public async Task<ApiResult<bool>> ForgotCustomerPassword([FromBody] string mail)
        {
            var result = await _customerService.ForgotPasswordAsync(mail);
            return result;
        }

        //[AllowAnonymous]
        //[HttpPost("ResetPassword")]
        //public async Task<ApiResult<bool>> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        //{
        //    var result = await _customerService.ResetPasswordAsync(request);
        //    return result;
        //}
    }
}