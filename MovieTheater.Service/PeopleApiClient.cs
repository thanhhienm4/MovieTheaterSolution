using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class PeopleApiClient : BaseApiClient
    {
        public PeopleApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
           IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        { }

        public async Task<ApiResult<bool>> CreateAsync(ActorCreateRequest request)
        {
            return await PostAsync<bool>("Api/People/Create", request);
        }

        public async Task<ApiResult<bool>> UpdateAsync(ActorUpdateRequest request)
        {
            return await PutAsync<bool>("Api/People/Update", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            return await DeleteAsync<bool>($"Api/People/Delete/{id}");
        }

        public async Task<ApiResult<PageResult<ActorVMD>>> GetPeoplePagingAsync(ActorPagingRequest request)
        {
            return await PostAsync<PageResult<ActorVMD>>($"Api/People/GetPeoplePaging", request);
        }

        public async Task<ApiResult<ActorVMD>> GetPeopleByIdAsync(int id)
        {
            return await GetAsync<ActorVMD>($"Api/People/GetPeopleById/{id}");
        }

        public async Task<ApiResult<List<ActorVMD>>> GetAllPeopleAsync()
        {
            return await GetAsync<List<ActorVMD>>($"Api/People/GetAllPeople");
        }
    }
}