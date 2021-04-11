using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            return await PostAsync<ApiResultLite>("Api/Creening/Create", request);
        }

      
        public async Task<ApiResultLite> UpdateAsync(ScreeningUpdateRequest request)
        {
            return await PutAsync<ApiResultLite>("Api/Screening/Update", request);
        }

       
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            return await DeleteAsync<ApiResultLite>($"Api/Screeing/Delete/{id}");
        }

        public async Task<ApiResult<ScreeningVMD>> GetScreeningByIdAsync(int id)
        {
            return await GetAsync<ApiResult<ScreeningVMD>> ($"Api/Screening/GetScreeningById/{id}");
        }

        
    }
}
