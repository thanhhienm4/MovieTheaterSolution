using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;
using MovieTheater.Models.User;

namespace MovieTheater.Api
{
    public class CustomerTypeApiClient : BaseApiClient
    {
        public CustomerTypeApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<IList<CustomerTypeVmd>>> GetAllAsync()
        {
            return await GetAsync<IList<CustomerTypeVmd>>(
                $"{ApiConstant.ApiCustomer}/{ApiConstant.CustomerTypeGetAll}");
        }
    }
}