using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Common.Constants;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.User;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTheater.Api
{
    public class CustomerApiClient : BaseApiClient
    {
        public CustomerApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        {
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
            return await PutAsync<bool>($"{ApiConstant.ApiCustomer}/{ApiConstant.CustomerUpdate}", request);
        }


        public async Task<ApiResult<UserVMD>> GetCustomerByIdAsync(string id)
        {
            return await GetAsync<UserVMD>($"{ApiConstant.ApiCustomer}/{ApiConstant.CustomerGetById}/{id}");
        }

        public async Task<ApiResult<bool>> ChangePasswordAsync(ChangePwRequest request)
        {
            return await PutAsync<bool>("Api/User/ChangePassword", request);
        }

        public async Task<ApiResult<bool>> ForgotCustomerPasswordAsync(string mail)
        {
            return await PostAsync<bool>($"{ApiConstant.ApiCustomer}/{ApiConstant.CustomerForgotPassword}", mail);
        }

    }
}