using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    class RoomApiClient : BaseApiClient
    {
        public RoomApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
             httpContextAccessor)
        { }

   
        public async Task<ApiResultLite> CreateAsync(RoomCreateRequest request)
        {
            return await PostAsync<ApiResultLite>("/api/Room/Create", request);
        }

        
        public async Task<ApiResultLite> UpdateAsync(RoomUpdateRequest request)
        {
            return await PostAsync<ApiResultLite>("/api/Room/Update", request);
        }

        
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            return await DeleteAsync<ApiResultLite>($"Api/Room/Delete/{id}");
        }
    }
}
