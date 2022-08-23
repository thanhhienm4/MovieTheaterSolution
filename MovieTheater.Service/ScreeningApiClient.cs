using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using MovieTheater.Models.Utilities;

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
            return await PostAsync<bool>($"{ApiConstant.ApiScreening}/{ApiConstant.ScreeningCreate}", request);
        }

        public async Task<ApiResult<bool>> UpdateAsync(ScreeningUpdateRequest request)
        {
            return await PutAsync<bool>($"{ApiConstant.ApiScreening}/{ApiConstant.ScreeningUpdate}", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            return await DeleteAsync<bool>($"{ApiConstant.ApiScreening}/{ApiConstant.ScreeningDelete}/{id}");
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
                $"{ApiConstant.ApiScreening}/{ApiConstant.ScreeningGetScreeningInDate}", queryParams);
        }
        public async Task<ApiResult<List<FullCalendarEvent>>> GetByAuditorium(DateTime fromDate, DateTime toDate, string auditoriumId)
        {
            var queryParams = new NameValueCollection()
            {
                { "fromDate", fromDate.ToString()},
                { "toDate", toDate.ToString()},
                { "auditoriumId", auditoriumId}

            };
            return await GetAsync<List<FullCalendarEvent>>(
                $"{ApiConstant.ApiScreening}/{ApiConstant.ScreeningGetByAuditorium}", queryParams);
        }
        public async Task<ApiResult<PageResult<ScreeningVMD>>> GetPagingAsync(ScreeningPagingRequest request)
        {
            return await PostAsync<PageResult<ScreeningVMD>>(
                $"{ApiConstant.ApiScreening}/{ApiConstant.ScreeningGetPaging}", request);
        }

        public async Task<ApiResult<ScreeningOfFilmInWeekVMD>> GetListScreeningOfFilmInWeekAsync(string filmId)
        {
            return await GetAsync<ScreeningOfFilmInWeekVMD>($"{ApiConstant.ApiScreening}/" +
                                                            $"{ApiConstant.ScreeningGetListOfFimInWeek}/{filmId}");
        }
    }
}