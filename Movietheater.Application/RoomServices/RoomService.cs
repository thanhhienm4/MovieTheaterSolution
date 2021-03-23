using MovieTheater.Data.EF;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Data.Entities;

namespace Movietheater.Application.RoomServices
{

    public class RoomService
    {
        private readonly MovieTheaterDBContext _context;

        public RoomService(MovieTheaterDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResultLite> CreateAsync(RoomCreateRequest model)
        {
            var room = new Room()
            {
                Name = model.Name,
                FormatId = model.FormatId
            };

            await _context.Rooms.AddAsync(room);
            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResultLite("Không thể thêm phòng");
            }
            return new ApiSuccessResultLite("Thêm thành công");

        }
        public async Task<ApiResultLite> UpdateAsync(RoomUpdateRequest model)
        {
            Room room = await _context.Rooms.FindAsync(model.Id);
            if (room == null)
            {
                return new ApiErrorResultLite("Không tìm thấy phòng");
            }
            else
            {
                room.Name = model.Name;
                room.FormatId = model.FormatId;
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();

                return new ApiSuccessResultLite("Cập nhật thành công");

            }
        }
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return new ApiErrorResultLite("Không tìm thấy phòng");
            }
            else
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
                return new ApiSuccessResultLite("Xóa thành công");
            }
        }

        // RoomFormat 


    }
}
