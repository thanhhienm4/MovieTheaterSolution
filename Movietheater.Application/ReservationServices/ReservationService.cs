using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
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
    public class ReservationService : IReservationService
    {
        private readonly MovieTheaterDBContext _context;
        public ReservationService(MovieTheaterDBContext context)
        {
            _context = context;
        }
        public async Task<ApiResultLite> CreateAsync(ReservationCreateRequest request)
        {
            if (CheckTickets(request.Tickets) == false)
                return new ApiErrorResultLite("Tạo mới thất bại");

            var reservattion = new Reservation()
            {
                Active = true,
                EmployeeId = request.EmployeeId,
                UserId = request.UserId,
                Paid = request.Paid,
                ReservationTypeId = request.ReservationTypeId,
                Tickets = request.Tickets.Select(x => new Ticket()
                {
                    ScreeningId = x.ScreeningId,
                    SeatId = x.SeatId,

                }).ToList()
            };

            await _context.Reservations.AddAsync(reservattion);
            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResultLite("Thêm thất bại");
            }

            return new ApiSuccessResultLite("Thêm thành công");

        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            Reservation rv = await _context.Reservations.FindAsync(id);
            if (rv == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                _context.Reservations.Remove(rv);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResultLite("Xóa thành công");
                }
                else return new ApiSuccessResultLite("Không xóa được");
            }
        }

      

        public async Task<ApiResultLite> UpdateAsync(ReservationUpdateRequest request)
        {
            Reservation rv = await _context.Reservations.FindAsync(request.Id);
            if (rv == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                rv.Id = request.Id;
                rv.Paid = request.Paid;
                rv.Active = request.Active;
                _context.Update(rv);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResultLite("Cập nhật thất bại");
                }
                return new ApiSuccessResultLite("Cập nhật thành công");
            }
        }
        

        private bool CheckTickets(List<TicketCreateRequest> tickets)
        {
            var query = _context.Tickets.Join(tickets,
                x => new { x.ScreeningId, x.SeatId },
                y => new { y.ScreeningId, y.SeatId },
                (x, y) => new { x });

            if (query != null)
                return true;
            else
                return false;


        }
        public async Task<ApiResult<PageResult<ReservationVMD>>> GetReservationPagingAsync(ReservationPagingRequest request)
        {
            var reservations = _context.Reservations.Select(x => x);
            int totalRow = await reservations.CountAsync();
            var item = reservations.OrderBy(x => x.Employee).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new ReservationVMD()
                {
                    Id = x.Id,
                    Paid = x.Paid,
                    Active = x.Active,
                    ReservationTypeId = x.ReservationTypeId,
                    UserId = x.UserId,
                    EmployeeId = x.EmployeeId
                }).ToList();

            var pageResult = new PageResult<ReservationVMD>()
            {

                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = item,
            };

            return new ApiSuccessResult<PageResult<ReservationVMD>>(pageResult);
        }

        public async Task<ApiResult<ReservationVMD>> GetReservationById(int Id)
        {
            Reservation reservation = await _context.Reservations.FindAsync(Id);
            if (reservation == null)
            {
                return new ApiErrorResult<ReservationVMD>("Không tìm thấy");
            }
            else
            {
                var result = new ReservationVMD()
                {
                    Id = reservation.Id,
                    Paid = reservation.Paid,
                    Active = reservation.Active,
                    EmployeeId = reservation.EmployeeId,
                    UserId = reservation.UserId,
                    ReservationTypeId = reservation.ReservationTypeId
                };
                return new ApiSuccessResult<ReservationVMD>(result);
            }
        }

        public async Task<int> CalPrePriceAsync(List<TicketCreateRequest> tickets)
        {
            int total = 0;
            if(tickets!=null)
            {
                foreach(var ticket in tickets)
                {
                    total += await CalPriceAsync(ticket);
                }
            }
            return total;

        }
        public async Task<int> CalPriceAsync(TicketCreateRequest ticket)
        {
            var query = from s in _context.Screenings
                        join ks in _context.KindOfScreenings on s.KindOfScreeningId equals ks.Id
                        join r in _context.Rooms on s.RoomId equals r.Id
                        join fr in _context.RoomFormats on r.FormatId equals fr.Id
                        join se in _context.Seats on r.Id equals se.RoomId
                        join kse in _context.KindOfSeats on se.KindOfSeatId equals kse.Id
                        where s.Id == ticket.ScreeningId && se.Id == ticket.SeatId

                        select new {ks,fr,kse};

            int a = query.Select(x => x.fr.Price).FirstOrDefault();
            int b = query.Select(x => x.ks.Surcharge).FirstOrDefault();
            int c = query.Select(x => x.kse.Surcharge).FirstOrDefault();
            int price =await query.Select(x => x.fr.Price + x.ks.Surcharge + x.kse.Surcharge).FirstOrDefaultAsync();
            return price;
        }
    }
}
