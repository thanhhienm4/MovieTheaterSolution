using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.FilmServices.Actors;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Application.ReservationServices.Tickets;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Reservation;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;

        public TicketController(ITicketService ticketService, IUserService userService) : base(userService)
        {
            _ticketService = ticketService;
            _userService = userService;
        }

        [HttpPost(APIConstant.TicketCreate)]
        public async Task<ApiResult<bool>> CreateAsync(TicketCreateRequest request)
        {
            var result = await _ticketService.CreateAsync(request);
            return result;
        }

        [HttpPut(APIConstant.TicketUpdate)]
        public async Task<ApiResult<bool>> UpdateAsync(TicketUpdateRequest request)
        {
            var result = await _ticketService.UpdateAsync(request);
            return result;
        }

        [HttpPost(APIConstant.TicketDelete)]
        public async Task<ApiResult<bool>> DeleteAsync(TicketCreateRequest request)
        {
            var result = await _ticketService.DeleteAsync(request);
            return result;
        }
    }
}