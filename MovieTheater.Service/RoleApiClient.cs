using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class RoleApiClient : BaseApiClient
    {
        public RoleApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
        }

        public async Task<ApiResult<List<RoleVMD>>> GetRolesAsync()
        {
            return await GetAsync<List<RoleVMD>>("/api/Role/GetAll");
        }

        public async Task<ApiResult<List<RoleVMD>>> GetRolesOfUserAsync(Guid userId)
        {
            return await GetAsync<List<RoleVMD>>($"/api/Role/User/{userId}");
        }
    }
}