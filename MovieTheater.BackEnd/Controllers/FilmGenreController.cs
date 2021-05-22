using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.FilmServices;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmGenreController : Controller
    {
        private readonly IFilmGenreService _filmGenreService;

        public FilmGenreController(IFilmGenreService filmGenreService)
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