﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using MovieTheater.Models.Infra.Seat.KindOfSeat;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;
using MovieTheater.Data.Results;

namespace MovieTheater.Api
{
    public class SeatApiClient : BaseApiClient
    {
        public SeatApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<List<SeatVMD>>> GetSeatInRoomAsync(string roomId)
        {
            return await GetAsync<List<SeatVMD>>($"{ApiConstant.ApiSeat}/{ApiConstant.SeatGetAllInRoom}/{roomId}");
        }

        public async Task<ApiResult<bool>> UpdateSeatInRoomAsync(SeatsInRoomUpdateRequest request)
        {
            return await PutAsync<bool>($"{ApiConstant.ApiSeat}/{ApiConstant.SeatUpdateInRoom}", request);
        }

        public async Task<ApiResult<List<SeatModel>>> GetListSeatReserved(int screeningId)
        {
            return await GetAsync<List<SeatModel>>(
                $"{ApiConstant.ApiSeat}/{ApiConstant.SeatGetListSeatReserve}/{screeningId}");
        }

        public async Task<ApiResult<bool>> CreateKindOfSeatAsync(SeatTypeCreateRequest request)
        {
            return await PostAsync<bool>("Api/KindOfSeat/Create", request);
        }

        public async Task<ApiResult<bool>> UpdateKindOfSeatAsync(KindOfSeatUpdateRequest request)
        {
            return await PutAsync<bool>("Api/KindOfSeat/Update", request);
        }

        public async Task<ApiResult<bool>> DeleteKindOfSeatAsync(int id)
        {
            return await DeleteAsync<bool>($"Api/KindOfSeat/Delete/{id}");
        }

        public async Task<ApiResult<List<SeatTypeVMD>>> GetAllKindOfSeatAsync()
        {
            return await GetAsync<List<SeatTypeVMD>>($"Api/KindOfSeat/GetAllKindOfSeat");
        }

        public async Task<ApiResult<SeatTypeVMD>> GetKindOfSeatByIdAsync(int id)
        {
            return await GetAsync<SeatTypeVMD>($"Api/KindOfSeat/GetKindOfSeatById/{id}");
        }
    }
}