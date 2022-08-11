using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.SeatServices.SeatRows;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.Seat.SeatRow;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SeatRowController : BaseController
    {
        private readonly IPriceService _seatRowService;

        public SeatRowController(IPriceService seatRowService, IUserService userService) : base(userService)
        {
            _seatRowService = seatRowService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync(SeatRowCreateRequest request)
        {
            var result = await _seatRowService.CreateAsync(request);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(SeatRowUpdateRequest request)
        {
            var result = await _seatRowService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _seatRowService.DeleteAsync(id);
            return result;
        }

        [AllowAnonymous]
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
        public async Task<ApiResult<bool>> Delete(int id)
        {
            var result = await _seatRowService.DeleteAsync(id);

            return result;
        }
    }
}