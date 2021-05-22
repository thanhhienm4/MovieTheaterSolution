using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public class BanService : IBanService
    {
        private readonly MovieTheaterDBContext _context;

        public BanService(MovieTheaterDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResultLite> CreateAsync(String name)
        {
            Ban ban = new Ban()
            {
                Name = name
            };
            _context.Bans.Add(ban);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResultLite("Thêm thất bại");
            }

            return new ApiSuccessResultLite("Thêm thành công");
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            Ban ban = await _context.Bans.FindAsync(id);
            if (ban == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                _context.Bans.Remove(ban);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResultLite("Xóa thành công");
                }
                else return new ApiSuccessResultLite("Không xóa được");
            }
        }

        public async Task<ApiResultLite> UpdateAsync(BanUpdateRequest request)
        {
            Ban ban = await _context.Bans.FindAsync(request.Id);
            if (ban == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                ban.Id = request.Id;
                ban.Name = request.Name;
                _context.Update(ban);
                int result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    return new ApiErrorResultLite("Cập nhật thất bại");
                }
                return new ApiSuccessResultLite("Cập nhật thành công");
            }
        }

        public async Task<ApiResult<List<BanVMD>>> GetAllBanAsync()
        {
            var bans = await _context.Bans.Select(x => new BanVMD()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return new ApiSuccessResult<List<BanVMD>>(bans);
        }
    }
}