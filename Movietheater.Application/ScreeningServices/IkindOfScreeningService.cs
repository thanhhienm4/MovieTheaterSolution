using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.ScreeningServices
{
    public interface IkindOfScreeningService
    {
        Task<ApiResultLite> CreateAsync(KindOfScreeningCreateRequest request);
        Task<ApiResultLite> UpdateAsync(KindOfScreeningUpdateRequest request);
        Task<ApiResultLite> DeleteAsync(int id);
        Task<ApiResult<List<KindOfScreeningVMD>>> GetAllKindOfScreeningAsync();
    }
}
