using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public interface IPositionService
    {
        Task<ApiResultLite> CreateAsync(string name);
        Task<ApiResultLite> UpdateAsync(PositionUpdateRequest request);
        Task<ApiResultLite> DeleteAsync(int id);
        Task<ApiResult<List<PositionVMD>>> GetAllPositionAsync();
    }
}
