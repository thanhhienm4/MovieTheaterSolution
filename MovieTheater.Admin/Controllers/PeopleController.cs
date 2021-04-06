using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleApiClient _PeopleApiClient;
        public PeopleController(PeopleApiClient PeopleApiClient)
        {
            _PeopleApiClient = PeopleApiClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new PeoplePagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,

            };

            ViewBag.KeyWord = keyword;
            var result = await _PeopleApiClient.GetPeoplePagingAsync(request);
            return View(result.ResultObj);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PeopleCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _PeopleApiClient.CreateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Tạo mới thành công";
                return RedirectToAction("Index", "People");
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
            var result = await _PeopleApiClient.GetPeopleByIdAsync(id);

            if (result.IsSuccessed)
            {
                var updateRequest = new PeopleUpdateRequest()
                {
                    Id = result.ResultObj.Id,
                    DOB = result.ResultObj.DOB,
                    Description = result.ResultObj.Description,
                    Name = result.ResultObj.Name
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PeopleUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _PeopleApiClient.UpdateAsync(request);
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index", "People");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

    }
}
