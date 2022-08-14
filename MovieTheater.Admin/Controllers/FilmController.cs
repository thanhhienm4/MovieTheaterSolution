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
        private readonly MovieApiClient _filmApiClient;
        private readonly MovieCensorshipApiClient _banApiClient;
        private readonly PositionApiClient _positionApiClient;
        private readonly PeopleApiClient _peopleApiClient;

        public FilmController(MovieApiClient filmApiClient, MovieCensorshipApiClient banApiClient,
            PositionApiClient positionApiClient, PeopleApiClient peopleApiClient)
        {
            _filmApiClient = filmApiClient;
            _banApiClient = banApiClient;
            _positionApiClient = positionApiClient;
            _peopleApiClient = peopleApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 15)
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
            var result = (await _banApiClient.GetAllAsync());
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
        public async Task<IActionResult> Create(MovieCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var bans = (await _banApiClient.GetAllAsync()).ResultObj;
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
        public async Task<IActionResult> Edit(string id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _filmApiClient.GetByIdAsync(id);
            if (result.IsReLogin == true)
                return RedirectToAction("Index", "Login");

            if (result.IsSuccessed)
            {
                var updateRequest = new MovieUpdateRequest()
                {
                    Id = result.ResultObj.Id,
                    Name = result.ResultObj.Name,
                    PublishDate = result.ResultObj.PublishDate,
                    CensorshipId = result.ResultObj.Censorship,
                    Description = result.ResultObj.Description,
                    TrailerURL = result.ResultObj.TrailerURL,
                    Length = result.ResultObj.Length
                };
                var bans = (await _banApiClient.GetAllAsync()).ResultObj;
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
        public async Task<IActionResult> Edit([FromForm] MovieUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsEdit = true;
                var bans = (await _banApiClient.GetAllAsync()).ResultObj;
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
        public async Task<ApiResult<bool>> Delete(string id)
        {
            var result = await _filmApiClient.DeleteAsync(id);

            TempData["Result"] = result.Message;
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> AssignGenre(string id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var genreAssignRequest = await GetGenreAssignRequest(id);

            return View(genreAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> AssignGenre(GenreAssignRequest request)
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
            var genreAssignRequest = await GetGenreAssignRequest(request.MovieId);
            return View(genreAssignRequest);
        }

        private async Task<GenreAssignRequest> GetGenreAssignRequest(string id)
        {
            var filmObject = await _filmApiClient.GetMovieGenreByMovieAsync(id);
            var result = (await _filmApiClient.GetAllFilmGenreAsync());

            var genreAssignRequest = new GenreAssignRequest();
            genreAssignRequest.MovieId = id;
            foreach (var genre in result.ResultObj)
            {
                genreAssignRequest.Genres.Add(new SelectedItem()
                {
                    Id = genre.Id.ToString(),
                    Name = genre.Name,
                    Selected = filmObject.ResultObj.Exists(x => x.Id == genre.Id)
                });
            }

            return genreAssignRequest;
        }

       
    }
}