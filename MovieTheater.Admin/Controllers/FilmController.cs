using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FilmController : Controller
    {
        private readonly FilmApiClient _filmApiClient;
        private readonly BanApiClient _banApiClient;
        private readonly PositionApiClient _positionApiClient;
        private readonly PeopleApiClient _peopleApiClient;

        public FilmController(FilmApiClient filmApiClient, BanApiClient banApiClient,
            PositionApiClient positionApiClient, PeopleApiClient peopleApiClient)
        {
            _filmApiClient = filmApiClient;
            _banApiClient = banApiClient;
            _positionApiClient = positionApiClient;
            _peopleApiClient = peopleApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new FilmPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            ViewBag.SuccessMsg = TempData["Result"];

            ViewBag.KeyWord = keyword;
            var result = await _filmApiClient.GetFilmPagingAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            return View(result.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var result = (await _banApiClient.GetAllBanAsync());
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");


            ViewBag.Bans = result.ResultObj.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FilmCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var bans = (await _banApiClient.GetAllBanAsync()).ResultObj;
                ViewBag.Bans = bans.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
                return View(request);
            }
            var result = await _filmApiClient.CreateAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = result.Message;
                return RedirectToAction("Index", "Film");
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
            var result = await _filmApiClient.GetFilmMDByIdAsync(id);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            if (result.IsSuccessed)
            {
                var updateRequest = new FilmUpdateRequest()
                {
                    Id = result.ResultObj.Id,
                    Name = result.ResultObj.Name,
                    PublishDate = result.ResultObj.PublishDate,
                    BanId = result.ResultObj.BanId,
                    Description = result.ResultObj.Description,
                    TrailerURL = result.ResultObj.TrailerURL,
                    Length = result.ResultObj.Length
                };
                var bans = (await _banApiClient.GetAllBanAsync()).ResultObj;
                ViewBag.Bans = bans.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] FilmUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsEdit = true;
                var bans = (await _banApiClient.GetAllBanAsync()).ResultObj;
                ViewBag.Bans = bans.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
                return View(request);
            }
            var result = await _filmApiClient.UpdateAsync(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index", "Film");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<ApiResult<bool>> Delete(int id)
        {
            var result = await _filmApiClient.DeleteAsync(id);

            TempData["Result"] = result.Message;
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> AssignGenre(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var genreAssignRequest = await GetGenreAssignRequest(id);

            return View(genreAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> GenreAssign(GenreAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _filmApiClient.AssignGenre(request);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Gán danh mục thành công";
                return RedirectToAction("Index", "Film");
            }
            ModelState.AddModelError("", result.Message);
            var genreAssignRequest = await GetGenreAssignRequest(request.FilmId);
            return View(genreAssignRequest);
        }

        private async Task<GenreAssignRequest> GetGenreAssignRequest(int id)
        {
            var filmObject = await _filmApiClient.GetFilmVMDByIdAsync(id);
            var result = (await _filmApiClient.GetAllFilmGenreAsync());


            var genreAssignRequest = new GenreAssignRequest();
            genreAssignRequest.FilmId = id;
            foreach (var genre in result.ResultObj)
            {
                genreAssignRequest.Genres.Add(new SelectedItem()
                {
                    Id = genre.Id.ToString(),
                    Name = genre.Name,
                    Selected = filmObject.ResultObj.Genres.Contains(genre.Name)
                });
            }

            return genreAssignRequest;
        }

        [HttpPost]
        public async Task<ApiResult<bool>> PosAssign(PosAssignRequest request)
        {
            var res = (await _filmApiClient.PosAssignAsync(request));
            return res;
        }

        [HttpPost]
        public async Task<ApiResult<bool>> DeletePosAssign(PosAssignRequest request)
        {
            var res = (await _filmApiClient.DeletePosAssignAsync(request));
            return res;
        }

        [HttpGet]
        public async Task<IActionResult> PosAssign(int id)
        {
            var film = (await _filmApiClient.GetFilmVMDByIdAsync(id)).ResultObj;

            await SetViewBagForPosAssignAsync();
            return View(film);
        }

        [HttpGet]
        public async Task<List<JoiningPosVMD>> GetJoining(int id)
        {
            var res = (await _filmApiClient.GetJoiningAsync(id)).ResultObj;
            return res;
        }

        private async Task SetViewBagForPosAssignAsync()
        {
            var peoples = (await _peopleApiClient.GetAllPeopleAsync()).ResultObj;
            ViewBag.Peoples = peoples.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            var positions = (await _positionApiClient.GetAllPositionAsync()).ResultObj;
            ViewBag.Positions = positions.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }
    }
}