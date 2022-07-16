using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.RoomServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Application.RoomServices.Auditoriums;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoomController : BaseController
    {
        private readonly IAuditoriumService _roomService;
        private readonly IUserService _userService;

        public RoomController(IAuditoriumService roomService, IUserService userService) : base(userService)
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
        public async Task<ApiResult<bool>> UpdateAsync(AuditoriumUpdateRequest request)
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
            var result = await _roomService.GetPagingAsync(request);
            return result;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ApiResult<RoomMD>> GetRoomByIdAsync(string id)
        {
            var result = await _roomService.GetById(id);
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
            var result = await _roomService.GetAllAsync();
            return result;
        }

        [AllowAnonymous]
        [HttpGet("GetCoordinate/{id}")]
        public async Task<ApiResult<RoomCoordinate>> GetCoordinateAsync(string id)
        {
            var result = await _roomService.GetCoordinateAsync(id);
            return result;
        }
    }
}