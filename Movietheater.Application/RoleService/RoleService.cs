using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Models;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Identity.Role;

namespace MovieTheater.Application.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly MoviesContext _context;
        public RoleService(MoviesContext context)
        {
            _context = context;
        }


        public async Task<ApiResult<List<RoleVMD>>> GetAllAsync()
        {
            var res = await _context.Roles.Select(x => new RoleVMD()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return new ApiSuccessResult<List<RoleVMD>>(res);

        }
    }
}