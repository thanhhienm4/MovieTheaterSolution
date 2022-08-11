using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Film.MovieCensorships;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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
                $"{ApiConstant.ApiMovieCensorship}/{ApiConstant.GetMovieCensorship}");
        }
    }
}