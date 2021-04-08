using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    public class RoomController : Controller
    {
        private readonly SeatApiClient _seatApiCient;
        private readonly SeatRowApiClient _seatRowApiClient;

        public RoomController(SeatApiClient seatApiClient,SeatRowApiClient seatRowApiClient)
        {
            _seatApiCient = seatApiClient;
            _seatRowApiClient = seatRowApiClient;
        }
        [HttpGet]
        public async Task<List<SeatVMD>> GetSeatInRoom(int roomId)
        {
            var seats = (await _seatApiCient.GetSeatInRoomAsync(roomId)).ResultObj;
            return seats;

        }
        [HttpGet]
        public IActionResult UpdateSeatInRoom(int roomId)
        {
            ViewBag.RoomId = roomId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSeatInRoom(SeatsInRoomUpdateRequest request)
        {
            var seatMatrix = await _seatApiCient.UpdateSeatInRoomAsync(request);
            return View(seatMatrix);
        }
    }
}
