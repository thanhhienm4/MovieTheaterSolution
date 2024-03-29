﻿using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.ReservationService.cs
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
                    Price = CalPrice(x.ScreeningId, x.SeatId)

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

        public Task<ApiResult<ReservationVMD>> GetReservationById(int Id)
        {
            throw new NotImplementedException();
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
                rv.ReservationTypeId = request.ReservationTypeId;
                rv.UserId = request.UserId;
                rv.EmployeeId = request.EmployeeId;
                _context.Update(rv);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResultLite("Cập nhật thất bại");
                }
                return new ApiSuccessResultLite("Cập nhật thành công");
            }
        }
        private int CalPrice(int ScreeningId, int SeatId)
        {
            var query = from s in _context.Seats
                        join ks in _context.KindOfSeats on s.KindOfSeatId equals ks.Id
                        join r in _context.Rooms on s.RoomId equals r.Id
                        join fr in _context.RoomFormats on r.FormatId equals fr.Id
                        where s.Id == SeatId
                        select new { RoomPrice = fr.Price, SeatSurcharge = ks.Surcharge };

            var screeningSurcharge = from sCR in _context.Screenings
                                     join kOS in _context.KindOfScreenings on sCR.KindOfScreeningId equals kOS.Id
                                     where sCR.Id == ScreeningId
                                     select (kOS.Surcharge);
            return query.First().RoomPrice + query.First().SeatSurcharge + screeningSurcharge.First();


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
    }
}
