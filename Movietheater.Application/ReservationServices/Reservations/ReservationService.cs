﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieTheater.Application.Common;
using MovieTheater.Common.Constants;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace MovieTheater.Application.ReservationServices.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly MoviesContext _context;
        private readonly IConfiguration _configuration;

        public ReservationService(MoviesContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ApiResult<int>> CreateAsync(ReservationCreateRequest request)
        {
            var reservation = new Reservation()
            {
                Time = DateTime.Now,
                Active = true,
                EmployeeId = request.EmployeeId,
                Customer = request.CustomerId,
                PaymentStatus = request.Paid,
                TypeId = request.ReservationTypeId,
                ScreeningId = request.ScreeningId
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
                await _context.SaveChangesAsync();
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
            var query = from r in _context.Reservations
                        join c in _context.Customers on r.Customer equals c.Id into rc
                        from c in rc.DefaultIfEmpty()
                        join s in _context.Staffs on r.EmployeeId equals s.UserName into rec
                        from s in rec.DefaultIfEmpty()
                        join rt in _context.ReservationTypes on r.TypeId equals rt.Id
                        join sr  in _context.Screenings on r.ScreeningId equals sr.Id
                        join m in _context.Movies on sr.MovieId equals m.Id
                        select new { r, c, rt, s, m, sr };

            if (request.userId != null)
            {
                query = query.Where(x => x.r.Customer == request.userId);
            }

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                query = query.Where(x => x.r.Id.ToString().Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();
            var items = query.OrderByDescending(x => x.r.Time).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new ReservationVMD()
                {
                    Id = x.r.Id,
                    Paid = x.r.PaymentStatus,
                    Active = x.r.Active,
                    ReservationType = x.rt.Name,
                    Time = x.r.Time,
                    Employee = x.s.LastName + " " + x.s.FirstName,
                    CustomerName = x.c.LastName + " " + x.c.FirstName,
                    MovieName = x.m.Name,
                    Poster = $"{_configuration["BackEndServer"]}/" +
                             $"{FileStorageService.UserContentFolderName}/{x.m.Poster}",
                    PaidName = x.r.PaymentStatusNavigation.Name

                }).ToList();
            foreach (var reservation in items)
            {
                reservation.Tickets = await GetTicketsAsync(reservation.Id);
            }
            foreach (var item in items)
            {
                item.TotalPrice = item.Tickets.Sum(x => x.Price);
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

                var res = query.Select(x => new ReservationVMD()
                {
                    Id = x.r.Id,
                    Paid = x.r.PaymentStatus,
                    Active = x.r.Active,
                    ReservationType = x.rt.Name,
                    Time = x.r.Time,
                    Employee = x.e.LastName + " " + x.e.FirstName,
                    CustomerName = x.c.LastName + " " + x.c.FirstName,
                    ScreeningId = x.r.ScreeningId,
                    MovieName = x.r.Screening.Movie.Name,
                    StartTime = x.r.Screening.StartTime,
                    AuditoriumId = x.r.Screening.AuditoriumId,
                    AuditoriumFormatName = x.r.Screening.Auditorium.Format.Name,
                    Customer = x.r.Customer
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
                Price = x.t.Price,
                Room = x.r.Name,
                Seat = x.se.Row.Name + x.se.Number.ToString(),
                Time = x.s.StartTime,
                CustomerType = x.t.CustomerTypeNavigation.Name,
                SeatType = x.se.Type.Name,
            }).ToListAsync();

            return tickets;
        }

        public async Task<ApiResult<decimal>> CalPrePriceAsync(List<TicketCreateRequest> tickets)
        {
            decimal total = 0;
            if (tickets != null)
            {
                foreach (var ticket in tickets)
                {
                    total += (await CalPriceAsync(ticket)).ResultObj;
                }
            }

            return new ApiSuccessResult<decimal>(total);
        }

        public Task<ApiResult<List<ReservationVMD>>> GetByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<Decimal>> CalPriceAsync(TicketCreateRequest ticket)
        {
            var parameterReturn = new SqlParameter
            {
                ParameterName = "ReturnValue",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Direction = System.Data.ParameterDirection.Output,
            };
            var reservationParam = new SqlParameter("@reservationId", ticket.ReservationId);
            var seatParam = new SqlParameter("@SeatId", ticket.SeatId);
            var customerParam = new SqlParameter("@customerType", ticket.CustomerType);


            var data = await _context.Database.ExecuteSqlRawAsync(
                "EXEC @returnValue = [dbo].[CalPrePrice] @reservationId, @SeatId, @customerType", parameterReturn,
                reservationParam, seatParam, customerParam);

            return new ApiSuccessResult<Decimal>((decimal)parameterReturn.Value);
        }

        public async Task<ApiResult<bool>> UpdatePaymentStatus(ReservationUpdatePaymentRequest request)
        {
            Reservation rv = await _context.Reservations.FindAsync(request.Id);
            if (rv == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                rv.PaymentStatus = request.Status;
                _context.Update(rv);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResult<bool>("Lưu thất bại");
                }

                return new ApiSuccessResult<bool>(true, "Lưu thành công");
            }
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
                Employee = x.e.LastName + x.e.FirstName,
                CustomerName = x.c.LastName + " " + x.c.FirstName,
                
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
            long res = (long)_context.Tickets.Where(x => x.ReservationId == id).Sum(x => x.Price);
            return res;
        }

        public TimeSpan GetTimeDiffOfWeek(DateTime time)
        {
            DateTime lastMonday = DateTime.Now.Date;
            while (lastMonday.DayOfWeek != DayOfWeek.Monday)
            {
                lastMonday = lastMonday.AddDays(-1);
            }

            return time - lastMonday;
        }
    }
}