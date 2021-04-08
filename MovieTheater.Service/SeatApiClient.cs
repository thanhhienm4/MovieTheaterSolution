using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            return await GetAsync<ApiResult<List<SeatVMD>>>($"Api/Seat/GetSeatInRoomAsync/{roomId}");
        }
        public async Task<ApiResultLite> UpdateSeatInRoomAsync(SeatsInRoomUpdateRequest request)
        {
            return await PutAsync<ApiResultLite>($"Api/Seat/UpdateSeatInRoomAsync",request);
        }
    }
  
}
