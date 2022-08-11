using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.FilmServices.MovieGenres;
using MovieTheater.Application.UserServices;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Film.MovieGenres;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieGenreController : BaseController
    {
        private readonly IMovieGenreService _movieGenreService;

        public MovieGenreController(IMovieGenreService movieGenreService, IUserService userService) : base(userService)
        {
            _movieGenreService = movieGenreService;
        }

        [HttpGet(ApiConstant.GetMovieGenre)]
        public async Task<ApiResult<List<MovieGenreVMD>>> GetAllMovieGenreAsync()
        {
            var result = await _movieGenreService.GetAllMovieGenreAsync();
            return result;
        }
    }
}