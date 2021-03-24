using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Identity.Role;
using MovieTheater.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.UserServices
{
    public interface IUserService
    {
        Task<ApiResult<string>> LoginAsync(LoginRequest request);

        Task<ApiResultLite> CreateAsync(UserCreateRequest model);
        Task<ApiResultLite> UpdateAsync(UserUpdateRequest model);
        Task<ApiResultLite> DeleteAsync(Guid Id);
        Task<ApiResultLite> ChangePasswordAsync(ChangePWRequest request);
        Task<ApiResultLite> RoleAssignAsync(RoleAssignRequest request);
        Task<ApiResult<UserVMD>> GetUserByIdAsync(string id);
        Task<ApiResult<PageResult<UserVMD>>> GetUserPagingAsync(UserPagingRequest request);
    }
}
