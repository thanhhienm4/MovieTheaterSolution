using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.FilmServices.Positions
{
    public interface IPositionService
    {
        Task<ApiResult<bool>> CreateAsync(string name);

        Task<ApiResult<bool>> UpdateAsync(PositionUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<List<PositionVMD>>> GetAllPositionAsync();
    }
}