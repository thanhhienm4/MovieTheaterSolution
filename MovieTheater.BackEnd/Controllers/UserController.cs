using Microsoft.AspNetCore.Mvc;
using Movietheater.Application;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
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
        private readonly UserService _userApi;

        public UserController(UserService userApi)
        {
            _userApi = userApi;
        }

        [HttpPost("Login")]
        public async Task<ApiResult<string>> LoginAsync([FromBody] LoginRequest request)
        {
            var result = await _userApi.LoginAsync(request);
            return result;
        }

        [HttpPost("Create")]
        public async Task<ApiResultLite> CreateAsync ([FromBody] UserCreateRequest request)
        {
            var result = await _userApi.CreateAsync(request);
            return result;
        }

        [HttpPost("Delete")]
        public async Task<ApiResultLite> DeleteAsync(Guid id)
        {
            var result = await _userApi.DeleteAsync(id);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ApiResultLite> UpdateAsync([FromBody] UserUpdateRequest request)
        {
            var result = await _userApi.UpdateAsync(request);
            return result;
        }

        [HttpPost("ChangePassword")]
        public async Task<ApiResultLite> ChangePasswordAsync([FromBody] ChangePWRequest request)
        {
            var result = await _userApi.ChangePasswordAsync(request);
            return result;
        }
        [HttpPost("GetUserPaging")]
        public async Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request)
        {
            var result = await _userApi.GetUserPagingAsync(request);
            return result;
        }
    }
}