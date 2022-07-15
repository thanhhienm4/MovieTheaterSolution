using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.ScreeningServices.Screenings;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ScreeningController : BaseController
    {
        private readonly IScreeningService _screeningService;
        private readonly IUserService _userService;

        public ScreeningController(IScreeningService screeningService, IUserService customerService) : base(customerService)
        {
            _screeningService = screeningService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync(ScreeningCreateRequest model)
        {
            var result = await _screeningService.CreateAsync(model);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResult<bool>> UpdateAsync(ScreeningUpdateRequest request)
        {
            var result = await _screeningService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _screeningService.DeleteAsync(id);
            return result;
        }

        [HttpPost("GetScreeningPaging")]
        public async Task<ApiResult<PageResult<ScreeningVMD>>> GetScreeningPagingAsync(ScreeningPagingRequest request)
        {
            var result = await _screeningService.GetScreeningPagingAsync(request);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("GetScreeningMDById/{id}")]
        public async Task<ApiResult<ScreeningMD>> GetScreeningMDByIdAsync(int id)
        {
            var result = await _screeningService.GetScreeningMDByIdAsync(id);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("GetScreeningVMDById/{id}")]
        public async Task<ApiResult<ScreeningVMD>> GetScreeningVMDByIdAsync(int id)
        {
            var result = await _screeningService.GetScreeningVMDByIdAsync(id);
            return result;
        }
        [AllowAnonymous]
        [HttpGet("GetFilmScreeningIndate")]
        public async Task<ApiResult<List<MovieScreeningVMD>>> GetFilmScreeningIndate([FromQuery] DateTime? date)
        {
            var result = await _screeningService.GetFilmScreeningInday(date);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("GetListCreeningOfFilmInWeek/{filmId}")]
        public async Task<ApiResult<ScreeningOfFilmInWeekVMD>> GetListCreeningOfFilmInWeek(int filmId)
        {
            var result = await _screeningService.GetListCreeningOfFilmInWeek(filmId);
            return result;
        }
    }
}