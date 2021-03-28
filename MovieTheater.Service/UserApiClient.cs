using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class UserApiClient : BaseApiClient
    {
        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor):base(httpClientFactory,  configuration,
             httpContextAccessor)
        { }
        public async Task<ApiResult<string>> LoginAsync(LoginRequest request)
        {
            return await PostAsync<ApiResult<string>>("/api/User/Login", request);
        }
        public async Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request)
        {
            return await PostAsync<ApiResult<PageResult<UserVMD>>>("/api/User/GetUserPaging", request);
        }
        public async Task<ApiResultLite> CreateAsync(UserCreateRequest request)
        {
            return await PostAsync<ApiResultLite>("Api/User/Create", request);
        }
        public async Task<ApiResultLite> UpdateAsync(UserUpdateRequest request)
        {
            return await PutAsync<ApiResultLite>("Api/User/Update", request);
        }
        public async Task<ApiResultLite> DeleteAsync(Guid id)
        {
            return await DeleteAsync<ApiResultLite>($"Api/User/Delete/{id}");
        }
        public async Task<ApiResult<UserVMD>> GetUserByIdAsync(Guid id)
        {
            return await GetAsync<ApiResult<UserVMD>> ($"Api/User/GetUserById/{id}");
        }
       
    }
}
