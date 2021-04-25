using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class ScreeningController : Controller
    {
        private readonly SeatApiClient _seatApiClient;
        private readonly ScreeningApiClient _screeningApiClient;
        private readonly RoomApiClient _roomApiClient;
        private readonly FilmApiClient _filmApiClient;
        public ScreeningController(SeatApiClient seatApiClient, ScreeningApiClient screeningApiClient, 
            RoomApiClient roomApiClient, FilmApiClient filmApiClient)
        {
            _seatApiClient = seatApiClient;
            _screeningApiClient = screeningApiClient;
            _roomApiClient = roomApiClient;
            _filmApiClient = filmApiClient;

        }
        [HttpGet]
        public async Task<List<SeatVMD>> GetListSeatReserved(int id)
        {
            var result = (await _seatApiClient.GetListSeatReserved(id)).ResultObj;
            return result;
        }
        

    }
}
