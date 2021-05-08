using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public class PositionService : IPositionService
    {
        private readonly MovieTheaterDBContext _context;
        public PositionService(MovieTheaterDBContext context)
        {
            _context = context;
        }
        public async Task<ApiResultLite> CreateAsync(string name)
        {
            Position position = new Position()
            {
                Name = name
            };
            _context.Positions.Add(position);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResultLite("Thêm thất bại");
            }

            return new ApiSuccessResultLite("Thêm thành công");
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            Position position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                _context.Positions.Remove(position);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResultLite("Xóa thành công");
                }
                else return new ApiSuccessResultLite("Không xóa được");
            }
        }

        public async Task<ApiResultLite> UpdateAsync(PositionUpdateRequest request)
        {
            Position position = await _context.Positions.FindAsync(request.Id);
            if (position == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                position.Id = request.Id;
                position.Name = request.Name;
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResultLite("Cập nhật thất bại");
                }
                return new ApiSuccessResultLite("Cập nhật thành công");
            }
        }

        public async Task<ApiResult<List<PositionVMD>>> GetAllPositionAsync()
        {
            var positions =  await _context.Positions.Select(x => new PositionVMD()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return new ApiSuccessResult<List<PositionVMD>>(positions);
        }
    }
}
