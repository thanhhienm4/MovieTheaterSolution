using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.ChartTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public  class StatiticApiClient : BaseApiClient
    {
        public StatiticApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
           IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        { }

       
        public async Task<ApiResult<ChartData>> GetTopGrossingFilmAsync(TopGrossingFilmRequest request)
        {
            return await PostAsync<ApiResult<ChartData>>("Api/Statitic/GetTopGrossingFilm", request); 

        }
    }
}
