using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class RoomApiClient : BaseApiClient
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


        public async Task<PageResult<RoomVMD>> GetRoomPagingAsync(RoomPagingRequest request)
        {
            return await PostAsync<PageResult<RoomVMD>>($"Api/Room/GetRoomPaging", request);
        }


        public async Task<ApiResult<RoomVMD>> GetRoomByIdAsync(int id)
        {
            return await GetAsync<ApiResult<RoomVMD>>($"Api/Room/GetRoomById/{id}");
        }
    }
}
