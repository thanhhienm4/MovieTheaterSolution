﻿using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.FilmServices;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
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
        public async Task<ApiResultLite> CreateAsync(FilmCreateRequest model)
        {
            var result = await _filmService.CreateAsync(model);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ApiResultLite> UpdateAsync(FilmUpdateRequest request)
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

        [HttpGet]
        public async Task<PageResult<FilmVMD>> GetFilmPaging(FilmPagingRequest request)
        {
            var result = await _filmService.GetFilmPagingAsync(request);
            return result;
        }
    }
}