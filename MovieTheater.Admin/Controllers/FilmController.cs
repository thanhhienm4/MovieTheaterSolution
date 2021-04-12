using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieTheater.Api;
using MovieTheater.Data.Enums;
using MovieTheater.Models.Catalog.Film;
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
                TempData["Result"] = "Tạo mới thành công";
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
            var result = await _filmApiClient.GetFilmByIdAsync(id);

            if (result.IsSuccessed)
            {
                var updateRequest = new FilmUpdateRequest()
                {
                    Id = result.ResultObj.Id,
                    Name = result.ResultObj.Name,
                    PublishDate = result.ResultObj.PublishDate
                    

                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FilmUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
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



        //[HttpPost]
        //public async Task<ApiResultLite> Delete(Guid id)
        //{

        //    var result = await _FilmApiClient.DeleteAsync(id);
        //    if (result.IsSuccessed)
        //    {
        //        TempData["Result"] = result.Message;


        //    }
        //    return result;
        //}
    }
}
