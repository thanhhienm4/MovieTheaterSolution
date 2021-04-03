using Microsoft.EntityFrameworkCore;
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
                RowId = request.Row,
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
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResultLite("Xóa thành công");
                }
                else return new ApiSuccessResultLite("Không xóa được");
            }
        }

        public Task<ApiResult<SeatVMD>> GetSeatById(int id)
        {
            throw new NotImplementedException();
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
                seat.RowId = request.RowId;
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

        public async Task<ApiResult<List<List<SeatVMD>>>> GetSeatInRoomAsync(int roomId)
        {
            var query =  _context.Seats.Where(x => x.RoomId == roomId).ToList();

            List<SeatRow> rows = _context.SeatRows.Select(x => x).OrderBy(x=> x.Name).ToList();
            List<List<SeatVMD>> result = new List<List<SeatVMD>>();

            foreach(var row in rows)
            {
                List<SeatVMD> rowSeats = query.Where(x => x.RowId == row.Id).Select(x => new SeatVMD()
                {
                    Id = x.Id,
                    Number = x.Number,
                    RowId = x.RowId,
                    RoomId = x.RoomId
                })
                .ToList();

                result.Add(rowSeats);
            }

            return new ApiSuccessResult<List<List<SeatVMD>>>(result);

        }

    }
}
