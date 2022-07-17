using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Film.MovieCensorships;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;

namespace MovieTheater.Api
{
    public class MovieCensorshipApiClient : BaseApiClient
    {
        public MovieCensorshipApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<IList<MovieCensorshipVMD>>> GetAllAsync()
        {
            return await GetAsync<IList<MovieCensorshipVMD>>(
                $"{APIConstant.ApiMovieCensorship}/{APIConstant.GetMovieCensorship}");
        }
    }
}