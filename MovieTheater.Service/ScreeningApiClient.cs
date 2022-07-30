using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;

namespace MovieTheater.Api
{
    public class ScreeningApiClient : BaseApiClient
    {
        public ScreeningApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<bool>> CreateAsync(ScreeningCreateRequest request)
        {
            return await PostAsync<bool>($"{APIConstant.ApiScreening}/{APIConstant.ScreeningCreate}", request);
        }

        public async Task<ApiResult<bool>> UpdateAsync(ScreeningUpdateRequest request)
        {
            return await PutAsync<bool>($"{APIConstant.ApiScreening}/{APIConstant.ScreeningUpdate}", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            return await DeleteAsync<bool>($"{APIConstant.ApiScreening}/{APIConstant.ScreeningDelete}/{id}");
        }

        public async Task<ApiResult<ScreeningMD>> GetScreeningMDByIdAsync(int id)
        {
            return await GetAsync<ScreeningMD>($"Api/Screening/GetScreeningMDById/{id}");
        }

        public async Task<ApiResult<ScreeningVMD>> GetScreeningVMDByIdAsync(int id)
        {
            return await GetAsync<ScreeningVMD>($"Api/Screening/GetScreeningVMDById/{id}");
        }

        public async Task<ApiResult<List<MovieScreeningVMD>>> GetScreeningInDateAsync(DateTime? date)
        {
            var queryParams = new NameValueCollection()
            {
                { "date", date.ToString() }
            };
            return await GetAsync<List<MovieScreeningVMD>>(
                $"{APIConstant.ApiScreening}/{APIConstant.ScreeningGetScreeningInDate}", queryParams);
        }

        public async Task<ApiResult<PageResult<ScreeningVMD>>> GetPagingAsync(ScreeningPagingRequest request)
        {
            return await PostAsync<PageResult<ScreeningVMD>>(
                $"{APIConstant.ApiScreening}/{APIConstant.ScreeningGetPaging}", request);
        }


        public async Task<ApiResult<ScreeningOfFilmInWeekVMD>> GetListCreeningOfFilmInWeekAsync(int filmId)
        {
            return await GetAsync<ScreeningOfFilmInWeekVMD>($"Api/Screening/GetListCreeningOfFilmInWeek/{filmId}");
        }
    }
}