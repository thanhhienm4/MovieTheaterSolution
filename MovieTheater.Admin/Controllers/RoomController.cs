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

namespace MovieTheater.Admin.Controllers
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
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {

            var request = new RoomPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,

            };

            

            ViewBag.SuccessMsg = TempData["Result"];
            ViewBag.KeyWord = keyword;
            var result = await _roomApiClient.GetRoomPagingAsync(request);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await SetViewBagAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                await SetViewBagAsync();
                return View(request);
            }
          
            var result = await _roomApiClient.CreateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("Index", "Room");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
               
                return View();
            }
            var result = await _roomApiClient.GetRoomByIdAsync(id);

            if (result.IsSuccessed)
            {
                var updateRequest = new RoomUpdateRequest()
                {
                    Id = result.ResultObj.Id,
                    Name = result.ResultObj.Name,
                    FormatId = result.ResultObj.FormatId

                };
                await SetViewBagAsync();
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoomUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                await SetViewBagAsync();
                return View(request);
            }
            var result = await _roomApiClient.UpdateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("Index", "Room");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<ApiResultLite> Delete(int id)
        {
            var result = await _roomApiClient.DeleteAsync(id);
            TempData["Result"] = result.Message;
            return result;
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
