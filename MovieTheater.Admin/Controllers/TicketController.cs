using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using CustomerType = MovieTheater.Common.Constants.CustomerType;
using ReservationType = MovieTheater.Common.Constants.ReservationType;

namespace MovieTheater.Admin.Controllers
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

        [Authorize]
        [HttpPost]
        public async Task<decimal> CalPrePrice(List<TicketCreateRequest> tickets)
        {
            if (tickets == null)
                return 0;
            tickets.ForEach(x => x.CustomerType = CustomerType.Adult);
            return (await _reservationApiClient.CalPrePriceAsync(tickets)).ResultObj;
        }

        [Authorize]
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