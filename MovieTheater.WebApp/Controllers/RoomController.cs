using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Api;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.Seat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class RoomController : Controller
    {
        private readonly SeatApiClient _seatApiCient;
        private readonly SeatRowApiClient _seatRowApiClient;
        private readonly AuditoriumApiClient _roomApiClient;

        public RoomController(SeatApiClient seatApiClient, SeatRowApiClient seatRowApiClient,
            AuditoriumApiClient roomApiClient)
        {
            _seatApiCient = seatApiClient;
            _seatRowApiClient = seatRowApiClient;
            _roomApiClient = roomApiClient;
        }

        [HttpGet]
        public async Task<List<SeatVMD>> GetSeatInRoom(string roomId)
        {
            var seats = (await _seatApiCient.GetSeatInRoomAsync(roomId)).ResultObj;
            return seats;
        }

        [HttpGet]
        public IActionResult UpdateSeatInRoom(int roomId)
        {
            ViewBag.AuditoriumId = roomId;
            return View();
        }

        [HttpPost]
        public async Task<ApiResult<bool>> UpdateSeatInRoom(SeatsInRoomUpdateRequest request)
        {
            var result = await _seatApiCient.UpdateSeatInRoomAsync(request);
            if (result.IsSuccessed == true)
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

        public async Task<ApiResult<RoomCoordinate>> GetCoordinate(string id)
        {
            var result = await _roomApiClient.GetCoordinateAsync(id);
            return result;
        }
    }
}