using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.RoomServices;
using Movietheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoomController : BaseController
    {
        private readonly IRoomService _roomService;
        private readonly IUserService _userService;

        public RoomController(IRoomService roomService, IUserService userService) : base(userService)
        {
            _roomService = roomService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync(RoomCreateRequest model)
        {
            var result = await _roomService.CreateAsync(model);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(RoomUpdateRequest request)
        {
            var result = await _roomService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _roomService.DeleteAsync(id);
            return result;
        }

        [HttpPost("GetRoomPaging")]
        public async Task<ApiResult<PageResult<RoomVMD>>> GetRoomPagingAsync(RoomPagingRequest request)
        {
            var result = await _roomService.GetRoomPagingAsync(request);
            return result;
        }

        [HttpGet("GetRoomById/{id}")]
        public async Task<ApiResult<RoomMD>> GetRoomByIdAsync(int id)
        {
            var result = await _roomService.GetRoomById(id);
            return result;
        }

        [HttpPost]
        public async Task<ApiResult<bool>> Delete(int id)
        {
            var result = await _roomService.DeleteAsync(id);

            return result;
        }

        [HttpGet("getAllRoom")]
        public async Task<ApiResult<List<RoomVMD>>> GetAllRoomAsync()
        {
            var result = await _roomService.GetAllRoomAsync();
            return result;
        }

        [AllowAnonymous]
        [HttpGet("GetCoordinate/{id}")]
        public async Task<ApiResult<RoomCoordinate>> GetCoordinateAsync(int id)
        {
            var result = await _roomService.GetCoordinateAsync(id);
            return result;
        }
    }
}