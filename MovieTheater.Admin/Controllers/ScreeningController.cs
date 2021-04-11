using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    public class ScreeningController : Controller
    {
        private readonly SeatApiClient _seatApiClient;
        public ScreeningController(SeatApiClient seatApiClient)
        {
            _seatApiClient = seatApiClient;

        }
        [HttpGet]
        public async Task<List<SeatVMD>> GetListSeatReserved(int id)
        {
            var result = (await _seatApiClient.GetListSeatReserved(id)).ResultObj;
            return result;
        }
    }
}
