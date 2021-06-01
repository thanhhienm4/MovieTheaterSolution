using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class SeatApiClient : BaseApiClient
    {
        public SeatApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
          IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
           httpContextAccessor)
        { }

        public async Task<ApiResult<List<SeatVMD>>> GetSeatInRoomAsync(int roomId)
        {
            return await GetAsync<List<SeatVMD>>($"Api/Seat/GetSeatInRoomAsync/{roomId}");
        }

        public async Task<ApiResult<bool>> UpdateSeatInRoomAsync(SeatsInRoomUpdateRequest request)
        {
            return await PutAsync<bool>($"Api/Seat/UpdateSeatInRoomAsync", request);
        }

        public async Task<ApiResult<List<SeatVMD>>> GetListSeatReserved(int screeningId)
        {
            return await GetAsync<List<SeatVMD>>($"Api/Seat/GetListSeatReserved/{screeningId}");
        }
    }
}