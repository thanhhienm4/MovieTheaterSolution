using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class ReservationApiClient : BaseApiClient
    {
        public ReservationApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<int>> CreateAsync(ReservationCreateRequest request)
        {
            return await PostAsync<int>("/api/Reservation/Create", request);
        }

        public async Task<ApiResult<string>> PaymentAsync(int id)
        {
            return await PostAsync<string>("/api/Reservation/Payment", id);
        }

        public async Task<ApiResult<bool>> UpdateAsync(ReservationUpdateRequest request)
        {
            return await PutAsync<bool>("/api/Reservation/Update", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            return await DeleteAsync<bool>($"Api/Reservation/Delete/{id}");
        }

        public async Task<ApiResult<PageResult<ReservationVMD>>> GetReservationPagingAsync(
            ReservationPagingRequest request)
        {
            return await PostAsync<PageResult<ReservationVMD>>($"Api/Reservation/GetReservationPaging", request);
        }

        public async Task<ApiResult<ReservationVMD>> GetReservationByIdAsync(int id)
        {
            return await GetAsync<ReservationVMD>($"Api/Reservation/GetById/{id}");
        }

        public async Task<ApiResult<List<ReservationVMD>>> GetReservationByUserIdAsync(string id)
        {
            return await GetAsync<List<ReservationVMD>>($"Api/Reservation/GetReservationByUserId/{id}");
        }

        public async Task<ApiResult<decimal>> CalPrePriceAsync(List<TicketCreateRequest> tickets)
        {
            return await PostAsync<decimal>($"Api/Reservation/CalPrePrice", tickets);
        }

        public async Task<ApiResult<bool>> UpdatePaymentAsync(ReservationUpdatePaymentRequest request)
        {
            return await PutAsync<bool>("/api/Reservation/UpdatePayment", request);
        }
    }
}