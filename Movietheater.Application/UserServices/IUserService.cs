using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.User;
using System;
using System.Threading.Tasks;
using MovieTheater.Data.Models;

namespace MovieTheater.Application.UserServices
{
    public interface IUserService
    {
        Task<ApiResult<string>> LoginStaffAsync(LoginRequest request);

        Task<ApiResult<Guid>> CreateStaffAsync(UserCreateRequest model);

        Task<ApiResult<bool>> UpdateStaffAsync(UserUpdateRequest model);

        Task<ApiResult<string>> LoginCustomerAsync(LoginRequest request);

        Task<ApiResult<bool>> CreateCustomerAsync(UserCreateRequest model);

        Task<ApiResult<bool>> UpdateCustomerAsync(UserUpdateRequest model);

        Task<ApiResult<bool>> DeleteAsync(Guid id);

        Task<ApiResult<bool>> ChangePasswordAsync(ChangePwRequest request);

        Task<ApiResult<bool>> RoleAssignAsync(RoleAssignRequest request);

        Task<ApiResult<UserVMD>> GetUserByIdAsync(Guid id);

        Task<ApiResult<UserVMD>> GetCustomerByIdAsync(Guid id);

        Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request);
        ApiResult<bool> CheckToken(Guid userId, string token);
        Task<ApiResult<bool>> ForgotStaffPasswordAsync(string mail);
        Task<ApiResult<bool>> ForgotCustomerPasswordAsync(string mail);
        Task<ApiResult<bool>> ResetPasswordAsync(ResetPasswordRequest request);
        
    }
}