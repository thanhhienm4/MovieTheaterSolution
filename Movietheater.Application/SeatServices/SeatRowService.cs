using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Seat.SeatRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.SeatServices
{
    public class SeatRowService :ISeatRowService
    {
        private readonly MovieTheaterDBContext _context;
        public SeatRowService(MovieTheaterDBContext context)
        {
            _context = context;
        }
        public async Task<ApiResultLite> CreateAsync(string name)
        {
            SeatRow kindOfSeat = new SeatRow()
            {
                Name = name
            };
            _context.SeatRows.Add(kindOfSeat);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResultLite("Tạo mới thất bại");
            }

            return new ApiSuccessResultLite();

        }

        public async Task<ApiResultLite> UpdateAsync(SeatRowUpdateRequest request)
        {
            SeatRow seat = await _context.SeatRows.FindAsync(request.Id);
            if (seat == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                seat.Name = request.Name;
                _context.Update(seat);
                if (await _context.SaveChangesAsync() != 0)
                    return new ApiSuccessResultLite("Cập nhật thành công");
                else
                    return new ApiErrorResultLite("Cập nhật thất bại");
            }
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            SeatRow seat = await _context.SeatRows.FindAsync(id);
            if (seat == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                _context.SeatRows.Remove(seat);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResultLite("Xóa thành công");
                }
                else
                {
                    return new ApiErrorResultLite("Xóa thất bại");
                }


            }
        }

   }
}
