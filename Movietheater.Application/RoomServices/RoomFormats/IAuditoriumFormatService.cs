using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Application.RoomServices.RoomFormats
{
    public interface IAuditoriumFormatService
    {
        Task<ApiResult<bool>> CreateAsync(AuditoriumFormatCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(AuditoriumFormatUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<List<AuditoriumFormatVMD>>> GetAllAsync();

        Task<ApiResult<AuditoriumFormatVMD>> GetByIdAsync(string id);
    }
}