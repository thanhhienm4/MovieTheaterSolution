using MovieTheater.Data.EF;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;
using MovieTheater.Data.Models;

namespace MovieTheater.Application.ReservationServices.Tickets
{
    public class TicketService : ITicketService
    {
        private readonly MoviesContext _context;

        public TicketService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(TicketCreateRequest request)
        {
            Ticket ticket = new Ticket()
            {
                SeatId = request.SeatId,

            };
            _context.Tickets.Add(ticket);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            Ticket ticket = await _context.Tickets.FindAsync(id);
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