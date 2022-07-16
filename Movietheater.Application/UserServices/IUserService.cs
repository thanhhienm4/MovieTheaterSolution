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
        Task<ApiResult<string>> LoginAsync(LoginRequest request);

        Task<ApiResult<bool>> CreateAsync(UserRegisterRequest model);

        Task<ApiResult<bool>> UpdateAsync(UserUpdateRequest model);

        Task<ApiResult<bool>> DeleteAsync(Guid id);

        Task<ApiResult<bool>> ChangePasswordAsync(ChangePwRequest request);

        Task<ApiResult<bool>> RoleAssignAsync(RoleAssignRequest request);

        Task<ApiResult<UserVMD>> GetUserByIdAsync(Guid id);

        Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request);

        Task<ApiResult<bool>> ForgotPasswordAsync(string mail);

        Task<ApiResult<bool>> ResetPasswordAsync(ResetPasswordRequest request);
        
    }
}