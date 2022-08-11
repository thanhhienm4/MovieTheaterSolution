using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Models;
using MovieTheater.Data.Results;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.SeatServices.Seats
{
    public class SeatService : ISeatService
    {
        private readonly MoviesContext _context;

        public SeatService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(SeatCreateRequest request)
        {
            Seat seat = new Seat()
            {
                RowId = request.RowId,
                Number = request.Number,
                TypeId = request.TypeId,
                AuditoriumId = request.AuditoriumId,
                IsActive = true
            };
            _context.Seats.Add(seat);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }

            return new ApiSuccessResult<bool>(true, "Thêm thành công");
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
                    return new ApiSuccessResult<bool>(true, "Xóa thành công");
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
                seat.TypeId = request.TypeId;
                seat.AuditoriumId = request.AuditoriumId;
                _context.Update(seat);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }

                return new ApiSuccessResult<bool>(true, "Cập nhật thành công");
            }
        }

        public async Task<ApiResult<List<SeatVMD>>> GetAllInRoomAsync(string roomId)
        {
            var seats = await _context.Seats.Where(x => x.AuditoriumId == roomId && x.IsActive == true).Select(x =>
                new SeatVMD()
                {
                    Id = x.Id,
                    Number = x.Number,
                    RowId = x.RowId,
                    AuditoriumId = x.AuditoriumId,
                    TypeId = x.TypeId,
                    RowName = x.Row.Name
                }).ToListAsync();

            return new ApiSuccessResult<List<SeatVMD>>(seats);
        }

        public async Task<ApiResult<bool>> UpdateInRoomAsync(SeatsInRoomUpdateRequest request)
        {
            var auditorium = await _context.Auditoriums.FindAsync(request.AuditoriumId);
            if (auditorium == null)
                return new ApiErrorResult<bool>("Phòng chiếu không tồn tại");

            var seats = await _context.Seats.Where(x => x.AuditoriumId == request.AuditoriumId).ToListAsync();

            if (request.Seats == null)
            {
                return new ApiErrorResult<bool>("Cập nhật không thành công");
            }

            List<int> rowIds = request.Seats.Select(x => x.RowId).ToList();
            if (CheckListRow(rowIds) == false)
                return new ApiErrorResult<bool>("Vị trí không hợp lệ");

            foreach (var seat in seats)
            {
                if (request.Seats.Any(x => x.Number == seat.Number && x.RowId == seat.RowId))
                {
                    seat.TypeId = seat.TypeId;
                    seat.IsActive = true;
                }
                else
                {
                    seat.IsActive = false;
                }
            }

            _context.UpdateRange(seats);

            var newSeats = request.Seats.Where(x =>
                    !seats.Any(y => x.AuditoriumId == y.AuditoriumId && x.RowId == y.RowId && x.Number == y.Number))
                .Select(x => new Seat()
                {
                    AuditoriumId = x.AuditoriumId,
                    RowId = x.RowId,
                    Number = x.Number,
                    TypeId = x.TypeId,
                    IsActive = true,
                });

            _context.AddRange(newSeats);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return new ApiErrorResult<bool>(
                    "phòng đã có suất chiếu, chỉ được thêm ghế, vui lòng kiểm tra lại thông tin");
            }

            return new ApiSuccessResult<bool>(true, "Cập nhật thành công");
        }

        public async Task<ApiResult<List<SeatModel>>> GetListReserved(int reservationId)
        {
            var res = _context.SeatModel.FromSqlRaw("GetSeatInScreening {0}", reservationId).ToList();
            return new ApiSuccessResult<List<SeatModel>>(res);
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