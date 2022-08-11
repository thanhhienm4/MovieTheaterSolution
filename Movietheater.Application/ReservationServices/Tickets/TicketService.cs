using Microsoft.EntityFrameworkCore;
using MovieTheater.Application.ReservationServices.Reservations;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace MovieTheater.Application.ReservationServices.Tickets
{
    public class TicketService : ITicketService
    {
        private readonly MoviesContext _context;
        private readonly IReservationService _reservationService;

        public TicketService(MoviesContext context, IReservationService reservationService)
        {
            _context = context;
            _reservationService = reservationService;
        }

        public async Task<ApiResult<bool>> CreateAsync(TicketCreateRequest request)
        {
            Ticket ticket = new Ticket()
            {
                SeatId = request.SeatId,
                CustomerType = request.CustomerType,
                ReservationId = request.ReservationId,
                Price = (await _reservationService.CalPriceAsync(request)).ResultObj
            };
            _context.Tickets.Add(ticket);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> DeleteAsync(TicketCreateRequest request)
        {
            Ticket ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.ReservationId == request.ReservationId
                                                                            && request.SeatId == x.SeatId);
            if (ticket == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                _context.Tickets.Remove(ticket);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true);
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public async Task<ApiResult<bool>> UpdateAsync(TicketUpdateRequest request)
        {
            Ticket ticket = await _context.Tickets.FindAsync(request.Id);
            if (ticket == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                ticket.SeatId = request.SeatId;
                ticket.CustomerType = request.CustomerType;

                _context.Update(ticket);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }

                return new ApiSuccessResult<bool>(true);
            }
        }
    }
}