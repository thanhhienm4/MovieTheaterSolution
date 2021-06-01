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

        public async Task<ApiResult<bool>> CreateAsync(String name)
        {
            Ban ban = new Ban()
            {
                Name = name
            };
            _context.Bans.Add(ban);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            Ban ban = await _context.Bans.FindAsync(id);
            if (ban == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                _context.Bans.Remove(ban);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true);
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public async Task<ApiResult<bool>> UpdateAsync(BanUpdateRequest request)
        {
            Ban ban = await _context.Bans.FindAsync(request.Id);
            if (ban == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                ban.Id = request.Id;
                ban.Name = request.Name;
                _context.Update(ban);
                int result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }
                return new ApiSuccessResult<bool>(true);
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