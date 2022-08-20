using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using MovieTheater.Models.Price.Surcharge;

namespace MovieTheater.Api
{
    public class SurchargeApiClient : BaseApiClient
    {
        public SurchargeApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<bool>> CreateAsync(SurchargeCreateRequest request)
        {
            return await PostAsync<bool>($"{ApiConstant.ApiSurcharge}/{ApiConstant.SurchargeCreate}", request);
        }

        public async Task<ApiResult<bool>> UpdateAsync(SurchargeUpdateRequest request)
        {
            return await PutAsync<bool>($"{ApiConstant.ApiSurcharge}/{ApiConstant.SurchargeUpdate}", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            return await DeleteAsync<bool>($"{ApiConstant.ApiSurcharge}/{ApiConstant.SurchargeDelete}/{id}");
        }

        public async Task<ApiResult<SurchargeVmd>> GetByIdAsync(int id)
        {
            var queryParams = new NameValueCollection()
            {
                { "id", id.ToString() }
            };
            return await GetAsync<SurchargeVmd>($"{ApiConstant.ApiSurcharge}/{ApiConstant.SurchargeGetById}",
                queryParams);
        }


        public async Task<ApiResult<PageResult<SurchargeVmd>>> GetPagingAsync(SurchargePagingRequest request)
        {
            return await PostAsync<PageResult<SurchargeVmd>>(
                $"{ApiConstant.ApiSurcharge}/{ApiConstant.SurchargePaging}", request);
        }
    }
}