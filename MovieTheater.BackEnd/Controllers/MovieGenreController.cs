using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.FilmServices.MovieGenres;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Catalog.Film.MovieGenres;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieGenreController : BaseController
    {
        private readonly IMovieGenreService _movieGenreService;

        public MovieGenreController(IMovieGenreService movieGenreService, IUserService customerService) : base(customerService)
        {
            _movieGenreService = movieGenreService;
        }

        [HttpGet(APIConstant.GetMovieGenre)]
        public async Task<ApiResult<List<MovieGenreVMD>>> GetAllMovieGenreAsync()
        {
            var result = await _movieGenreService.GetAllMovieGenreAsync();
            return result;
        }
    }
}