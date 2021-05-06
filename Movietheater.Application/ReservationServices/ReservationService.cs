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
                Time = DateTime.Now,
                Active = true,
                EmployeeId = request.EmployeeId,
                CustomerId = request.CustomerId,
                Paid = request.Paid,
                ReservationTypeId = request.ReservationTypeId,
                Tickets = request.Tickets.Select(x => new Ticket()
                {
                    ScreeningId = x.ScreeningId,
                    SeatId = x.SeatId,

                }).ToList()
            };

       
            await _context.Reservations.AddAsync(reservattion);
            try
            {
                if (await _context.SaveChangesAsync() == 0)
                {
                    return new ApiErrorResultLite("Thêm thất bại");
                }

                return new ApiSuccessResultLite("Thêm thành công");
            }catch(Exception e)
            {
                return new ApiErrorResultLite("Thêm thất bại");
            }
           

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
            var query = from r in _context.Reservations
                        join c in _context.CustomerInfors on r.CustomerId equals c.Id into rc
                        from c in rc.DefaultIfEmpty()
                        join e in _context.UserInfors on r.EmployeeId equals e.Id into rec 
                        from e in rec.DefaultIfEmpty()
                        join rt in _context.ReservationTypes on r.ReservationTypeId equals rt.Id
                        select new { r, c, rt ,e };


            int totalRow = await query.CountAsync();
            var item = query.OrderBy(x => x.r.Time).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new ReservationVMD()
                {
                    Id = x.r.Id,
                    Paid = x.r.Paid,
                    Active = x.r.Active,
                    ReservationType = x.rt.Name,
                    Time = x.r.Time,
                    Employee = x.e.LastName +" " + x.e.FirstName ,
                    Customer = x.c.LastName + " " + x.c.FirstName
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
                var query = from r in _context.Reservations
                            join c in _context.CustomerInfors on r.CustomerId equals c.Id into rc
                            from c in rc.DefaultIfEmpty()
                            join e in _context.UserInfors on r.EmployeeId equals e.Id into rec
                            from e in rec.DefaultIfEmpty()
                            join rt in _context.ReservationTypes on r.ReservationTypeId equals rt.Id
                            select new { r, c, rt, e };

                int totalRow = await query.CountAsync();
                var res = query.Select(x => new ReservationVMD()
                    {
                        Id = x.r.Id,
                        Paid = x.r.Paid,
                        Active = x.r.Active,
                        ReservationType = x.rt.Name,
                        Time = x.r.Time,
                        Employee = x.e.LastName + " " + x.e.FirstName,
                        Customer = x.c.LastName + " " + x.c.FirstName
                    }).FirstOrDefault();
                return new ApiSuccessResult<ReservationVMD>(res);

              
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
