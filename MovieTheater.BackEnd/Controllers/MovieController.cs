using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.FilmServices.Movies;
using MovieTheater.Application.UserServices;
using MovieTheater.Common.Constants;
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
    public class MovieController : BaseController
    {
        private readonly IMovieService _filmService;

        public MovieController(IMovieService filmService, IUserService userService) : base(userService)
        {
            _filmService = filmService;
        }

        [HttpPost(ApiConstant.MovieCreate)]
        public async Task<ApiResult<bool>> CreateAsync([FromForm] MovieCreateRequest model)
        {
            var result = await _filmService.CreateAsync(model);
            return result;
        }

        [HttpPut(ApiConstant.MovieUpdate)]
        public async Task<ApiResult<bool>> UpdateAsync([FromForm] MovieUpdateRequest request)
        {
            var result = await _filmService.UpdateAsync(request);
            return result;
        }

        [HttpDelete(ApiConstant.MovieDelete)]
        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            var result = await _filmService.DeleteAsync(id);
            return result;
        }

        [HttpPost(ApiConstant.GetMoviePaging)]
        public async Task<ApiResult<PageResult<MovieVMD>>> GetPaging(FilmPagingRequest request)
        {
            var result = await _filmService.GetPagingAsync(request);
            return result;
        }

        [HttpGet(ApiConstant.MovieGetById)]
        public async Task<ApiResult<MovieMD>> GetByIdAsync(string id)
        {
            var result = await _filmService.GetById(id);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("GetFilmVMDById/{id}")]
        public async Task<ApiResult<MovieVMD>> GetFilmVMDByIdAsync(string id)
        {
            var result = await _filmService.GetFilmVMDById(id);
            return result;
        }

        [HttpGet("getAllFilm")]
        public async Task<ApiResult<List<MovieVMD>>> GetAllBanAsync()
        {
            var result = await _filmService.GetAllAsync();
            return result;
        }

        //[HttpPost("GenreAssign")]
        //public async Task<ApiResult<bool>> GenreAssignAsync(GenreAssignRequest request)
        //{
        //    var result = await _filmService.GenreAssignAsync(request);
        //    return result;
        //}

        [AllowAnonymous]
        [HttpGet("getAllPlayingFilm")]
        public async Task<ApiResult<List<MovieVMD>>> GetAllPlayingFilmAsync()
        {
            var result = await _filmService.GetAllPlayingAsync();
            return result;
        }

        [AllowAnonymous]
        [HttpGet("getAllUpcomingFilm")]
        public async Task<ApiResult<List<MovieVMD>>> GetAllUpcomingBanAsync()
        {
            var result = await _filmService.GetAllUpcomingAsync();
            return result;
        }

        //[HttpPost("PosAssign")]
        //public async Task<ApiResult<bool>> PosAssignAsync(PosAssignRequest request)
        //{
        //    var result = await _filmService.PosAssignAsync(request);
        //    return result;
        //}

        //[HttpPost("DeletePosAssign")]
        //public async Task<ApiResult<bool>> DeletePosAssignAsync(PosAssignRequest request)
        //{
        //    var result = await _filmService.DeletePosAssignAsync(request);
        //    return result;
        //}

        //[HttpGet("GetJoining/{id}")]
        //public async Task<ApiResult<List<JoiningPosVMD>>> GetJoiningAsync(int id)
        //{
        //    var result = await _filmService.GetJoiningAsync(id);
        //    return result;
        //}
    }
}