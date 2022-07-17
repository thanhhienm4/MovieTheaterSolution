using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.RoomServices.RoomFormats;
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
        private readonly IAuditoriumFormatService _roomFormatService;

        public RoomFormatController(IAuditoriumFormatService roomFormatService, IUserService userService) : base(
            userService)
        {
            _roomFormatService = roomFormatService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync(AuditoriumFormatCreateRequest model)
        {
            var result = await _roomFormatService.CreateAsync(model);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(AuditoriumFormatUpdateRequest request)
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
        public async Task<ApiResult<List<AuditoriumFormatVMD>>> GetAllRoomFormatAsync()
        {
            var result = await _roomFormatService.GetAllAsync();
            return result;
        }

        [HttpGet("getRoomFormatById/{id}")]
        public async Task<ApiResult<AuditoriumFormatVMD>> GetRoomFormatByIdAsync(string id)
        {
            var result = await _roomFormatService.GetByIdAsync(id);
            return result;
        }
    }
}