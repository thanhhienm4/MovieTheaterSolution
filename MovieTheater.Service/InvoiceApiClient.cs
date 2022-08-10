using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Invoice;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;

namespace MovieTheater.Api
{
    public class InvoiceApiClient : BaseApiClient
    {
        public InvoiceApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        { }

        public async Task<ApiResult<bool>> CreateAsync(InvoiceCreateRequest request)
        {
            return await PostAsync<bool>("/api/Invoice/Create", request);
        }
    }
}
