using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Api;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using MovieTheater.Models.Infra.Seat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    public class RoomController : Controller
    {
        private readonly SeatApiClient _seatApiClient;
        private readonly SeatRowApiClient _seatRowApiClient;
        private readonly AuditoriumApiClient _roomApiClient;

        public RoomController(SeatApiClient seatApiClient, SeatRowApiClient seatRowApiClient,
            AuditoriumApiClient roomApiClient)
        {
            _seatApiClient = seatApiClient;
            _seatRowApiClient = seatRowApiClient;
            _roomApiClient = roomApiClient;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index(string keyword, string formatId, int pageIndex = 1, int pageSize = 15)
        {
            var request = new AuditoriumPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                FormatId = formatId,
            };

            var formats = new List<SelectListItem>();
            formats.Add(new SelectListItem() { Text = "Tất cả", Value = "" });
            formats.AddRange((await _roomApiClient.GetAllRoomFormatAsync()).ResultObj.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (formatId != null) && (formatId == x.Id)
            }).OrderBy(x => x.Text));
            ViewBag.RoomFormats = formats;

            ViewBag.SuccessMsg = TempData["Result"];
            ViewBag.KeyWord = keyword;
            var result = await _roomApiClient.GetPagingAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            return View(result.ResultObj);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await SetViewBagAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(AuditoriumCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                await SetViewBagAsync();
                return View(request);
            }

            var result = await _roomApiClient.CreateAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("Index", "Room");
            }

            await SetViewBagAsync();
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _roomApiClient.GetByIdAsync(id);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            if (result.IsSuccessed)
            {
                var updateRequest = new AuditoriumUpdateRequest()
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(AuditoriumUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsEdit = true;
                await SetViewBagAsync();
                return View(request);
            }

            var result = await _roomApiClient.UpdateAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("Index", "Room");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<bool>> Delete(string id)
        {
            var result = await _roomApiClient.DeleteAsync(id);

            TempData["Result"] = result.Message;
            return result;
        }

        [HttpGet]
        public async Task<List<SeatVMD>> GetSeatInRoom(string roomId)
        {
            var seats = (await _seatApiClient.GetSeatInRoomAsync(roomId)).ResultObj;
            return seats;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UpdateSeatInRoom([FromQuery] string roomId)
        {
            ViewBag.AuditoriumId = roomId;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateSeatInRoom(SeatsInRoomUpdateRequest request)
        {
            var result = await _seatApiClient.UpdateSeatInRoomAsync(request);

            TempData["Result"] = result.Message;
            return result;
        }

        [Authorize(Roles = "Admin")]
        private async Task SetViewBagAsync()
        {
            var roomFormats = (await _roomApiClient.GetAllRoomFormatAsync()).ResultObj;

            ViewBag.RoomFormats = roomFormats.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }

        [Authorize(Roles = "Admin,Employee")]
        public async Task<ApiResult<RoomCoordinate>> GetCoordinate(string id)
        {
            var result = await _roomApiClient.GetCoordinateAsync(id);
            return result;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RoomFormat()
        {
            var result = await _roomApiClient.GetAllRoomFormatAsync();
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            ViewBag.SuccessMsg = TempData["Result"];
            return View(result.ResultObj);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FormatEdit(int id)
        {
            var result = await _roomApiClient.GetRoomFormatByIdAsync(id);
            var roomformat = new AuditoriumFormatUpdateRequest()
            {
                Id = result.ResultObj.Id,
                Name = result.ResultObj.Name,
                Price = result.ResultObj.Price
            };
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            return View(roomformat);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> FormatEdit(AuditoriumFormatUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsEdit = true;
                //await SetViewBagAsync();
                return View(request);
            }

            var result = await _roomApiClient.UpdateRoomFormatAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("RoomFormat", "Room");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> FormatCreate()
        {
            //await SetViewBagAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> FormatCreate(AuditoriumFormatCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                await SetViewBagAsync();
                return View(request);
            }

            var result = await _roomApiClient.CreateRoomFormatAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("RoomFormat", "Room");
            }

            await SetViewBagAsync();
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteRoomFormat(int id)
        {
            var result = await _roomApiClient.DeleteRoomFormatAsync(id);

            TempData["Result"] = result.Message;
            return result;
        }

        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> KindOfSeat()
        //{
        //    var result = await _seatApiClient.Get;
        //    if (result.IsReLogin == true)
        //        return RedirectToAction("Index", "Login");
        //    return View(result.ResultObj);
        //}
    }
}