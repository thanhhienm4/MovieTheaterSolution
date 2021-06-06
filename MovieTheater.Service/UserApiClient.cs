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
            return await PostAsync<string>("/api/Login/LoginStaff", request);
        }

        public async Task<ApiResult<Guid>> CreateStaffAsync(UserCreateRequest request)
        {
            return await PostAsync<Guid>("Api/User/CreateStaff", request);
        }

        public async Task<ApiResult<bool>> UpdateStaffAsync(UserUpdateRequest request)
        {
            return await PutAsync<bool>("Api/User/UpdateStaff", request);
        }

        public async Task<ApiResult<string>> LoginCustomerAsync(LoginRequest request)
        {
            return await PostAsync<string>("/api/Login/LoginCustomer", request);
        }

        public async Task<ApiResult<bool>> CreateCustomerAsync(UserCreateRequest request)
        {
            return await PostAsync<bool>("Api/User/CreateCustomer", request);
        }

        public async Task<ApiResult<bool>> UpdateCustomerAsync(UserUpdateRequest request)
        {
            return await PutAsync<bool>("Api/User/UpdateCustomer", request);
        }

        public async Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request)
        {
            return await PostAsync<PageResult<UserVMD>>("/api/User/GetUserPaging", request);
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            return await DeleteAsync<bool>($"Api/User/Delete/{id}");
        }

        public async Task<ApiResult<UserVMD>> GetUserByIdAsync(Guid id)
        {
            return await GetAsync<UserVMD>($"Api/User/GetUserById/{id}");
        }

        public async Task<ApiResult<UserVMD>> GetCustomerByIdAsync(Guid id)
        {
            return await GetAsync<UserVMD>($"Api/User/GetCustomerById/{id}");
        }

        public async Task<ApiResult<bool>> RoleAssignAsync(RoleAssignRequest request)
        {
            return await PutAsync<bool>("Api/User/RoleAssign", request);
        }

        public async Task<ApiResult<bool>> ChangePasswordAsync(ChangePWRequest request)
        {
            return await PutAsync<bool>("Api/User/ChangePassword", request);
        }
        
         public async Task<ApiResult<bool>> ForgotPasswordAsync(string mail)
        {
            return await PostAsync<bool>("Api/User/ForgotPassword", mail);
        }
        public async Task<ApiResult<bool>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return await PostAsync<bool>("Api/User/ResetPassword", request);
        }
        
    }
}