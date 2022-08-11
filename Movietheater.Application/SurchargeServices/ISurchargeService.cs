using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using MovieTheater.Models.Price.Surcharge;
using System.Threading.Tasks;

namespace MovieTheater.Application.SurchargeServices
{
    public interface ISurchargeService
    {
        Task<ApiResult<bool>> CreateAsync(SurchargeCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(SurchargeUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<PageResult<SurchargeVmd>>> GetSurchargePagingAsync(SurChargePagingRequest request);

        Task<ApiResult<SurchargeVmd>> GetSurchargeById(int id);
    }
}