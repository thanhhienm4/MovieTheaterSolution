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
    [Authorize(Roles = "Admin")]
    public class PositionController : BaseController
    {
        private readonly IPositionService _positionService;
        private readonly IUserService _userService;

        public PositionController(IPositionService positionService, IUserService userService) : base(userService)
        {
            _positionService = positionService;
        }

        [HttpGet("GetAllPosition")]
        public async Task<ApiResult<List<PositionVMD>>> GetAllPosition()
        {
            var result = await _positionService.GetAllPositionAsync();
            return result;
        }
    }
}