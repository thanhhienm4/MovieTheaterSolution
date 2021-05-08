using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> Index(string keyword, DateTime? date, int pageIndex = 1, int pageSize = 10)
        {

            var request = new ScreeningPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Date = date

            };
            if (date == null)
                date = DateTime.Now.Date;

            ViewBag.Date = date;
            ViewBag.KeyWord = keyword;
            ViewBag.SuccessMsg = TempData["Result"];
            var result = (await _screeningApiClient.GetScreeningPagingAsync(request)).ResultObj;
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            await SetViewBagAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScreeningCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                await SetViewBagAsync();
                return View(request);
            }

            var result = await _screeningApiClient.CreateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("Index", "Screening");
            }

            await SetViewBagAsync();
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
            var result = await _screeningApiClient.GetScreeningMDByIdAsync(id);

            if (result.IsSuccessed)
            {
                var updateRequest = new ScreeningUpdateRequest()
                {
                    Id = result.ResultObj.Id,
                    FilmId = result.ResultObj.FilmId,
                    KindOfScreeningId = result.ResultObj.KindOfScreeningId,
                    RoomId = result.ResultObj.RoomId,
                    TimeStart = result.ResultObj.TimeStart

                };
                ViewBag.Date = result.ResultObj.TimeStart;
                await SetViewBagAsync();
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ScreeningUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsEdit = true;
                await SetViewBagAsync();
                return View(request);
            }
            var result = await _screeningApiClient.UpdateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("Index", "Screening");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

       
        [HttpPost]
        public async Task<ApiResultLite> Delete(int id)
        {

            var result = await _screeningApiClient.DeleteAsync(id);
            TempData["Result"] = result.Message;
            return result;
        }
        private async Task SetViewBagAsync()
        {
            var rooms = (await _roomApiClient.GetAllRoomAsync()).ResultObj;
            ViewBag.Rooms = rooms.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            var films = (await _filmApiClient.GetAllFilmAsync()).ResultObj;
            ViewBag.Films = films.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            var kindOfScreenings = (await _screeningApiClient.GetAllKindOfScreeningAsync()).ResultObj;
            ViewBag.KindOfScreenings = kindOfScreenings.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }

    }
}
