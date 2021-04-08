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
                RowId = request.RowId,
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

        public async Task<ApiResult<List<SeatVMD>>> GetSeatInRoomAsync(int roomId)
        {


            var seats = await _context.Seats.Where(x => x.RoomId == roomId && x.IsActive == true).Select(x => new SeatVMD()
            {
                Id = x.Id,
                Number = x.Number,
                RowId = x.RowId,
                RoomId = x.RoomId,
                KindOfSeatId = x.KindOfSeatId
            }).ToListAsync();


            return new ApiSuccessResult<List<SeatVMD>>(seats);

        }

        public async Task<ApiResultLite> UpdateSeatInRoomAsync(SeatsInRoomUpdateRequest request)
        {
            var room = await _context.Rooms.FindAsync(request.RoomId);
            if (room == null)
                return new ApiErrorResultLite("Phòng chiếu không hợp lệ");


            var seats = await _context.Seats.Where(x => x.RoomId == request.RoomId).ToListAsync();
            foreach (Seat seat in seats)
            {
                seat.IsActive = false;
                _context.Seats.Update(seat);
            }
            if (request.Seats == null)
            {
                return new ApiSuccessResultLite("Cập nhật thành công ");
            }
            List<int> rowIds = request.Seats.Select(x => x.RowId).ToList();
            if (CheckListRow(rowIds) == false)
                return new  ApiErrorResultLite("Vị trí không hợp lệ");

            

            foreach(SeatCreateRequest seatCR in request.Seats)
            {
                var seat  = await _context.Seats.Where(x=> x.RoomId == seatCR.RoomId
                                                        && x.Number == seatCR.Number
                                                        && x.RowId == seatCR.RowId).FirstOrDefaultAsync();
                if(seat == null)
                {
                    Seat newSeat = new Seat()
                    {
                        RoomId = seatCR.RoomId,
                        RowId = seatCR.RowId,
                        Number = seatCR.Number,
                        KindOfSeatId = seatCR.KindOfSeatId,
                        IsActive = true,
                    };
                    _context.Seats.Add(newSeat);
                   // await _context.SaveChangesAsync();
                }
                else
                {
                    seat.KindOfSeatId = seatCR.KindOfSeatId;
                    seat.IsActive = true;
                    _context.Seats.Update(seat);
                   // await _context.SaveChangesAsync();
                }

            }

            if (await _context.SaveChangesAsync() == 0)
                return new ApiErrorResultLite("Cập nhật không thành công");
            else
                return new ApiSuccessResultLite("Cập nhật thành công ");


        }

        private bool CheckListRow(List<int> listSeatRow)
        {
            List<int> rowIds = _context.SeatRows.Select(x => x.Id).ToList();
            int cnt1 = new HashSet<int>(rowIds).Count;

            rowIds.AddRange(listSeatRow);
            int cnt2 = new HashSet<int>(rowIds).Count;
            if (cnt1 != cnt2)
                return false;
            else
                return true;
            
        }
    }
}
