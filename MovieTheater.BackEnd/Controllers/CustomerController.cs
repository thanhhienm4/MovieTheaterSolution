using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.CustomerServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.User;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        //private readonly IUserService _userService;

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

        //[Authorize(Roles = "Admin")]
        //[HttpPut("RoleAssign")]
        //public async Task<ApiResult<bool>> RoleAssignAsync([FromBody] RoleAssignRequest request)
        //{
        //    var result = await _customerService.RoleAssignAsync(request);
        //    return result;
        //}

        //[Authorize]
        //[HttpGet("GetCustomerById/{id}")]
        //public async Task<ApiResult<UserVMD>> GetCustomerById(Guid id)
        //{
        //    var result = await _customerService.GetCustomerByIdAsync(id);
        //    return result;
        //}

        //[AllowAnonymous]
        //[HttpPost("ForgotStaffPassword")]
        //public async Task<ApiResult<bool>> ForgotStaffPassword([FromBody] string mail)
        //{
        //    var result =  await _customerService.ForgotPasswordAsync(mail);
        //    return result;
        //}

        //[AllowAnonymous]
        //[HttpPost("ForgotCustomerPassword")]
        //public async Task<ApiResult<bool>> ForgotCustomerPassword([FromBody] string mail)
        //{
        //    var result = await _customerService.ForgotCustomerPasswordAsync(mail);
        //    return result;
        //}

        //[AllowAnonymous]
        //[HttpPost("ResetPassword")]
        //public async Task<ApiResult<bool>> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        //{
        //    var result = await _customerService.ResetPasswordAsync(request);
        //    return result;
        //}
    }
}