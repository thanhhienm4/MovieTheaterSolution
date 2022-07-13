using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.RoomServices;
using MovieTheater.Application.UserServices;
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

        public RoomFormatController(IRoomFormatService roomFormatService, IUserService customerService) : base(customerService)
        {
            _roomFormatService = roomFormatService;
        }
        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync(RoomFormatCreateRequest model)
        {
            var result = await _roomFormatService.CreateAsync(model);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(RoomFormatUpdateRequest request)
        {
            var result = await _roomFormatService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _roomFormatService.DeleteAsync(id);
            return result;
        }

        [HttpGet("getAllRoomFormat")]
        public async Task<ApiResult<List<RoomFormatVMD>>> GetAllRoomFormatAsync()
        {
            var result = await _roomFormatService.GetAllRoomFormatAsync();
            return result;
        }

        [HttpGet("getRoomFormatById/{id}")]
        public async Task<ApiResult<RoomFormatVMD>> GetRoomFormatByIdAsync(int id)
        {
            var result = await _roomFormatService.GetRoomFormatByIdAsync(id);
            return result;
        }

    }
}