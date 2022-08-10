using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Common.Constants;
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
        {
        }

        public async Task<ApiResult<string>> LoginStaffAsync(LoginRequest request)
        {
            return await PostAsync<string>($"{ApiConstant.ApiUser}/{ApiConstant.UserLogin}", request);
        }

        public async Task<ApiResult<bool>> CreateStaffAsync(UserRegisterRequest request)
        {
            return await PostAsync<bool>($"{ApiConstant.ApiUser}/{ApiConstant.UserRegister}", request);
        }

        public async Task<ApiResult<bool>> UpdateStaffAsync(UserUpdateRequest request)
        {
            return await PutAsync<bool>("Api/User/UpdateStaff", request);
        }

        public async Task<ApiResult<string>> LoginCustomerAsync(LoginRequest request)
        {
            return await PostAsync<string>($"{ApiConstant.ApiCustomer}/{ApiConstant.CustomerLogin}", request);
        }

        public async Task<ApiResult<bool>> CreateCustomerAsync(UserRegisterRequest request)
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

        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            return await DeleteAsync<bool>($"Api/User/Delete/{id}");
        }

        public async Task<ApiResult<UserVMD>> GetUserByIdAsync(string id)
        {
            return await GetAsync<UserVMD>($"Api/User/GetUserById/{id}");
        }

        public async Task<ApiResult<UserVMD>> GetCustomerByIdAsync(string id)
        {
            return await GetAsync<UserVMD>($"Api/User/GetCustomerById/{id}");
        }

        public async Task<ApiResult<bool>> RoleAssignAsync(RoleAssignRequest request)
        {
            return await PutAsync<bool>("Api/User/RoleAssign", request);
        }

        public async Task<ApiResult<bool>> ChangePasswordAsync(ChangePwRequest request)
        {
            return await PutAsync<bool>("Api/User/ChangePassword", request);
        }

        public async Task<ApiResult<bool>> ForgotStaffPasswordAsync(string mail)
        {
            return await PostAsync<bool>("Api/User/ForgotStaffPassword", mail);
        }

        public async Task<ApiResult<bool>> ForgotCustomerPasswordAsync(string mail)
        {
            return await PostAsync<bool>("Api/User/ForgotCustomerPassword", mail);
        }

        public async Task<ApiResult<bool>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return await PostAsync<bool>("Api/User/ResetPassword", request);
        }
    }
}