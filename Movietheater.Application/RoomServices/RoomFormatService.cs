using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.RoomServices
{
    public class RoomFormatService : IRoomFormatService
    {
        private readonly MovieTheaterDBContext _context;

        public RoomFormatService(MovieTheaterDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(RoomFormatCreateRequest model)
        {
            var room = new RoomFormat()
            {
                Name = model.Name,
                Price = model.Price,
            };

            await _context.AddAsync(room);
            if ((await _context.SaveChangesAsync()) == 0)
            {
                return new ApiErrorResult<bool>("Không thể thêm định dạng");
            }
            return new ApiSuccessResult<bool>(true,"Thêm thành công");
        }

        public async Task<ApiResult<bool>> UpdateAsync(RoomFormatUpdateRequest model)
        {
            RoomFormat room = await _context.RoomFormats.FindAsync(model.Id);
            if (room == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy định dạng");
            }
            else
            {
                room.Name = model.Name;
                room.Price = model.Price;
                _context.Update(room);
                await _context.SaveChangesAsync();

                return new ApiSuccessResult<bool>(true,"Cập nhật thành công");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            RoomFormat room = await _context.RoomFormats.FindAsync(id);
            if (room == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy định dạng");
            }
            else
            {
                _context.RoomFormats.Remove(room);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true,"Xóa thành công");
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public async Task<ApiResult<List<RoomFormatVMD>>> GetAllRoomFormatAsync()
        {
            var roomFormats = await _context.RoomFormats.Select(x => new RoomFormatVMD()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            }).ToListAsync();

            return new ApiSuccessResult<List<RoomFormatVMD>>(roomFormats);
        }

        public async Task<ApiResult<RoomFormatVMD>> GetRoomFormatByIdAsync(int id)
        {
            var roomFormats = await _context.RoomFormats.Where(x => x.Id == id).Select(x => new RoomFormatVMD()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            }).FirstOrDefaultAsync();

            if(roomFormats == null)
            {
                return new ApiErrorResult<RoomFormatVMD>("Không tìm thấy định dạng phòng");
            }    
            return new ApiSuccessResult<RoomFormatVMD>(roomFormats);
        }
    }
}