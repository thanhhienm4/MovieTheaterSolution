using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Api;
using MovieTheater.Data.Results;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
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
        private readonly AuditoriumApiClient _roomApiClient;
        private readonly MovieApiClient _filmApiClient;

        public ScreeningController(SeatApiClient seatApiClient, ScreeningApiClient screeningApiClient,
            AuditoriumApiClient roomApiClient, MovieApiClient filmApiClient)
        {
            _seatApiClient = seatApiClient;
            _screeningApiClient = screeningApiClient;
            _roomApiClient = roomApiClient;
            _filmApiClient = filmApiClient;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<SeatModel>> GetListSeatReserved(int id)
        {
            var result = (await _seatApiClient.GetListSeatReserved(id)).ResultObj;
            return result;
        }

        public async Task<IActionResult> Index(string keyword, DateTime? date = null, int pageIndex = 1,
            int pageSize = 15)
        {
            var request = new ScreeningPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Date = date
            };

            ViewBag.Date = date;
            ViewBag.KeyWord = keyword;
            ViewBag.SuccessMsg = TempData["Result"];
            var result = (await _screeningApiClient.GetPagingAsync(request));
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            return View(result.ResultObj);
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
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
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
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            if (result.IsSuccessed)
            {
                var updateRequest = new ScreeningUpdateRequest()
                {
                    Id = result.ResultObj.Id,
                    FilmId = result.ResultObj.MovieId,
                    KindOfScreeningId = result.ResultObj.KindOfScreeningId,
                    AuditoriumId = result.ResultObj.AuditoriumId,
                    StartTime = result.ResultObj.StartTime
                };
                ViewBag.Date = result.ResultObj.StartTime;
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
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("Index", "Screening");
            }

            ViewBag.IsEdit = true;
            await SetViewBagAsync();
            ViewBag.Date = request.StartTime;
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<ApiResult<bool>> Delete(int id)
        {
            var result = await _screeningApiClient.DeleteAsync(id);
            TempData["Result"] = result.Message;
            return result;
        }

        private async Task SetViewBagAsync()
        {
            var rooms = (await _roomApiClient.GetAllAsync()).ResultObj;
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
        }
    }
}