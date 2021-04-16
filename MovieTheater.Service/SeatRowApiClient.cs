using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.Seat;
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

        public async Task<ApiResultLite> CreateAsync(SeatRowCreateRequest request)
        {
            return await PostAsync<ApiResultLite>("Api/SeatRow/Create", request);
        }
        public async Task<ApiResultLite> UpdateAsync(SeatRowUpdateRequest request)
        {
            return await PutAsync<ApiResultLite>("Api/SeatRow/Update", request);
        }
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            return await DeleteAsync<ApiResultLite>($"Api/SeatRow/Delete/{id}");
        }

        public async Task<ApiResult<List<SeatRowVMD>>> GetAllSeatRowsAsync()
        {
            return await GetAsync<ApiResult<List<SeatRowVMD>>>("Api/SeatRow/GetAllSeatRows");
        }

        public async Task<ApiResult<PageResult<SeatRowVMD>>> GetSeatRowPagingAsync(SeatRowPagingRequest request)
        {
            return await PostAsync<ApiResult<PageResult<SeatRowVMD>>>($"Api/SeatRow/GetSeatRowPaging", request);
        }
        public async Task<ApiResult<SeatRowVMD>> GetSeatRowByIdAsync(int id)
        {
            return await GetAsync<ApiResult<SeatRowVMD>>($"Api/SeatRow/GetSeatRowById/{id}");
        }
    }
}
