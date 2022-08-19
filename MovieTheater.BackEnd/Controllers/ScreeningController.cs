﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.ScreeningServices.Screenings;
using MovieTheater.Application.UserServices;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Models.Utilities;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ScreeningController : BaseController
    {
        private readonly IScreeningService _screeningService;

        public ScreeningController(IScreeningService screeningService, IUserService userService) : base(userService)
        {
            _screeningService = screeningService;
        }

        [HttpPost(ApiConstant.ScreeningCreate)]
        public async Task<ApiResult<bool>> CreateAsync(ScreeningCreateRequest model)
        {
            var result = await _screeningService.CreateAsync(model);
            return result;
        }

        [HttpPut(ApiConstant.ScreeningUpdate)]
        public async Task<ApiResult<bool>> UpdateAsync(ScreeningUpdateRequest request)
        {
            var result = await _screeningService.UpdateAsync(request);
            return result;
        }

        [HttpDelete(ApiConstant.ScreeningDelete + "/{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var result = await _screeningService.DeleteAsync(id);
            return result;
        }

        [HttpPost(ApiConstant.ScreeningGetPaging)]
        public async Task<ApiResult<PageResult<ScreeningVMD>>> GetPagingAsync(ScreeningPagingRequest request)
        {
            var result = await _screeningService.GetScreeningPagingAsync(request);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("GetScreeningMDById/{id}")]
        public async Task<ApiResult<ScreeningMD>> GetScreeningMDByIdAsync(int id)
        {
            var result = await _screeningService.GetMDByIdAsync(id);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("GetScreeningVMDById/{id}")]
        public async Task<ApiResult<ScreeningVMD>> GetVMDByIdAsync(int id)
        {
            var result = await _screeningService.GetVMDByIdAsync(id);
            return result;
        }

        [AllowAnonymous]
        [HttpGet(ApiConstant.ScreeningGetByAuditorium)]
        public async Task<ApiResult<List<FullCalendarEvent>>> GetByAuditorium(DateTime fromDate, DateTime toDate, string auditoriumId )
        {
            var result = await _screeningService.GetByAuditorium(fromDate, toDate, auditoriumId);
            return result;
        }

        [AllowAnonymous]
        [HttpGet(ApiConstant.ScreeningGetScreeningInDate)]
        public async Task<ApiResult<List<MovieScreeningVMD>>> GetMovieScreeningInDate([FromQuery] DateTime? date)
        {
            var result = await _screeningService.GetFilmScreeningInday(date);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("GetListOfMovieInWeek/{filmId}")]
        public async Task<ApiResult<ScreeningOfFilmInWeekVMD>> GetListScreeningOfMovieInWeek(string filmId)
        {
            var result = await _screeningService.GetListOfMovieInWeek(filmId);
            return result;
        }
    }
}