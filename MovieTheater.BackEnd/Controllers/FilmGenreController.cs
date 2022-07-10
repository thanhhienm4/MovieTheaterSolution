using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.FilmServices;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class FilmGenreController : BaseController
    {
        private readonly IFilmGenreService _filmGenreService;

        public FilmGenreController(IFilmGenreService filmGenreService, IUserService userService) : base(userService)
        {
            _filmGenreService = filmGenreService;
        }

        [HttpGet("getAllFilmGenre")]
        public async Task<ApiResult<List<FilmGenreVMD>>> GetAllFilmGenreAsync()
        {
            var result = await _filmGenreService.GetAllFilmGenreAsync();
            return result;
        }
    }
}