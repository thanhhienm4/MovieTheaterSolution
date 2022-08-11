using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.FilmServices.MovieCensorshipes;
using MovieTheater.Application.UserServices;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Film.MovieCensorships;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    public class MovieCensorshipController : BaseController
    {
        private readonly IMovieCensorshipService _movieCensorshipService;

        public MovieCensorshipController(IMovieCensorshipService movieCensorshipService, IUserService userService) :
            base(userService)
        {
            _movieCensorshipService = movieCensorshipService;
        }

        [HttpGet(ApiConstant.GetMovieCensorship)]
        public async Task<ApiResult<List<MovieCensorshipVMD>>> GetAllBanAsync()
        {
            var result = await _movieCensorshipService.GetAllBanAsync();
            return result;
        }
    }
}