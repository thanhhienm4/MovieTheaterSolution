using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.ReservationService.cs
{
    public interface IReservationService
    {
        Task<ApiResultLite> CreateAsync(ReservationCreateRequest request);
        Task<ApiResultLite> UpdateAsync(ReservationUpdateRequest request);
        Task<ApiResultLite> DeleteAsync(int id);
        Task<ApiResult<ReservationVMD>> GetReservationById(int Id);

    }
}
