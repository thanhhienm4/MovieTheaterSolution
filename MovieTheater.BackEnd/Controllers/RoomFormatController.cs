using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.RoomServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomFormatController : Controller
    {
        private readonly IRoomFormatService _roomFormatService;

        public RoomFormatController(IRoomFormatService roomFormatService)
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