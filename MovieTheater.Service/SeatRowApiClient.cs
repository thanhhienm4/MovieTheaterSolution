using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.Seat.SeatRow;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class SeatRowApiClient : BaseApiClient
    {
        public SeatRowApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<bool>> CreateAsync(SeatRowCreateRequest request)
        {
            return await PostAsync<bool>("Api/SeatRow/Create", request);
        }

        public async Task<ApiResult<bool>> UpdateAsync(SeatRowUpdateRequest request)
        {
            return await PutAsync<bool>("Api/SeatRow/Update", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            return await DeleteAsync<bool>($"Api/SeatRow/Delete/{id}");
        }

        public async Task<ApiResult<List<SeatRowVMD>>> GetAllSeatRowsAsync()
        {
            return await GetAsync<List<SeatRowVMD>>("Api/SeatRow/GetAllSeatRows");
        }

        public async Task<ApiResult<PageResult<SeatRowVMD>>> GetSeatRowPagingAsync(SeatRowPagingRequest request)
        {
            return await PostAsync<PageResult<SeatRowVMD>>($"Api/SeatRow/GetSeatRowPaging", request);
        }

        public async Task<ApiResult<SeatRowVMD>> GetSeatRowByIdAsync(int id)
        {
            return await GetAsync<SeatRowVMD>($"Api/SeatRow/GetSeatRowById/{id}");
        }
    }
}