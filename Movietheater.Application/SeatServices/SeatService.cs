using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.SeatServices
{
    public class SeatService : ISeatService
    {
        private readonly MovieTheaterDBContext _context;
        public SeatService(MovieTheaterDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResultLite> CreateAsync(SeatCreateRequest request)
        {
            Seat seat = new Seat()
            {
                Row = request.Row,
                Number = request.Number,
                KindOfSeatId = request.KindOfSeatId,
                RoomId = request.RoomId
            };
            _context.Seats.Add(seat);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResultLite("Thêm thất bại");
            }

            return new ApiSuccessResultLite("Thêm thành công");
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            Seat seat = await _context.Seats.FindAsync(id);
            if (seat == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                _context.Seats.Remove(seat);
                await _context.SaveChangesAsync();
                return new ApiSuccessResultLite("Xóa thành công");
            }
        }

        public async Task<ApiResultLite> UpdateAsync(SeatUpdateRequest request)
        {
            Seat seat = await _context.Seats.FindAsync(request.Id);
            if (seat == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                seat.Id = request.Id;
                seat.Row = request.Row;
                seat.Number = request.Number;
                seat.KindOfSeatId = request.KindOfSeatId;
                seat.RoomId = request.RoomId;
                _context.Update(seat);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResultLite("Cập nhật thất bại");
                }
                return new ApiSuccessResultLite("Cập nhật thành công");
            }
        }
    }
}
