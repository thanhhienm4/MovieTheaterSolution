using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.SeatServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : Controller
    {
        private readonly ISeatService _seatService;
        public SeatController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpPost("Create")]
        public async Task<ApiResultLite> CreateAsync(SeatCreateRequest model)
        {
            var result = await _seatService.CreateAsync(model);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ApiResultLite> UpdateAsync(SeatUpdateRequest request)
        {
            var result = await _seatService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            var result = await _seatService.DeleteAsync(id);
            return result;
        }

        
    }
}
