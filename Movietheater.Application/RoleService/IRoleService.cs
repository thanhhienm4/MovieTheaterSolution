using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;

namespace MovieTheater.Application.RoleService
{
    public interface IRoleService
    {
        Task<ApiResult<List<RoleVMD>>> GetAllAsync();

    }
}