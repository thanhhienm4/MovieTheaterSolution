using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
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

            //List<SelectListItem> roles = new List<SelectListItem>();
            //roles.Add(new SelectListItem() { Text = "Tất cả", Value = "" });
            //var listRoles = (await _roleApiClient.GetRolesAsync())
            //    .Select(x => new SelectListItem()
            //    {
            //        Text = x.Name,
            //        Value = x.Id.ToString(),
            //        Selected = (!string.IsNullOrWhiteSpace(roleId)) && roleId == x.Id.ToString()
            //    }).ToList().OrderBy(x => x.Text);

            //roles.AddRange(listRoles);
            //ViewBag.Roles = roles;


            ViewBag.KeyWord = keyword;
            var result = await _roomApiClient.GetRoomPagingAsync(request);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoomCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _roomApiClient.CreateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Tạo mới thành công";
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
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoomUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _roomApiClient.UpdateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index", "Room");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
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
