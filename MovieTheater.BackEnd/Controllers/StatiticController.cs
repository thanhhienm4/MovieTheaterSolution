using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.Statitic;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
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

        [HttpPost("GetTopRevenueFilm")]
        public async Task<ApiResult<ChartData>> GetTopRevenueFilmAsync(CalRevenueRequest request)
        {
            var result = await _statiticService.GetTopRevenueFilmAsync(request);
            return result;
        }

        [HttpPost("GetRevenueAsync")]
        public async Task<ApiResult<long>> GetGetRevenueAsync(CalRevenueRequest request)
        {
            var result = await _statiticService.GetRevenueAsync(request);
            return result;
        }

        [HttpPost("GetRevenueTypeAsync")]
        public async Task<ApiResult<ChartData>> GetRevenueTypeAsync(CalRevenueRequest request)
        {
            var result = await _statiticService.GetRevenueTypeAsync(request);
            return result;
        }
        [HttpPost("GetRevenueInNMonthAsync")]
        public async Task<ApiResult<ChartData>> GetRevenueInNMonthAsync(int n)
        {
            var result = await _statiticService.GetRevenueInNMonthAsync(n);
            return result;
        }
      
    }
}