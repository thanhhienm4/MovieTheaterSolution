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
    public class FilmController : BaseController
    {
        private readonly IFilmService _filmService;
        private readonly IUserService _userService;

        public FilmController(IFilmService filmService, IUserService userService) : base(userService)
        {
            _filmService = filmService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult<bool>> CreateAsync([FromForm] FilmCreateRequest model)
        {
            var result = await _filmService.CreateAsync(model);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ApiResult<bool>> UpdateAsync([FromForm] FilmUpdateRequest request)
        {
            var result = await _filmService.UpdateAsync(request);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
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

        [AllowAnonymous]
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

        [HttpPost("GenreAssign")]
        public async Task<ApiResult<bool>> GenreAssignAsync(GenreAssignRequest request)
        {
            var result = await _filmService.GenreAssignAsync(request);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("getAllPlayingFilm")]
        public async Task<ApiResult<List<FilmVMD>>> GetAllPlayingFilmAsync()
        {
            var result = await _filmService.GetAllPlayingFilmAsync();
            return result;
        }

        [AllowAnonymous]
        [HttpGet("getAllUpcomingFilm")]
        
        public async Task<ApiResult<List<FilmVMD>>> GetAllUpcomingBanAsync()
        {
            var result = await _filmService.GetAllUpcomingFilmAsync();
            return result;
        }

        [HttpPost("PosAssign")]
        public async Task<ApiResult<bool>> PosAssignAsync(PosAssignRequest request)
        {
            var result = await _filmService.PosAssignAsync(request);
            return result;
        }

        [HttpPost("DeletePosAssign")]
        public async Task<ApiResult<bool>> DeletePosAssignAsync(PosAssignRequest request)
        {
            var result = await _filmService.DeletePosAssignAsync(request);
            return result;
        }

        [HttpGet("GetJoining/{id}")]
        public async Task<ApiResult<List<JoiningPosVMD>>> GetJoiningAsync(int id)
        {
            var result = await _filmService.GetJoiningAsync(id);
            return result;
        }
    }
}