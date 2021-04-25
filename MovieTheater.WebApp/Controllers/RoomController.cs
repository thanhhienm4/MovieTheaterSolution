using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Api;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class RoomController : Controller
    {
        private readonly SeatApiClient _seatApiCient;
        private readonly SeatRowApiClient _seatRowApiClient;
        private readonly RoomApiClient _roomApiClient;

        public RoomController(SeatApiClient seatApiClient,SeatRowApiClient seatRowApiClient, RoomApiClient roomApiClient)
        {
            _seatApiCient = seatApiClient;
            _seatRowApiClient = seatRowApiClient;
            _roomApiClient = roomApiClient;
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
        public async Task<ApiResultLite> UpdateSeatInRoom(SeatsInRoomUpdateRequest request)
        {
            var result = await _seatApiCient.UpdateSeatInRoomAsync(request);
            if(result.IsSuccessed == true)
            {
                TempData["Result"] = result.Message;
               
            }
            return result;
        }

        private async Task SetViewBagAsync()
        {
            var roomFormats = (await _roomApiClient.GetAllRoomFormatAsync()).ResultObj;

            ViewBag.RoomFormats = roomFormats.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }
    }
}
