using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.FilmServices;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpPost("Create")]
        public async Task<ApiResultLite> CreateAsync([FromForm]FilmCreateRequest model)
        {
            var result = await _filmService.CreateAsync(model);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResultLite> UpdateAsync([FromForm]FilmUpdateRequest request)
        {
            var result = await _filmService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            var result = await _filmService.DeleteAsync(id);
            return result;
        }

        [HttpPost("GetFilmPaging")]
        public async Task<ApiResult<PageResult<FilmVMD>>> GetFilmPaging(FilmPagingRequest request)
        {
            var result = await _filmService.GetFilmPagingAsync(request);
            return result;
        }

        [HttpGet("GetFilmMDById/{id}")]
        public async Task<ApiResult<FilmMD>> GetFilmMDByIdAsync(int id)
        {
            var result = await _filmService.GetFilmMDById(id);
            return result;
        }

        [HttpGet("GetFilmVMDById/{id}")]
        public async Task<ApiResult<FilmVMD>> GetFilmVMDByIdAsync(int id)
        {
            var result = await _filmService.GetFilmVMDById(id);
            return result;
        }

        [HttpGet("getAllFilm")]
        public async Task<ApiResult<List<FilmVMD>>> GetAllBanAsync()
        {
            var result = await _filmService.GetAllFilmAsync();
            return result;
        }
    }
}