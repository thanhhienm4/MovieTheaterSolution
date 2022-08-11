using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class TicketApiClient : BaseApiClient
    {
        public TicketApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<bool>> CreateAsync(TicketCreateRequest request)
        {
            return await PostAsync<bool>($"{ApiConstant.ApiTicket}/{ApiConstant.TicketCreate}", request);
        }

        public async Task<ApiResult<bool>> UpdateAsync(TicketUpdateRequest request)
        {
            return await PutAsync<bool>($"{ApiConstant.ApiTicket}/{ApiConstant.TicketUpdate}", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(TicketCreateRequest request)
        {
            return await PostAsync<bool>($"{ApiConstant.ApiTicket}/{ApiConstant.TicketDelete}", request);
        }
    }
}