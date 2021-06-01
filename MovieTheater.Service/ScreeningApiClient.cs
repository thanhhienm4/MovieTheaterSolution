using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class ScreeningApiClient : BaseApiClient
    {
        public ScreeningApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
             httpContextAccessor)
        { }

        public async Task<ApiResult<bool>> CreateAsync(ScreeningCreateRequest request)
        {
            return await PostAsync<bool>("Api/Screening/Create", request);
        }

        public async Task<ApiResult<bool>> UpdateAsync(ScreeningUpdateRequest request)
        {
            return await PutAsync<bool>("Api/Screening/Update", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            return await DeleteAsync<bool>($"Api/Screening/Delete/{id}");
        }

        public async Task<ApiResult<ScreeningMD>> GetScreeningMDByIdAsync(int id)
        {
            return await GetAsync<ScreeningMD>($"Api/Screening/GetScreeningMDById/{id}");
        }

        public async Task<ApiResult<ScreeningVMD>> GetScreeningVMDByIdAsync(int id)
        {
            return await GetAsync<ScreeningVMD>($"Api/Screening/GetScreeningVMDById/{id}");
        }

        public async Task<ApiResult<List<FilmScreeningVMD>>> GetFilmScreeningIndateAsync(DateTime? date)
        {
            return await GetAsync<List<FilmScreeningVMD>>($"Api/Screening/GetFilmScreeningIndate?date={date}");
        }

        public async Task<ApiResult<PageResult<ScreeningVMD>>> GetScreeningPagingAsync(ScreeningPagingRequest request)
        {
            return await PostAsync<PageResult<ScreeningVMD>>($"Api/Screening/GetScreeningPaging", request);
        }

        public async Task<ApiResult<List<KindOfScreeningVMD>>> GetAllKindOfScreeningAsync()
        {
            return await GetAsync<List<KindOfScreeningVMD>>($"Api/KindOfScreening/GetAllKindOfScreening");
        }

        public async Task<ApiResult<ScreeningOfFilmInWeekVMD>> GetListCreeningOfFilmInWeekAsync(int filmId)
        {
            return await GetAsync<ScreeningOfFilmInWeekVMD>($"Api/Screening/GetListCreeningOfFilmInWeek/{filmId}");
        }
    }
}