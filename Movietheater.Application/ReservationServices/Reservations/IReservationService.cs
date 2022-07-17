using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;

namespace MovieTheater.Application.ReservationServices.Reservations
{
    public interface IReservationService
    {
        Task<ApiResult<int>> CreateAsync(ReservationCreateRequest request);

        Task<ApiResult<bool>> UpdateAsync(ReservationUpdateRequest request);

        Task<ApiResult<bool>> DeleteAsync(int id);

        Task<ApiResult<ReservationVMD>> GetById(int id);

        Task<ApiResult<PageResult<ReservationVMD>>> GetPagingAsync(ReservationPagingRequest request);

        Task<ApiResult<int>> CalPrePriceAsync(List<TicketCreateRequest> tickets);

        Task<ApiResult<List<ReservationVMD>>> GetByUserId(Guid userId);
    }
}