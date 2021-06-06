using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movietheater.Application.Statitic;
using Movietheater.Application.UserServices;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StatiticController : BaseController
    {
        private readonly IStatiticService _statiticService;
        private readonly IUserService _userService;

        public StatiticController(IStatiticService statiticService, IUserService userService) : base(userService)
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