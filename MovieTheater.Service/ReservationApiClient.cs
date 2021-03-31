using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    class ReservationApiClient : BaseApiClient
    {
        public ReservationApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
             httpContextAccessor)
        { }

        
        public async Task<ApiResultLite> CreateAsync(ReservationCreateRequest request)
        {
            return await PostAsync<ApiResultLite>("/api/Reservation/Create", request);
        }

        
        public async Task<ApiResultLite> UpdateAsync(ReservationUpdateRequest request)
        {
            return await PostAsync<ApiResultLite>("/api/Reservation/Update", request);
        }

        
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            return await DeleteAsync<ApiResultLite>($"Api/Reservation/Delete/{id}");
        }
    }
}
