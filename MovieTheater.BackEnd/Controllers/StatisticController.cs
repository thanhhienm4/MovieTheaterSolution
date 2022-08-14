using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.Statistic;
using MovieTheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Invoice;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StatisticController : BaseController
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService, IUserService userService) : base(userService)
        {
            _statisticService = statisticService;
        }

        [HttpPost("GetTopRevenueFilm")]
        public async Task<ApiResult<ChartData>> GetTopRevenueFilmAsync(CalRevenueRequest request)
        {
            var result = await _statisticService.GetTopRevenueFilmAsync(request);
            return result;
        }

        [HttpPost("GetRevenueAsync")]
        public async Task<ApiResult<long>> GetGetRevenueAsync(CalRevenueRequest request)
        {
            var result = await _statisticService.GetRevenueAsync(request);
            return result;
        }

        [HttpPost("GetRevenueTypeAsync")]
        public async Task<ApiResult<ChartData>> GetRevenueTypeAsync(CalRevenueRequest request)
        {
            var result = await _statisticService.GetRevenueTypeAsync(request);
            return result;
        }

        [HttpPost("GetRevenueInNMonthAsync")]
        public async Task<ApiResult<ChartData>> GetRevenueInNMonthAsync(int n)
        {
            var result = await _statisticService.GetRevenueInNMonthAsync(n);
            return result;
        }

        [HttpGet(ApiConstant.StatisticGetRawData)]
        public async Task<ApiResult<IList<InvoiceRawData>>> GetRawData(DateTime fromDate, DateTime toDate)
        {
            var result = await _statisticService.GetRawData(fromDate, toDate);
            return result;
        }

        [HttpGet(ApiConstant.StatisticGetRevenueDayInWeek)]
        public async Task<ApiResult<ChartData>> GetRevenueDayInWeek(DateTime fromDate, DateTime toDate)
        {
            var result = await _statisticService.GetRevenueDayInWeek(fromDate, toDate);
            return result;
        }

    }
}