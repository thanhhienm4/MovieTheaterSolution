using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Data.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class ScreeningController : Controller
    {
        private readonly SeatApiClient _seatApiClient;

        public ScreeningController(SeatApiClient seatApiClient)
        {
            _seatApiClient = seatApiClient;
        }

        [HttpGet]
        public async Task<List<SeatModel>> GetListSeatReserved(int id)
        {
            var result = (await _seatApiClient.GetListSeatReserved(id)).ResultObj;
            return result;
        }
    }
}