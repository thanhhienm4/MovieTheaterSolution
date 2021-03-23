using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.ScreeningServices
{
    public interface IScreeningService 
    {
        Task<ApiResultLite> CreateAsync(ScreeningCreateRequest request);
        Task<ApiResultLite> UpdateAsync(ScreeningUpdateRequest request);
        Task<ApiResultLite> DeleteAsync(int id);
    }
}
