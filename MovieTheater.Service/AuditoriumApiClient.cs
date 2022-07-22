﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;

namespace MovieTheater.Api
{
    public class AuditoriumApiClient : BaseApiClient
    {
        public AuditoriumApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<bool>> CreateAsync(AuditoriumCreateRequest request)
        {
            return await PostAsync<bool>($"{APIConstant.ApiAuditorium}/{APIConstant.AuditoriumCreate}", request);
        }

        public async Task<ApiResult<bool>> UpdateAsync(AuditoriumUpdateRequest request)
        {
            return await PostAsync<bool>($"{APIConstant.ApiAuditorium}/{APIConstant.AuditoriumUpdate}", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            return await DeleteAsync<bool>($"{APIConstant.ApiAuditorium}/{APIConstant.AuditoriumDelete}/{id}");
        }

        public async Task<ApiResult<PageResult<AuditoriumVMD>>> GetPagingAsync(AuditoriumPagingRequest request)
        {
            return await PostAsync<PageResult<AuditoriumVMD>>($"{APIConstant.ApiAuditorium}/{APIConstant.AuditoriumGetPaging}", request);
        }

        public async Task<ApiResult<RoomMD>> GetByIdAsync(string id)
        {
            var queryParams = new NameValueCollection()
            {
                { "id", id }
            };
            return await GetAsync<RoomMD>($"{APIConstant.ApiAuditorium}/{APIConstant.AuditoriumGetById}", queryParams);
        }

        public async Task<ApiResult<List<AuditoriumVMD>>> GetAllAsync()
        {
            return await GetAsync<List<AuditoriumVMD>>($"{APIConstant.ApiAuditorium}/{APIConstant.AuditoriumGetAll}");
        }

        public async Task<ApiResult<List<AuditoriumFormatVMD>>> GetAllRoomFormatAsync()
        {
            return await GetAsync<List<AuditoriumFormatVMD>>($"Api/RoomFormat/GetAllRoomFormat");
        }

        public async Task<ApiResult<RoomCoordinate>> GetCoordinateAsync(string id)
        {
            var queryParams = new NameValueCollection()
            {
                { "id", id }
            };
            return await GetAsync<RoomCoordinate>($"{APIConstant.ApiAuditorium}/{APIConstant.AuditoriumGetCoordinate}",queryParams);
        }

        public async Task<ApiResult<AuditoriumFormatVMD>> GetRoomFormatByIdAsync(int id)
        {
            return await GetAsync<AuditoriumFormatVMD>($"Api/RoomFormat/GetRoomFormatById/{id}");
        }


        public async Task<ApiResult<bool>> CreateRoomFormatAsync(AuditoriumFormatCreateRequest request)
        {
            return await PostAsync<bool>("/api/RoomFormat/Create", request);
        }

        public async Task<ApiResult<bool>> UpdateRoomFormatAsync(AuditoriumFormatUpdateRequest request)
        {
            return await PostAsync<bool>("/api/RoomFormat/Update", request);
        }

        public async Task<ApiResult<bool>> DeleteRoomFormatAsync(int id)
        {
            return await DeleteAsync<bool>($"Api/RoomFormat/Delete/{id}");
        }
    }
}