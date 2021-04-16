using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.SeatServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.Seat;
using MovieTheater.Models.Infra.Seat.SeatRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class SeatRowController : Controller
    {
        private readonly ISeatRowService _seatRowService;

        public SeatRowController(ISeatRowService seatRowService)
        {
            _seatRowService = seatRowService;
        }


        [HttpPost("Create")]
        public async Task<ApiResultLite> CreateAsync(SeatRowCreateRequest request)
        {
            var result = await _seatRowService.CreateAsync(request);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResultLite> UpdateAsync(SeatRowUpdateRequest request)
        {
            var result = await _seatRowService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            var result = await _seatRowService.DeleteAsync(id);
            return result;
        }

        [HttpGet("GetAllSeatRows")]
        public async Task<ApiResult<List<SeatRowVMD>>> GetAllSeatRows()
        {
            var result = await _seatRowService.GetAllSeatRows();
            return result;
        }

        [HttpPost("GetSeatRowPaging")]
        public async Task<ApiResult<PageResult<SeatRowVMD>>> GetSeatRowPaging(SeatRowPagingRequest request)
        {
            var result = await _seatRowService.GetSeatRowPagingAsync(request);
            return result;
        }
        [HttpGet("GetSeatRowById/{id}")]
        public async Task<ApiResult<SeatRowVMD>> GetSeatRowByIdAsync(int id)
        {
            var result = await _seatRowService.GetSeatRowById(id);
            return result;
        }
        [HttpPost]
        public async Task<ApiResultLite> Delete(int id)
        {

            var result = await _seatRowService.DeleteAsync(id);

            return result;
        }
    }
}
