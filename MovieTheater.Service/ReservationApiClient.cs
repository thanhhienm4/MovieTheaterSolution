﻿using Microsoft.AspNetCore.Http;
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
        { }

        public async Task<ApiResultLite> CreateAsync(ReservationCreateRequest request)
        {
            return await PostAsync<ApiResultLite>("/api/Reservation/Create", request);
        }

        public async Task<ApiResultLite> UpdateAsync(ReservationUpdateRequest request)
        {
            return await PutAsync<ApiResultLite>("/api/Reservation/Update", request);
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            return await DeleteAsync<ApiResultLite>($"Api/Reservation/Delete/{id}");
        }

        public async Task<ApiResult<PageResult<ReservationVMD>>> GetReservationPagingAsync(ReservationPagingRequest request)
        {
            return await PostAsync<ApiResult<PageResult<ReservationVMD>>>($"Api/Reservation/GetReservationPaging", request);
        }

        public async Task<ApiResult<ReservationVMD>> GetReservationByIdAsync(int id)
        {
            return await GetAsync<ApiResult<ReservationVMD>>($"Api/Reservation/GetReservationById/{id}");
        }

        public async Task<ApiResult<List<ReservationVMD>>> GetReservationByUserIdAsync(Guid id)
        {
            return await GetAsync<ApiResult<List<ReservationVMD>>>($"Api/Reservation/GetReservationUserById/{id}");
        }

        public async Task<int> CalPrePriceAsync(List<TicketCreateRequest> tickets)
        {
            return await PostAsync<int>($"Api/Reservation/CalPrePrice", tickets);
        }
    }
}