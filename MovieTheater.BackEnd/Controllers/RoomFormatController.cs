using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.RoomServices;
using Movietheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoomFormatController : BaseController
    {
        private readonly IRoomFormatService _roomFormatService;
        private readonly IUserService _userService;

        public RoomFormatController(IRoomFormatService roomFormatService, IUserService userService) : base(userService)
        {
            _roomFormatService = roomFormatService;
        }

        [HttpGet("getAllRoomFormat")]
        public async Task<ApiResult<List<RoomFormatVMD>>> GetAllRoomFormatAsync()
        {
            var result = await _roomFormatService.GetAllRoomFormatAsync();
            return result;
        }
    }
}