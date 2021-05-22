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

        public async Task<ApiResultLite> CreateAsync(ScreeningCreateRequest request)
        {
            return await PostAsync<ApiResultLite>("Api/Screening/Create", request);
        }

        public async Task<ApiResultLite> UpdateAsync(ScreeningUpdateRequest request)
        {
            return await PutAsync<ApiResultLite>("Api/Screening/Update", request);
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            return await DeleteAsync<ApiResultLite>($"Api/Screening/Delete/{id}");
        }

        public async Task<ApiResult<ScreeningMD>> GetScreeningMDByIdAsync(int id)
        {
            return await GetAsync<ApiResult<ScreeningMD>>($"Api/Screening/GetScreeningMDById/{id}");
        }

        public async Task<ApiResult<ScreeningVMD>> GetScreeningVMDByIdAsync(int id)
        {
            return await GetAsync<ApiResult<ScreeningVMD>>($"Api/Screening/GetScreeningVMDById/{id}");
        }

        public async Task<ApiResult<List<FilmScreeningVMD>>> GetFilmScreeningIndateAsync(DateTime? date)
        {
            return await GetAsync<ApiResult<List<FilmScreeningVMD>>>($"Api/Screening/GetFilmScreeningIndate?date={date}");
        }

        public async Task<ApiResult<PageResult<ScreeningVMD>>> GetScreeningPagingAsync(ScreeningPagingRequest request)
        {
            return await PostAsync<ApiResult<PageResult<ScreeningVMD>>>($"Api/Screening/GetScreeningPaging", request);
        }

        public async Task<ApiResult<List<KindOfScreeningVMD>>> GetAllKindOfScreeningAsync()
        {
            return await GetAsync<ApiResult<List<KindOfScreeningVMD>>>($"Api/KindOfScreening/GetAllKindOfScreening");
        }

        public async Task<ApiResult<ScreeningOfFilmInWeekVMD>> GetListCreeningOfFilmInWeekAsync(int filmId)
        {
            return await GetAsync<ApiResult<ScreeningOfFilmInWeekVMD>>($"Api/Screening/GetListCreeningOfFilmInWeek/{filmId}");
        }
    }
}