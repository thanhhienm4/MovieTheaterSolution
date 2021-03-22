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
        public async Task<ApiResult<PageResult<UserVMD>>> GetUserPaging(UserPagingRequest request)
        {
            return await PostAsync<ApiResult<PageResult<UserVMD>>>("/api/User/GetUserPaging", request);
        }

       
    }
}
