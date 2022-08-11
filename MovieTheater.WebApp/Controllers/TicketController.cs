using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerType = MovieTheater.Common.Constants.CustomerType;
using ReservationType = MovieTheater.Common.Constants.ReservationType;

namespace MovieTheater.WebApp.Controllers
{
    public class TicketController : BaseController
    {
        private readonly TicketApiClient _ticketApiClient;
        private readonly ReservationApiClient _reservationApiClient;

        public TicketController(ReservationApiClient reservationApiClient,
            TicketApiClient ticketApiClient)
        {
            _reservationApiClient = reservationApiClient;
            _ticketApiClient = ticketApiClient;
        }

        [AllowAnonymous]
        public Task<IActionResult> ChooseSeat(int id)
        {
            if (User.Identity!.IsAuthenticated == false)
            {
                return Task.FromResult<IActionResult>(Redirect($"/login/Index?RedirectURL=/Ticket/ChooseSeat/{id}"));
            }

            var reservation = _reservationApiClient.CreateAsync(new ReservationCreateRequest()
            {
                CustomerId = GetUserId(),
                ScreeningId = id,
                Paid = PaymentStatusType.None,
                Active = true,
                EmployeeId = null,
                ReservationTypeId = ReservationType.Online
            }).Result;

            return Task.FromResult<IActionResult>(Redirect($"/Reservation/Create/{reservation.ResultObj}"));
        }

        [Authorize]
        [HttpPost]
        public async Task<decimal> CalPrePrice(List<TicketCreateRequest> tickets)
        {
            if (tickets == null)
                return 0;
            tickets.ForEach(x => x.CustomerType = CustomerType.Adult);
            return (await _reservationApiClient.CalPrePriceAsync(tickets)).ResultObj;
        }

        [Authorize()]
        [HttpPost]
        public async Task<ApiResult<bool>> Create(TicketCreateRequest ticket)
        {
            ticket.CustomerType = CustomerType.Adult;
            return await _ticketApiClient.CreateAsync(ticket);
        }

        [Authorize]
        [HttpPost]
        public async Task<ApiResult<bool>> Delete(TicketCreateRequest ticket)
        {
            ticket.CustomerType = CustomerType.Adult;
            return await _ticketApiClient.DeleteAsync(ticket);
        }
    }
}