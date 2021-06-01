using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movietheater.Application.ReservationServices
{
    public interface IReservationService
    {
        Task<ApiResult<bool>> CreateAsync(ReservationCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(ReservationUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<ReservationVMD>> GetReservationById(int Id);

        Task<ApiResult<PageResult<ReservationVMD>>> GetReservationPagingAsync(ReservationPagingRequest request);

        Task<ApiResult<int>> CalPrePriceAsync(List<TicketCreateRequest> tickets);

        Task<ApiResult<List<ReservationVMD>>> GetReservationByUserId(Guid userId);
    }
}