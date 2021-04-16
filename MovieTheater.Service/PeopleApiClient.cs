using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class PeopleApiClient : BaseApiClient
    {
        public PeopleApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
           IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        { }
        public async Task<ApiResultLite> CreateAsync(PeopleCreateRequest request)
        {
            return await PostAsync<ApiResultLite>("Api/People/Create", request);
        }
        public async Task<ApiResultLite> UpdateAsync(PeopleUpdateRequest request)
        {
            return await PutAsync<ApiResultLite>("Api/People/Update", request);
        }
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            return await DeleteAsync<ApiResultLite>($"Api/People/Delete/{id}");
        }
        public async Task<ApiResult<PageResult<PeopleVMD>>> GetPeoplePagingAsync(PeoplePagingRequest request)
        {
            return await PostAsync<ApiResult<PageResult<PeopleVMD>>>($"Api/People/GetPeoplePaging", request);
        }
        public async Task<ApiResult<PeopleVMD>> GetPeopleByIdAsync(int id)
        {
            return await GetAsync<ApiResult<PeopleVMD>>($"Api/People/GetPeopleById/{id}");
        }
    }
}
