using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.ScreeningServices;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningController : Controller
    {
        private readonly IScreeningService _screeningService;
        public ScreeningController(IScreeningService screeningService)
        {
            _screeningService = screeningService;
        }

        [HttpPost("Create")]
        public async Task<ApiResultLite> CreateAsync(ScreeningCreateRequest model)
        {
            var result = await _screeningService.CreateAsync(model);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ApiResultLite> UpdateAsync(ScreeningUpdateRequest request)
        {
            var result = await _screeningService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResultLite> DeleteAsync(int id)
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

        [HttpGet("GetScreeningMDById/{id}")]
        public async Task<ApiResult<ScreeningMD>> GetScreeningMDByIdAsync(int id)
        {
            var result = await _screeningService.GetScreeningMDByIdAsync(id);
            return result;
        }


        [HttpGet("GetScreeningVMDById/{id}")]
        public async Task<ApiResult<ScreeningVMD>> GetScreeningVMDByIdAsync(int id)
        {
            var result = await _screeningService.GetScreeningVMDByIdAsync(id);
            return result;
        }

        [HttpGet("GetFilmScreeningIndate")]
        public async Task<ApiResult<List<FilmScreeningVMD>>> GetFilmScreeningIndate([FromQuery] DateTime? date)
        {
            var result = await _screeningService.GetFilmScreeningInday(date);
            return result;
        }


}
}
