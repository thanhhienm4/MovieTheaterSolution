using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ApiResult<int>> CreateAsync(ReservationCreateRequest request)
        {
            if (CheckTickets(request.Tickets) == false)
                return new ApiErrorResult<int>("Tạo mới thất bại");

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
                    return new ApiErrorResult<int>("Thêm thất bại");
                }

                return new ApiSuccessResult<int>(reservattion.Id,"Thêm thành công");
            }
            catch (Exception)
            {
                return new ApiErrorResult<int>("Thêm thất bại");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            Reservation rv = await _context.Reservations.FindAsync(id);
            if (rv == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                if(rv.Paid == true)
                    return new ApiErrorResult<bool>("Không xóa đơn mua do khách hàng đã thanh toán");

                var ticket = _context.Tickets.Where(x => x.ReservationId == id).ToList();
                _context.Tickets.RemoveRange(ticket);
                _context.SaveChanges();
                _context.Reservations.Remove(rv);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true,"Xóa thành công");
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public async Task<ApiResult<bool>> UpdateAsync(ReservationUpdateRequest request)
        {
            Reservation rv = await _context.Reservations.FindAsync(request.Id);
            if (rv == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
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
                    return new ApiErrorResult<bool>("Lưu thất bại");
                }
                return new ApiSuccessResult<bool>(true,"Lưu thành công");
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
                        join u in _context.Users on r.EmployeeId equals u.Id into recu
                        from u in recu.DefaultIfEmpty()
                        join rt in _context.ReservationTypes on r.ReservationTypeId equals rt.Id
                        select new { r, c,rt, e, u };

            if(request.userId!=null)
            {
                query = query.Where(x => x.r.CustomerId == request.userId);
            }

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
               query = query.Where(x => x.r.Id.ToString().Contains(request.Keyword));
                                         
                                      
            }

            int totalRow = await query.CountAsync();
            var items = query.OrderBy(x => x.r.Time).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new ReservationVMD()
                {
                    Id = x.r.Id,
                    Paid = x.r.Paid,
                    Active = x.r.Active,
                    ReservationType = x.rt.Name,
                    Time = x.r.Time,
                    Employee = x.e.LastName + " " + x.e.FirstName,
                    Customer = x.c.LastName + " " + x.c.FirstName,
                  
                }).ToList();
            foreach(var item in items )
            {
                item.TotalPrice = CallTotal(item.Id);
            }
            var pageResult = new PageResult<ReservationVMD>()
            {
                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = items,
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
                            where r.Id == Id
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
                    Customer = x.c.LastName + " " + x.c.FirstName,
                    
                }).FirstOrDefault();
                res.TotalPrice = CallTotal(res.Id);
                res.Tickets = await GetTicketsAsync(Id);
                return new ApiSuccessResult<ReservationVMD>(res);
            }
        }

        public async Task<List<TicketVMD>> GetTicketsAsync(int reserId)
        {
            var query = from t in _context.Tickets
                        join s in _context.Screenings on t.ScreeningId equals s.Id
                        join f in _context.Films on s.FilmId equals f.Id
                        join r in _context.Rooms on s.RoomId equals r.Id
                        join se in _context.Seats on t.SeatId equals se.Id
                        where t.ReservationId == reserId
                        select new { t, s, f, r, se };

            var tickets = await query.OrderBy(x => x.se.Name).Select(x => new TicketVMD()
            {
                Film = x.f.Name,
                Price = x.t.Price,
                Room = x.r.Name,
                Seat = x.se.Name,
                Time = x.s.StartTime
            }).ToListAsync();

            return tickets;
        }

        public async Task<ApiResult<int>> CalPrePriceAsync(List<TicketCreateRequest> tickets)
        {
            int total = 0;
            if (tickets != null)
            {
                foreach (var ticket in tickets)
                {
                    total += (await CalPriceAsync(ticket)).ResultObj;
                }
            }
            return new ApiSuccessResult<int>(total);
        }

        public async Task<ApiResult<int>> CalPriceAsync(TicketCreateRequest ticket)
        {
            var query = from s in _context.Screenings
                        join ks in _context.KindOfScreenings on s.KindOfScreeningId equals ks.Id
                        join r in _context.Rooms on s.RoomId equals r.Id
                        join fr in _context.RoomFormats on r.FormatId equals fr.Id
                        join se in _context.Seats on r.Id equals se.RoomId
                        join kse in _context.KindOfSeats on se.KindOfSeatId equals kse.Id
                        where s.Id == ticket.ScreeningId && se.Id == ticket.SeatId

                        select new { ks, fr, kse };

            int a = query.Select(x => x.fr.Price).FirstOrDefault();
            int b = query.Select(x => x.ks.Surcharge).FirstOrDefault();
            int c = query.Select(x => x.kse.Surcharge).FirstOrDefault();
            int price = await query.Select(x => x.fr.Price + x.ks.Surcharge + x.kse.Surcharge).FirstOrDefaultAsync();
            return new ApiSuccessResult<int>(price);
        }

        public async Task<ApiResult<List<ReservationVMD>>> GetReservationByUserId(Guid userId)
        {
            var query = from r in _context.Reservations
                        join c in _context.CustomerInfors on r.CustomerId equals c.Id into rc
                        from c in rc.DefaultIfEmpty()
                        join e in _context.UserInfors on r.EmployeeId equals e.Id into rec
                        from e in rec.DefaultIfEmpty()
                        join rt in _context.ReservationTypes on r.ReservationTypeId equals rt.Id
                        where c.Id == userId
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
            }).ToList();

            foreach(var reservation in res)
            {
                reservation.Tickets = await GetTicketsAsync(reservation.Id);
            }
           
            
            return new ApiSuccessResult<List<ReservationVMD>>(res);
        }
        private long  CallTotal(int id)
        {
            if (_context.Tickets.Where(x => x.ReservationId == id).Count() == 0)
                return 0;
            long res = _context.Tickets.Where(x => x.ReservationId == id).Sum(x => x.Price);
            return res;
        }
    }
}