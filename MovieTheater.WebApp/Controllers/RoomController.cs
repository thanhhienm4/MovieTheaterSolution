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
        private readonly SeatApiClient _seatApiClient;
        private readonly AuditoriumApiClient _roomApiClient;

        public RoomController(SeatApiClient seatApiClient,
            AuditoriumApiClient roomApiClient)
        {
            _seatApiClient = seatApiClient;
            _roomApiClient = roomApiClient;
        }

        [HttpGet]
        public async Task<List<SeatVMD>> GetSeatInRoom(string roomId)
        {
            var seats = (await _seatApiClient.GetSeatInRoomAsync(roomId)).ResultObj;
            return seats;
        }

        public async Task<ApiResult<RoomCoordinate>> GetCoordinate(string id)
        {
            var result = await _roomApiClient.GetCoordinateAsync(id);
            return result;
        }
    }
}