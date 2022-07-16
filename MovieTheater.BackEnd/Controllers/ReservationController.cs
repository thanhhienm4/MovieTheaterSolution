using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.ReservationServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Application.ReservationServices.Reservations;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : BaseController
    {
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;

        public ReservationController(IReservationService reservationService,IUserService userService):base(userService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<int>> CreateAsync(ReservationCreateRequest model)
        {
            var result = await _reservationService.CreateAsync(model);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(ReservationUpdateRequest request)
        {
            var result = await _reservationService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _reservationService.DeleteAsync(id);
            return result;
        }

      
        [HttpPost("GetReservationPaging")]
        public async Task<ApiResult<PageResult<ReservationVMD>>> GetPeoplePaging(ReservationPagingRequest request)
        {
            var result = await _reservationService.GetPagingAsync(request);
            return result;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ApiResult<ReservationVMD>> GeReservationByIdAsync(int id)
        {
            var result = await _reservationService.GetById(id);
            return result;
        }

        [HttpGet("GetByUserId/{id}")]
        public async Task<ApiResult<List<ReservationVMD>>> GeReservationByUserIdAsync(Guid id)
        {
            var result = await _reservationService.GetByUserId(id);
            return result;
        }

        [HttpPost("CalPrePrice")]
        public async Task<ApiResult<int>> CalPrePriceAsync(List<TicketCreateRequest> tickets)
        {
            var result = await _reservationService.CalPrePriceAsync(tickets);
            return result;
        }
    }
}