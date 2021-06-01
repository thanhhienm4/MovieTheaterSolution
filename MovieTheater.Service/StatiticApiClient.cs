using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class StatiticApiClient : BaseApiClient
    {
        public StatiticApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
           IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        { }

        public async Task<ApiResult<ChartData>> GetTopRevenueFilmAsync(CalRevenueRequest request)
        {
            return await PostAsync<ChartData>("Api/Statitic/GetTopRevenueFilm", request);
        }

        public async Task<ApiResult<long>> GetRevenueAsync(CalRevenueRequest request)
        {
            return await PostAsync<long>("Api/Statitic/GetRevenueAsync", request);
        }

        public async Task<ApiResult<ChartData>> GetRevenueTypeAsync(CalRevenueRequest request)
        {
            return await PostAsync<ChartData>("Api/Statitic/GetRevenueTypeAsync", request);
        }
    }
}