using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Models;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Infra.RoomModels.Format;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.RoomServices.RoomFormats
{
    public class AuditoriumFormatService : IAuditoriumFormatService
    {
        private readonly MoviesContext _context;

        public AuditoriumFormatService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(AuditoriumFormatCreateRequest model)
        {
            var auditoriumFormat = new AuditoriumFormat()
            {
                Name = model.Name,
                Id = model.Id
            };

            await _context.AddAsync(auditoriumFormat);
            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("Không thể thêm định dạng");
            }

            return new ApiSuccessResult<bool>(true, "Thêm thành công");
        }

        public async Task<ApiResult<bool>> UpdateAsync(AuditoriumFormatUpdateRequest model)
        {
            AuditoriumFormat auditoriumFormat = await _context.AuditoriumFormats.FindAsync(model.Id);
            if (auditoriumFormat == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy định dạng");
            }
            else
            {
                auditoriumFormat.Name = model.Name;
                auditoriumFormat.Id = model.Id;
                _context.Update(auditoriumFormat);
                await _context.SaveChangesAsync();

                return new ApiSuccessResult<bool>(true, "Cập nhật thành công");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            AuditoriumFormat auditoriumFormat = await _context.AuditoriumFormats.FindAsync(id);
            if (auditoriumFormat == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy định dạng");
            }
            else
            {
                _context.AuditoriumFormats.Remove(auditoriumFormat);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true, "Xóa thành công");
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public async Task<ApiResult<List<AuditoriumFormatVMD>>> GetAllAsync()
        {
            var roomFormats = await _context.AuditoriumFormats.Select(x => new AuditoriumFormatVMD()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            return new ApiSuccessResult<List<AuditoriumFormatVMD>>(roomFormats);
        }

        public async Task<ApiResult<AuditoriumFormatVMD>> GetByIdAsync(string id)
        {
            var roomFormats = await _context.AuditoriumFormats.Where(x => x.Id == id).Select(x =>
                new AuditoriumFormatVMD()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).FirstOrDefaultAsync();

            if (roomFormats == null)
            {
                return new ApiErrorResult<AuditoriumFormatVMD>("Không tìm thấy định dạng phòng");
            }

            return new ApiSuccessResult<AuditoriumFormatVMD>(roomFormats);
        }
    }
}