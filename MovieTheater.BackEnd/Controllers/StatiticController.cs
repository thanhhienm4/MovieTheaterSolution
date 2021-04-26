using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.Statitic;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatiticController : Controller
    {
        private readonly IStatiticService _statiticService;
        public StatiticController(IStatiticService statiticService)
        {
            _statiticService = statiticService;
        }
        [HttpPost("GetTopGrossingFilm")]
        public async Task<ApiResult<ChartData>> GetTopGrossingFilmAsync(TopGrossingFilmRequest request)
        {
            var result = await _statiticService.GetTopGrossingFilmAsync(request);
            return result;

        }

    }
}
