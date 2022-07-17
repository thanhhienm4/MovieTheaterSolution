using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTheater.Application.SeatServices.SeatTypes;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class KindOfSeatController : Controller
    {
        private readonly ISeatTypeService _kindOfSeatService;

        public KindOfSeatController(ISeatTypeService kindOfSeatService)
        {
            _kindOfSeatService = kindOfSeatService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync(SeatTypeCreateRequest request)
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
        public async Task<ApiResult<List<SeatTypeVMD>>> GetAllAsync()
        {
            var res = await _kindOfSeatService.GetAllAsync();
            return res;
        }

       
    }
}