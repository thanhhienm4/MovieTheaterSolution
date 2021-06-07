using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class RoomApiClient : BaseApiClient
    {
        public RoomApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
             httpContextAccessor)
        { }

        public async Task<ApiResult<bool>> CreateAsync(RoomCreateRequest request)
        {
            return await PostAsync<bool>("/api/Room/Create", request);
        }

        public async Task<ApiResult<bool>> UpdateAsync(RoomUpdateRequest request)
        {
            return await PostAsync<bool>("/api/Room/Update", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            return await DeleteAsync<bool>($"Api/Room/Delete/{id}");
        }

        public async Task<ApiResult<PageResult<RoomVMD>>> GetRoomPagingAsync(RoomPagingRequest request)
        {
            return await PostAsync<PageResult<RoomVMD>>($"Api/Room/GetRoomPaging", request);
        }

        public async Task<ApiResult<RoomMD>> GetRoomByIdAsync(int id)
        {
            return await GetAsync<RoomMD>($"Api/Room/GetRoomById/{id}");
        }

        public async Task<ApiResult<List<RoomVMD>>> GetAllRoomAsync()
        {
            return await GetAsync<List<RoomVMD>>($"Api/Room/GetAllRoom");
        }

        public async Task<ApiResult<List<RoomFormatVMD>>> GetAllRoomFormatAsync()
        {
            return await GetAsync<List<RoomFormatVMD>>($"Api/RoomFormat/GetAllRoomFormat");
        }
     
        public async Task<ApiResult<RoomCoordinate>> GetCoordinateAsync(int id)
        {
            
            return await GetAsync< RoomCoordinate > ($"Api/Room/GetCoordinate/{id}");
        }
        public async Task<ApiResult<RoomFormatVMD>> GetRoomFormatByIdAsync(int id)
        {
            return await GetAsync<RoomFormatVMD>($"Api/RoomFormat/GetRoomFormatById/{id}");
        }


        public async Task<ApiResult<bool>> CreateRoomFormatAsync(RoomFormatCreateRequest request)
        {
            return await PostAsync<bool>("/api/RoomFormat/Create", request);
        }

        public async Task<ApiResult<bool>> UpdateRoomFormatAsync(RoomFormatUpdateRequest request)
        {
            return await PostAsync<bool>("/api/RoomFormat/Update", request);
        }

        public async Task<ApiResult<bool>> DeleteRoomFormatAsync(int id)
        {
            return await DeleteAsync<bool>($"Api/Room/Delete/{id}");
        }



    }
}