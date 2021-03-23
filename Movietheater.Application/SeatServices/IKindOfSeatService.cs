using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.KindOfSeat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.SeatServices
{
    public interface IKindOfSeatService
    {
        Task<ApiResultLite> CreateAsync(KindOfSeatCreateRequest request);
        Task<ApiResultLite> UpdateAsync(KindOfSeatUpdateRequest request);
        Task<ApiResultLite> DeleteAsync(int Id);

    }
}
