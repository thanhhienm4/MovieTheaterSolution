using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.SeatRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class SeatRowApiClient : BaseApiClient
    {
        public SeatRowApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
          IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
           httpContextAccessor)
        { }
       
        public async Task<ApiResult<List<SeatRowVMD>>> GetAllSeatRowsAsync()
        {
            return await GetAsync<ApiResult<List<SeatRowVMD>>>("Api/SeatRow/GetAllSeatRows");
        }
    }
}
