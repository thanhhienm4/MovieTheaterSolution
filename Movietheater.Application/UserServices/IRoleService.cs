using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movietheater.Application.UserServices
{
    public interface IRoleService
    {
        Task<ApiResult<bool>> CreateAsync(RoleCreateRequest model);

        Task<ApiResult<bool>> DeleteAsync(string id);

        Task<ApiResult<bool>> UpdateAsync(RoleUpdateRequest model);

        Task<ApiResult<List<RoleVMD>>> GetAllRoles();
    }
}