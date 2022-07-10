using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.FilmServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class BanController : BaseController
    {
        private readonly IBanService _banService;
        private readonly IUserService _userService;


        public BanController(IBanService banService,IUserService userService):base(userService)
        {
            _banService = banService;
        }

        [HttpGet("getAllBan")]
        public async Task<ApiResult<List<BanVMD>>> GetAllBanAsync()
        {
            var result = await _banService.GetAllBanAsync();
            return result;
        }
    }
}