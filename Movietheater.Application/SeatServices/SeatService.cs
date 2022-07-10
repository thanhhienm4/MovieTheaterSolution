using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.SeatServices
{
    public class SeatService : ISeatService
    {
        private readonly MovieTheaterDBContext _context;

        public SeatService(MovieTheaterDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(SeatCreateRequest request)
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
                return new ApiErrorResult<bool>("Thêm thất bại");
            }

            return new ApiSuccessResult<bool>(true,"Thêm thành công");
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            Seat seat = await _context.Seats.FindAsync(id);
            if (seat == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                _context.Seats.Remove(seat);

                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true,"Xóa thành công");
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public Task<ApiResult<SeatVMD>> GetSeatById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> UpdateAsync(SeatUpdateRequest request)
        {
            Seat seat = await _context.Seats.FindAsync(request.Id);
            if (seat == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
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
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }
                return new ApiSuccessResult<bool>(true,"Cập nhật thành công");
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
                KindOfSeatId = x.KindOfSeatId,
                Name = x.Name
            }).ToListAsync();

            return new ApiSuccessResult<List<SeatVMD>>(seats);
        }

        public async Task<ApiResult<bool>> UpdateSeatInRoomAsync(SeatsInRoomUpdateRequest request)
        {
            var room = await _context.Rooms.FindAsync(request.RoomId);
            if (room == null)
                return new ApiErrorResult<bool>("Phòng chiếu không hợp lệ");

            var seats = await _context.Seats.Where(x => x.RoomId == request.RoomId).ToListAsync();
            foreach (Seat seat in seats)
            {
                seat.IsActive = false;
                _context.Seats.Update(seat);
            }
            if (request.Seats == null)
            {
                return new ApiErrorResult<bool>("Cập nhật không thành công ");
            }
            List<int> rowIds = request.Seats.Select(x => x.RowId).ToList();
            if (CheckListRow(rowIds) == false)
                return new ApiErrorResult<bool>("Vị trí không hợp lệ");

            foreach (SeatCreateRequest seatCR in request.Seats)
            {
                var seat = await _context.Seats.Where(x => x.RoomId == seatCR.RoomId
                                                       && x.Number == seatCR.Number
                                                       && x.RowId == seatCR.RowId).FirstOrDefaultAsync();
                if (seat == null)
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

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return new ApiErrorResult<bool>("phòng đã có suất chiếu, chỉ được thêm ghế, vui lòng kiểm tra lại thông tin");
            }

            return new ApiSuccessResult<bool>(true,"Cập nhật thành công");
        }

        public async Task<ApiResult<List<SeatVMD>>> GetListSeatReserved(int screeningId)
        {
            var screening = await _context.Screenings.FindAsync(screeningId);
            if (screening == null)
                return new ApiErrorResult<List<SeatVMD>>("Không tìm thấy xuất chiếu");

            var query = from t in _context.Tickets
                        join s in _context.Seats on t.SeatId equals s.Id
                        where t.ScreeningId == screeningId
                        select s;

            var seats = await query.Select(x => new SeatVMD()
            {
                Id = x.Id,
                KindOfSeatId = x.KindOfSeatId,
                Number = x.Number,
                RoomId = x.RoomId,
                RowId = x.RowId
            }).ToListAsync();

            return new ApiSuccessResult<List<SeatVMD>>(seats);
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