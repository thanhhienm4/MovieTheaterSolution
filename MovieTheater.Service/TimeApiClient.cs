using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Catalog.Price.Time;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Infra.RoomModels;
using MovieTheater.Models.Infra.RoomModels.Format;

namespace MovieTheater.Api
{
    public class TimeApiClient : BaseApiClient
    {
        public TimeApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<bool>> CreateAsync(TimeCreateRequest request)
        {
            return await PostAsync<bool>($"{ApiConstant.ApiTime}/{ApiConstant.TimeCreate}", request);
        }

        public async Task<ApiResult<bool>> UpdateAsync(TimeUpdateRequest request)
        {
            return await PostAsync<bool>($"{ApiConstant.ApiTime}/{ApiConstant.TimeUpdate}", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            return await DeleteAsync<bool>($"{ApiConstant.ApiTime}/{ApiConstant.TimeDelete}/{id}");
        }

        public async Task<ApiResult<TimeVMD>> GetByIdAsync(string id)
        {
            var queryParams = new NameValueCollection()
            {
                { "id", id }
            };
            return await GetAsync<TimeVMD>($"{ApiConstant.ApiTime}/{ApiConstant.TimeGetById}", queryParams);
        }

        public async Task<ApiResult<List<TimeVMD>>> GetAllAsync()
        {
            return await GetAsync<List<TimeVMD>>($"{ApiConstant.ApiTime}/{ApiConstant.TimeGetAll}");
        }

        public async Task<ApiResult<PageResult<TimeVMD>>> GetPagingAsync(TimePagingRequest request)
        {
            return await PostAsync<PageResult<TimeVMD>>($"{ApiConstant.ApiTime}/{ApiConstant.TimePaging}", request);
        }
    }
}