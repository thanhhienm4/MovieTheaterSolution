using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class BanApiClient :BaseApiClient
    {
        public BanApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
           IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        { }
        public async Task<ApiResult<List<BanVMD>>> GetAllBanAsync()
        {
            return await GetAsync<ApiResult<List<BanVMD>>>($"Api/Ban/GetAllBan");
        }
    }
}
