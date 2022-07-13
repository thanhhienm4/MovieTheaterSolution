using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.SeatServices;
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

        public SeatController(ISeatService seatService, IUserService customerService) : base(customerService)
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
        [HttpGet("GetSeatInRoomAsync/{id}")]
        public async Task<ApiResult<List<SeatVMD>>> GetSeatInRoomAsync(int id)
        {
            var result = await _seatService.GetSeatInRoomAsync(id);
            return result;
        }

        [HttpPut("UpdateSeatInRoomAsync")]
        public async Task<ApiResult<bool>> UpdateSeatInRoomAsync(SeatsInRoomUpdateRequest request)
        {
            var result = await _seatService.UpdateSeatInRoomAsync(request);
            return result;
        }
        [AllowAnonymous]
        [HttpGet("GetListSeatReserved/{screeningId}")]
        public async Task<ApiResult<List<SeatVMD>>> GetListSeatReserved(int screeningId)
        {
            var result = await _seatService.GetListSeatReserved(screeningId);
            return result;
        }
    }
}