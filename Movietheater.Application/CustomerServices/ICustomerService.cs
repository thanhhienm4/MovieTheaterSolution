using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.User;
using System;
using System.Threading.Tasks;

namespace MovieTheater.Application.CustomerServices
{
    public interface ICustomerService
    {
        Task<ApiResult<string>> LoginAsync(LoginRequest request);

        Task<ApiResult<bool>> RegisterAsync(UserRegisterRequest request);

        Task<ApiResult<bool>> UpdateAsync(UserUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(Guid id);

        Task<ApiResult<bool>> ChangePasswordAsync(ChangePwRequest request);

        Task<ApiResult<CustomerVMD>> GetById(string id);

        Task<ApiResult<PageResult<CustomerVMD>>> GetUserPagingAsync(UserPagingRequest request);

        ApiResult<bool> CheckToken(Guid userId, string token);

        Task<ApiResult<bool>> ForgotPasswordAsync(string mail);

        Task<ApiResult<bool>> ResetPasswordAsync(ResetPasswordRequest request);
    }
}