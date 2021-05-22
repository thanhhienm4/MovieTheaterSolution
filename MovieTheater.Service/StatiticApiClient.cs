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

        public async Task<ApiResult<ChartData>> GetTopGrossingFilmAsync(CalRevenueRequest request)
        {
            return await PostAsync<ApiResult<ChartData>>("Api/Statitic/GetTopGrossingFilm", request);
        }

        public async Task<ApiResult<long>> GetRevenueAsync(CalRevenueRequest request)
        {
            return await PostAsync<ApiResult<long>>("Api/Statitic/GetRevenueAsync", request);
        }

        public async Task<ApiResult<ChartData>> GetGroosingTypeAsync(CalRevenueRequest request)
        {
            return await PostAsync<ApiResult<ChartData>>("Api/Statitic/GetGroosingTypeAsync", request);
        }
    }
}