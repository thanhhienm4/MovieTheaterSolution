using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Infra.Seat.SeatRow;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    [AllowAnonymous]
    public class SeatRowController : Controller
    {
        private readonly SeatRowApiClient _seatRowApiClient;

        public SeatRowController(SeatRowApiClient seatRowApiClient)
        {
            _seatRowApiClient = seatRowApiClient;
        }

        [HttpGet]
        public async Task<List<SeatRowVMD>> GetAllSeatRows()
        {
            var result = (await _seatRowApiClient.GetAllSeatRowsAsync()).ResultObj;
            return result;
        }
    }
}