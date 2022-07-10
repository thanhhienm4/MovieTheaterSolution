using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.SeatServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class KindOfSeatController : Controller
    {
        private readonly IKindOfSeatService _kindOfSeatService;
        public KindOfSeatController(IKindOfSeatService kindOfSeatService)
        {
            _kindOfSeatService = kindOfSeatService;
        }
        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync(KindOfSeatCreateRequest request)
        {
            var result = await _kindOfSeatService.CreateAsync(request);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(KindOfSeatUpdateRequest request)
        {
            var result = await _kindOfSeatService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _kindOfSeatService.DeleteAsync(id);
            return result;
        }

        [HttpGet("GetAllKindOfSeat")]
        public async Task<ApiResult<List<KindOfSeatVMD>>> GetAllAsync()
        {
            var res = await _kindOfSeatService.GetAllAsync();
            return res;
        }
      
        [HttpGet("GetKindOfSeatById/{id}")]
        public async Task<ApiResult<KindOfSeatVMD>> GetKindOfSeatByIdAsync(int id)
        {
            var res = await _kindOfSeatService.GetKindOfSeatByIdAsync(id);
            return res;
        }
    }
}
