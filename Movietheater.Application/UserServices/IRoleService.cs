using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.UserServices
{
    public interface IRoleService
    {
        Task<ApiResultLite> CreateAsync(RoleCreateRequest model);
        Task<ApiResultLite> DeleteAsync(string id);
        Task<ApiResultLite> UpdateAsync(RoleUpdateRequest model);
        Task<List<RoleVMD>> GetAllRoles();
    }
}
