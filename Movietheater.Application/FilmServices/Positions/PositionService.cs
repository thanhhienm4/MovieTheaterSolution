using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTheater.Data.Models;

namespace MovieTheater.Application.FilmServices.Positions
{
    public class PositionService : IPositionService
    {
        private readonly MoviesContext _context;

        public PositionService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(string name)
        {
            Position position = new Position()
            {
                Name = name
            };
            _context.Positions.Add(position);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }

            return new ApiSuccessResult<bool>(true, "Thêm thành công");
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            Position position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                _context.Positions.Remove(position);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true, "Xóa thành công");
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public async Task<ApiResult<bool>> UpdateAsync(PositionUpdateRequest request)
        {
            Position position = await _context.Positions.FindAsync(request.Id);
            if (position == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                position.Id = request.Id;
                position.Name = request.Name;
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }
                return new ApiSuccessResult<bool>(true, "Cập nhật thành công");
            }
        }

        public async Task<ApiResult<List<PositionVMD>>> GetAllPositionAsync()
        {
            var positions = await _context.Positions.Select(x => new PositionVMD()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return new ApiSuccessResult<List<PositionVMD>>(positions);
        }
    }
}