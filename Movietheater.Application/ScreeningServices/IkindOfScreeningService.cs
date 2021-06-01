using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movietheater.Application.ScreeningServices
{
    public interface IkindOfScreeningService
    {
        Task<ApiResult<bool>> CreateAsync(KindOfScreeningCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(KindOfScreeningUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<List<KindOfScreeningVMD>>> GetAllKindOfScreeningAsync();
    }
}