using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using MovieTheater.Models.Price.TicketPrice;

namespace MovieTheater.Api
{
    public class TicketPriceApiClient : BaseApiClient
    {
        public TicketPriceApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<bool>> CreateAsync(TicketPriceCreateRequest request)
        {
            return await PostAsync<bool>($"{ApiConstant.ApiTicketPrice}/{ApiConstant.TicketPriceCreate}", request);
        }

        public async Task<ApiResult<bool>> UpdateAsync(TicketPriceUpdateRequest request)
        {
            return await PostAsync<bool>($"{ApiConstant.ApiTicketPrice}/{ApiConstant.TicketPriceUpdate}", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            return await DeleteAsync<bool>($"{ApiConstant.ApiTicketPrice}/{ApiConstant.TicketPriceDelete}/{id}");
        }

        public async Task<ApiResult<TicketPriceVmd>> GetByIdAsync(int id)
        {
            var queryParams = new NameValueCollection()
            {
                { "id", id.ToString() }
            };
            return await GetAsync<TicketPriceVmd>($"{ApiConstant.ApiTicketPrice}/{ApiConstant.TicketPriceGetById}", queryParams);
        }

        public async Task<ApiResult<PageResult<TicketPriceVmd>>> GetPagingAsync(TicketPricePagingRequest request)
        {
            return await PostAsync<PageResult<TicketPriceVmd>>($"{ApiConstant.ApiTicketPrice}/{ApiConstant.TicketPricePaging}", request);
        }
    }
}