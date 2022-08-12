using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Invoice;
using MovieTheater.Models.Common.ApiResult;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class InvoiceApiClient : BaseApiClient
    {
        public InvoiceApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<bool>> CreateAsync(InvoiceCreateRequest request)
        {
            return await PostAsync<bool>("/api/Invoice/Create", request);
        }
    }
}