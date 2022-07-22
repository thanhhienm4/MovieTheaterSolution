using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Reservation;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;
using MovieTheater.Data.Models;
using ReservationType = MovieTheater.Common.Constants.ReservationType;

namespace MovieTheater.WebApp.Controllers
{
    public class TicketController : BaseController
    {
        private readonly ScreeningApiClient _screeningApiClient;
        private readonly MovieApiClient _filmApiClient;
        private readonly ReservationApiClient _reservationApiClient;

        public TicketController(ScreeningApiClient screeningApiClient, ReservationApiClient reservationApiClient,
            MovieApiClient filmApiClient)
        {
            _screeningApiClient = screeningApiClient;
            _reservationApiClient = reservationApiClient;
            _filmApiClient = filmApiClient;
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
        public async Task<int> CalPrePrice(List<TicketCreateRequest> tickets)
        {
            if (tickets == null)
                return 0;
            return (await _reservationApiClient.CalPrePriceAsync(tickets)).ResultObj;
        }
    }
}