using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PeopleController : Controller
    {
        private readonly PeopleApiClient _peopleApiClient;

        public PeopleController(PeopleApiClient peopleApiClient)
        {
            _peopleApiClient = peopleApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 15)
        {
            var request = new ActorPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            ViewBag.SuccessMsg = TempData["Result"];
            ViewBag.KeyWord = keyword;
            var result = await _peopleApiClient.GetPeoplePagingAsync(request);
            if (result.IsReLogin)
                return RedirectToAction("Index", "Login");

            return View(result.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActorCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _peopleApiClient.CreateAsync(request);
            if (result.IsReLogin)
                return RedirectToAction("Index", "Login");
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

            var result = await _peopleApiClient.GetPeopleByIdAsync(id);
            if (result.IsReLogin)
                return RedirectToAction("Index", "Login");

            if (result.IsSuccessed)
            {
                var updateRequest = new ActorUpdateRequest()
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
        public async Task<IActionResult> Edit(ActorUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsEdit = true;
                return View(request);
            }

            var result = await _peopleApiClient.UpdateAsync(request);
            if (result.IsReLogin)
                return RedirectToAction("Index", "Login");
            if (result.IsSuccessed)
            {
                TempData["Result"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index", "People");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<ApiResult<bool>> Delete(int id)
        {
            var result = await _peopleApiClient.DeleteAsync(id);
            TempData["Result"] = result.Message;
            return result;
        }
    }
}