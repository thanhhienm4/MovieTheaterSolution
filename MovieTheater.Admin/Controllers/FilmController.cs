using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Api;
using MovieTheater.Data.Enums;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    public class FilmController : Controller
    {
        private readonly FilmApiClient _filmApiClient;
        private readonly BanApiClient _banApiClient;
        public FilmController(FilmApiClient filmApiClient, BanApiClient banApiClient) 
        {
            _filmApiClient = filmApiClient;
            _banApiClient = banApiClient;
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
            return View(result.ResultObj);
        }
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var bans = (await _banApiClient.GetAllBanAsync()).ResultObj;
            ViewBag.Bans = bans.Select(x => new SelectListItem()
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
                var bans = (await _banApiClient.GetAllBanAsync()).ResultObj;
                ViewBag.Bans = bans.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
                return View(request);
            }
            var result = await _filmApiClient.UpdateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index", "Film");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }


        [HttpPost]
        public async Task<ApiResultLite> Delete(int id)
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
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Gán quyền thành công";
                return RedirectToAction("Index", "Film");
            }
            ModelState.AddModelError("", result.Message);
            var genreAssignRequest = await GetGenreAssignRequest(request.FilmId);
            return View(genreAssignRequest);
        }

        private async Task<GenreAssignRequest> GetGenreAssignRequest(int id)
        {
            var filmObject = await _filmApiClient.GetFilmVMDByIdAsync(id);
            var result = (await _filmApiClient.GetAllFilmGenreAsync()).ResultObj;
            var genreAssignRequest = new GenreAssignRequest();
            genreAssignRequest.FilmId = id;
            foreach (var genre in result)
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
    }
}
