using Microsoft.AspNetCore.Mvc;
using Movietheater.Application;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.User;
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
        public async Task<ApiResult<string>> LoginAsync(LoginRequest request)
        {
            var result = await _userApi.LoginAsync(request);
            return result;
        }
    }
}