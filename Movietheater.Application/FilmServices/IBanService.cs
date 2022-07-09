using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.FilmServices
{
    public interface IBanService
    {
        Task<ApiResult<bool>> CreateAsync(string name);

        Task<ApiResult<bool>> UpdateAsync(BanUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<List<BanVMD>>> GetAllBanAsync();
    }
}