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
    public class FlimService : IFilmService
    {
        private readonly MovieTheaterDBContext _context;
        public FlimService(MovieTheaterDBContext context)
        {
            _context = context;
        }


        public async Task<ApiResultLite> CreateAsync(FilmCreateRequest request)
        {
            var room = new Film()
            {
                Name = request.Name,
                Description = request.Description,
                BanId = request.BanId,
                Length = request.Length,
                PublishDate = request.PublishDate,
                TrailerURL = request.TrailerURL,
                Poster = request.Poster
            };

            await _context.AddAsync(room);
            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResultLite("Không thể thêm");
            }
            return new ApiSuccessResultLite("Thêm thành công");

        }
        public async Task<ApiResultLite> UpdateAsync(FilmUpdateRequest request)
        {
            Film film = await _context.Films.FindAsync(request.Id);
            if (film == null)
            {
                return new ApiErrorResultLite("Không tìm thấy phòng");
            }
            else
            {
                film.Name = request.Name;
                film.Description = request.Description;
                film.BanId = request.BanId;
                film.Length = request.Length;
                film.PublishDate = request.PublishDate;
                film.TrailerURL = request.TrailerURL;
                film.Poster = request.Poster;
                _context.Films.Update(film);
                if (await _context.SaveChangesAsync() == 0)
                {
                    return new ApiErrorResultLite("Chỉnh sửa thất bại");
                }
                return new ApiSuccessResultLite("Chỉnh sửa thành công");

            }
        }
        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            Film film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return new ApiErrorResultLite("Không tìm thấy phim");
            }
            else
            {
                _context.Films.Remove(film);
                if (await _context.SaveChangesAsync() == 0)
                {
                    return new ApiErrorResultLite("Xóa thất bại");
                }
                return new ApiSuccessResultLite("Xóa thành công");
            }
        }

        //



    }
}
