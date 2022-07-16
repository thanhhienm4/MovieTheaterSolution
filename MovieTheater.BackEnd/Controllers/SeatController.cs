using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.SeatServices.Seats;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SeatController : BaseController
    {
        private readonly ISeatService _seatService;
        private readonly IUserService _userService;

        public SeatController(ISeatService seatService, IUserService userService) : base(userService)
        {
            _seatService = seatService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync(SeatCreateRequest model)
        {
            var result = await _seatService.CreateAsync(model);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(SeatUpdateRequest request)
        {
            var result = await _seatService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _seatService.DeleteAsync(id);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("GetAllInRoomAsync/{id}")]
        public async Task<ApiResult<List<SeatVMD>>> GetSeatInRoomAsync(string id)
        {
            var result = await _seatService.GetAllInRoomAsync(id);
            return result;
        }

        [HttpPut("UpdateInRoomAsync")]
        public async Task<ApiResult<bool>> UpdateSeatInRoomAsync(SeatsInRoomUpdateRequest request)
        {
            var result = await _seatService.UpdateInRoomAsync(request);
            return result;
        }
        [AllowAnonymous]
        [HttpGet("GetListReserved/{screeningId}")]
        public async Task<ApiResult<List<SeatVMD>>> GetListSeatReserved(int screeningId)
        {
            var result = await _seatService.GetListReserved(screeningId);
            return result;
        }
    }
}