using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.ReservationServices
{
    public interface IReservationService
    {
        Task<ApiResultLite> CreateAsync(ReservationCreateRequest request);
        Task<ApiResultLite> UpdateAsync(ReservationUpdateRequest request);
        Task<ApiResultLite> DeleteAsync(int id);
        Task<ApiResult<ReservationVMD>> GetReservationById(int Id);
        Task<ApiResult<PageResult<ReservationVMD>>> GetReservationPagingAsync(ReservationPagingRequest request);


    }
}
