using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.RoomServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost("Create")]
        public async Task<ApiResultLite> CreateAsync(RoomCreateRequest model)
        {
            var result = await _roomService.CreateAsync(model);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ApiResultLite> UpdateAsync(RoomUpdateRequest request)
        {
            var result = await _roomService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            var result = await _roomService.DeleteAsync(id);
            return result;
        }

        [HttpPost("GetRoomPaging")]
        public async Task<PageResult<RoomVMD>> GetRoomPagingAsync(RoomPagingRequest request)
        {
            var result = await _roomService.GetRoomPagingAsync(request);
            return result;
        }

        [HttpGet("GetRoomById/{id}")]
        public async Task<ApiResult<RoomVMD>> GetRoomByIdAsync(int id)
        {
            var result = await _roomService.GetRoomById(id);
            return result;
        }

    }
    
}
