using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MovieTheater.Application.ReservationServices.Tickets;
using MovieTheater.Application.UserServices;
using MovieTheater.BackEnd.Hub;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;
        private readonly IHubContext<ReservationHub> _hubContext;

        public TicketController(ITicketService ticketService, IHubContext<ReservationHub> hubContext,
            IUserService userService) : base(userService)
        {
            _ticketService = ticketService;
            _userService = userService;
            _hubContext = hubContext;
        }

        [HttpPost(ApiConstant.TicketCreate)]
        public async Task<ApiResult<bool>> CreateAsync(TicketCreateRequest request)
        {
            var result = await _ticketService.CreateAsync(request);
            if (result.IsSuccessed)
                await _hubContext.Clients.Group(request.ScreeningId.ToString()).SendAsync("Disable", request.SeatId);
            return result;
        }

        [HttpPut(ApiConstant.TicketUpdate)]
        public async Task<ApiResult<bool>> UpdateAsync(TicketUpdateRequest request)
        {
            var result = await _ticketService.UpdateAsync(request);
            return result;
        }

        [HttpPost(ApiConstant.TicketDelete)]
        public async Task<ApiResult<bool>> DeleteAsync(TicketCreateRequest request)
        {
            var result = await _ticketService.DeleteAsync(request);
            await _hubContext.Clients.Group(request.ScreeningId.ToString()).SendAsync("Enable", request.SeatId);
            return result;
        }
    }
}