using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.Room.Format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application
{
    public class RoomFormatFormatService
    {
        private readonly MovieTheaterDBContext _context;
        public RoomFormatFormatService(MovieTheaterDBContext context)
        {
            _context = context;
        }
        public async Task<ApiResultLite> CreateAsync(RoomFormatCreateRequest model)
        {
            var room = new RoomFormat()
            {
                Name = model.Name,
                Price = model.Price,
                
            };

            await _context.AddAsync(room);
            if ((await _context.SaveChangesAsync()) == 0)
            {
                return new ApiErrorResultLite("Không thể thêm định dạng");
            }
            return new ApiSuccessResultLite("Thêm thành công");

        }
        public async Task<ApiResultLite> UpdateAsync(RoomFormatUpdateRequest model)
        {
            RoomFormat room = await _context.RoomFormats.FindAsync(model.Id);
            if (room == null)
            {
                return new ApiErrorResultLite("Không tìm thấy định dạng");
            }
            else
            {
                room.Name = model.Name;
                room.Price = model.Price;
                _context.Update(room);
                await _context.SaveChangesAsync();

                return new ApiSuccessResultLite("Cập nhật thành công");

            }
        }
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            RoomFormat room = await _context.RoomFormats.FindAsync(id);
            if (room == null)
            {
                return new ApiErrorResultLite("Không tìm thấy định dạng");
            }
            else
            {
                _context.RoomFormats.Remove(room);
                await _context.SaveChangesAsync();
                return new ApiSuccessResultLite("Xóa thành công");
            }
        }
    }
}
