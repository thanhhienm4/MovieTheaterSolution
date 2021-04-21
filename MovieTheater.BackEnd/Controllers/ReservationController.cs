using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.ReservationServices;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("Create")]
        public async Task<ApiResultLite> CreateAsync(ReservationCreateRequest model)
        {
            var result = await _reservationService.CreateAsync(model);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResultLite> UpdateAsync(ReservationUpdateRequest request)
        {
            var result = await _reservationService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            var result = await _reservationService.DeleteAsync(id);
            return result;
        }

        [HttpPost("GetReservationPaging")]
        public async Task<ApiResult<PageResult<ReservationVMD>>> GetPeoplePaging(ReservationPagingRequest request)
        {
            var result = await _reservationService.GetReservationPagingAsync(request);
            return result;
        }
        [HttpGet("GetReservationById/{id}")]
        public async Task<ApiResult<ReservationVMD>> GeReservationByIdAsync(int id)
        {
            var result = await _reservationService.GetReservationById(id);
            return result;
        }

        [HttpPost("CalPrePrice")]
        public async Task<int> CalPrePriceAsync(List<TicketCreateRequest> tickets)
        {
            var result = await _reservationService.CalPrePriceAsync(tickets);
            return result;
        }
    }
}
