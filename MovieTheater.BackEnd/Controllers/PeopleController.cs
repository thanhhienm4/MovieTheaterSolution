using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.FilmServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PeopleController : BaseController
    {
        private readonly IPeopleService _PeopleService;
        private readonly IUserService _userService;
        public PeopleController(IPeopleService PeopleService, IUserService userService) : base(userService)
        {
            _PeopleService = PeopleService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync(PeopleCreateRequest request)
        {
            var result = await _PeopleService.CreateAsync(request);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(PeopleUpdateRequest request)
        {
            var result = await _PeopleService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _PeopleService.DeleteAsync(id);
            return result;
        }

        [HttpPost("GetPeoplePaging")]
        public async Task<ApiResult<PageResult<PeopleVMD>>> GetPeoplePaging(PeoplePagingRequest request)
        {
            var result = await _PeopleService.GetPeoplePagingAsync(request);
            return result;
        }

        [HttpGet("GetPeopleById/{id}")]
        public async Task<ApiResult<PeopleVMD>> GetPeopleByIdAsync(int id)
        {
            var result = await _PeopleService.GetPeopleById(id);
            return result;
        }

        [HttpGet("GetAllPeople")]
        public async Task<ApiResult<List<PeopleVMD>>> GetAllPeopleAsync()
        {
            var result = await _PeopleService.GetAllPeopleAsync();
            return result;
        }
    }
}