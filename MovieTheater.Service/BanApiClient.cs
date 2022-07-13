using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Film.MovieCensorships;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class BanApiClient : BaseApiClient
    {
        public BanApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
           IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        { }

        public async Task<ApiResult<List<MovieCensorshipVMD>>> GetAllBanAsync()
        {
            return await GetAsync<List<MovieCensorshipVMD>>($"Api/Ban/GetAllBan");
        }
    }
}