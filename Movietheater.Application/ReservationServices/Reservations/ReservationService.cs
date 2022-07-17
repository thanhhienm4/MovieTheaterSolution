using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Common.Constants;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;

namespace MovieTheater.Application.ReservationServices.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly MoviesContext _context;

        public ReservationService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<int>> CreateAsync(ReservationCreateRequest request)
        {
            if (CheckTickets(request.Tickets) == false)
                return new ApiErrorResult<int>("Tạo mới thất bại");

            var reservation = new Reservation()
            {
                Time = DateTime.Now,
                Active = true,
                EmployeeId = request.EmployeeId,
                Customer = request.CustomerId,
                PaymentStatus = request.Paid,
                TypeId = request.ReservationTypeId
            };

            await _context.Reservations.AddAsync(reservation);
            try
            {
                if (await _context.SaveChangesAsync() == 0)
                {
                    return new ApiErrorResult<int>("Thêm thất bại");
                }

                return new ApiSuccessResult<int>(reservation.Id, "Thêm thành công");
            }
            catch (Exception)
            {
                return new ApiErrorResult<int>("Thêm thất bại");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            Reservation reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                if (reservation.PaymentStatus == PaymentStatusType.Done)
                    return new ApiErrorResult<bool>("Không xóa đơn mua do khách hàng đã thanh toán");

                var ticket = _context.Tickets.Where(x => x.ReservationId == id).ToList();
                _context.Tickets.RemoveRange(ticket);
                _context.SaveChanges();
                _context.Reservations.Remove(reservation);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true, "Xóa thành công");
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
                rv.PaymentStatus = request.Paid;
                rv.Active = request.Active;
                _context.Update(rv);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResult<bool>("Lưu thất bại");
                }

                return new ApiSuccessResult<bool>(true, "Lưu thành công");
            }
        }

        private bool CheckTickets(List<TicketCreateRequest> tickets)
        {
            return true;
        }

        public async Task<ApiResult<PageResult<ReservationVMD>>> GetPagingAsync(ReservationPagingRequest request)
        {
            //var query = from r in _context.Reservations
            //            join c in _context.Customers on r.Customer equals c.Id into rc
            //            from c in rc.DefaultIfEmpty()
            //            join e in _context.Staffs on r.EmployeeId equals e.UserName into rec
            //            from e in rec.DefaultIfEmpty()
            //            join rt in _context.ReservationTypes on r.TypeId equals rt.Id
            //            select new { r, c,rt, e };

            //if(request.userId!=null)
            //{
            //    query = query.Where(x => x.r.CustomerId == request.userId);
            //}

            //if (!string.IsNullOrWhiteSpace(request.Keyword))
            //{
            //   query = query.Where(x => x.r.Id.ToString().Contains(request.Keyword));


            //}

            //int totalRow = await query.CountAsync();
            //var items = query.OrderBy(x => x.r.Time).Skip((request.PageIndex - 1) * request.PageSize)
            //    .Take(request.PageSize).Select(x => new ReservationVMD()
            //    {
            //        Id = x.r.Id,
            //        Paid = x.r.Paid,
            //        Active = x.r.Active,
            //        ReservationType = x.rt.Name,
            //        Time = x.r.Time,
            //        Employee = x.e.LastName + " " + x.e.FirstName,
            //        Customer = x.c.LastName + " " + x.c.FirstName,

            //    }).ToList();
            //foreach(var item in items )
            //{
            //    item.TotalPrice = CallTotal(item.Id);
            //}
            //var pageResult = new PageResult<ReservationVMD>()
            //{
            //    TotalRecord = totalRow,
            //    PageIndex = request.PageIndex,
            //    PageSize = request.PageSize,
            //    Item = items,
            //};

            return new ApiSuccessResult<PageResult<ReservationVMD>>(new PageResult<ReservationVMD>());
        }

        public async Task<ApiResult<ReservationVMD>> GetById(int Id)
        {
            Reservation reservation = await _context.Reservations.FindAsync(Id);
            if (reservation == null)
            {
                return new ApiErrorResult<ReservationVMD>("Không tìm thấy");
            }
            else
            {
                var query = from r in _context.Reservations
                    join c in _context.Customers on r.Customer equals c.Id into rc
                    from c in rc.DefaultIfEmpty()
                    join e in _context.Staffs on r.EmployeeId equals e.UserName into rec
                    from e in rec.DefaultIfEmpty()
                    join rt in _context.ReservationTypes on r.TypeId equals rt.Id
                    where r.Id == Id
                    select new { r, c, rt, e };

                int totalRow = await query.CountAsync();
                var res = query.Select(x => new ReservationVMD()
                {
                    Id = x.r.Id,
                    Paid = x.r.PaymentStatus,
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

        public async Task<List<TicketVMD>> GetTicketsAsync(int reservationId)
        {
            var query = from t in _context.Tickets
                join re in _context.Reservations on t.ReservationId equals re.Id
                join s in _context.Screenings on re.ScreeningId equals s.Id
                join m in _context.Movies on s.MovieId equals m.Id
                join r in _context.Auditoriums on s.AuditoriumId equals r.Id
                join se in _context.Seats on t.Seat.Id equals se.Id
                where t.ReservationId == reservationId
                select new { t, s, m, r, se };

            var tickets = await query.OrderBy(x => x.se.RowId).Select(x => new TicketVMD()
            {
                Film = x.m.Name,
                //Price = x.t.Price,
                Room = x.r.Name,
                Seat = x.se.Number.ToString(),
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

        public Task<ApiResult<List<ReservationVMD>>> GetByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<int>> CalPriceAsync(TicketCreateRequest ticket)
        {
            //var query = from s in _context.Screenings
            //            join ks in _context.KindOfScreenings on s.KindOfScreeningId equals ks.Id
            //            join r in _context.Rooms on s.AuditoriumId equals r.Id
            //            join fr in _context.RoomFormats on r.FormatId equals fr.Id
            //            join se in _context.Seats on r.Id equals se.AuditoriumId
            //            join kse in _context.KindOfSeats on se.TypeId equals kse.Id
            //            where s.Id == ticket.ScreeningId && se.Id == ticket.SeatId

            //            select new { ks, fr, kse };

            //int a = query.Select(x => x.fr.Price).FirstOrDefault();
            //int b = query.Select(x => x.ks.Surcharge).FirstOrDefault();
            //int c = query.Select(x => x.kse.Surcharge).FirstOrDefault();
            //int price = await query.Select(x => x.fr.Price + x.ks.Surcharge + x.kse.Surcharge).FirstOrDefaultAsync();
            //return new ApiSuccessResult<int>(price);
            return new ApiSuccessResult<int>(1);
        }

        public async Task<ApiResult<List<ReservationVMD>>> GetByUserId(string userId)
        {
            var query = from r in _context.Reservations
                join c in _context.Customers on r.Customer equals c.Id into rc
                from c in rc.DefaultIfEmpty()
                join e in _context.Staffs on r.EmployeeId equals e.UserName into rec
                from e in rec.DefaultIfEmpty()
                join rt in _context.ReservationTypes on r.TypeId equals rt.Id
                where c.Id == userId
                select new { r, c, rt, e };

            int totalRow = await query.CountAsync();
            var res = query.Select(x => new ReservationVMD()
            {
                Id = x.r.Id,
                Paid = x.r.PaymentStatus,
                Active = x.r.Active,
                ReservationType = x.rt.Name,
                Time = x.r.Time,
                Employee = x.e.LastName + " " + x.e.FirstName,
                Customer = x.c.LastName + " " + x.c.FirstName
            }).ToList();

            foreach (var reservation in res)
            {
                reservation.Tickets = await GetTicketsAsync(reservation.Id);
            }


            return new ApiSuccessResult<List<ReservationVMD>>(res);
        }

        private long CallTotal(int id)
        {
            if (!_context.Tickets.Any(x => x.ReservationId == id))
                return 0;
            long res = 1; //_context.Tickets.Where(x => x.ReservationId == id).Sum(x => x.Price);
            return res;
        }
    }
}