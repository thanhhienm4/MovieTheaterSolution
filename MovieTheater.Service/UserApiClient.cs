using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.User;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class UserApiClient : BaseApiClient
    {
        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
             httpContextAccessor)
        { }

        public async Task<ApiResult<string>> LoginStaffAsync(LoginRequest request)
        {
            return await PostAsync<ApiResult<string>>("/api/User/LoginStaff", request);
        }

        public async Task<ApiResultLite> CreateStaffAsync(UserCreateRequest request)
        {
            return await PostAsync<ApiResultLite>("Api/User/CreateStaff", request);
        }

        public async Task<ApiResultLite> UpdateStaffAsync(UserUpdateRequest request)
        {
            return await PutAsync<ApiResultLite>("Api/User/UpdateStaff", request);
        }

        public async Task<ApiResult<string>> LoginCustomerAsync(LoginRequest request)
        {
            return await PostAsync<ApiResult<string>>("/api/User/LoginCustomer", request);
        }

        public async Task<ApiResultLite> CreateCustomerAsync(UserCreateRequest request)
        {
            return await PostAsync<ApiResultLite>("Api/User/CreateCustomer", request);
        }

        public async Task<ApiResultLite> UpdateCustomerAsync(UserUpdateRequest request)
        {
            return await PutAsync<ApiResultLite>("Api/User/UpdateCustomer", request);
        }

        public async Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request)
        {
            return await PostAsync<ApiResult<PageResult<UserVMD>>>("/api/User/GetUserPaging", request);
        }

        public async Task<ApiResultLite> DeleteAsync(Guid id)
        {
            return await DeleteAsync<ApiResultLite>($"Api/User/Delete/{id}");
        }

        public async Task<ApiResult<UserVMD>> GetUserByIdAsync(Guid id)
        {
            return await GetAsync<ApiResult<UserVMD>>($"Api/User/GetUserById/{id}");
        }

        public async Task<ApiResult<UserVMD>> GetCustomerByIdAsync(Guid id)
        {
            return await GetAsync<ApiResult<UserVMD>>($"Api/User/GetCustomerById/{id}");
        }

        public async Task<ApiResultLite> RoleAssignAsync(RoleAssignRequest request)
        {
            return await PutAsync<ApiResultLite>("Api/User/RoleAssign", request);
        }

        public async Task<ApiResultLite> ChangePasswordAsync(ChangePWRequest request)
        {
            return await PutAsync<ApiResultLite>("Api/User/ChangePassword", request);
        }
    }
}