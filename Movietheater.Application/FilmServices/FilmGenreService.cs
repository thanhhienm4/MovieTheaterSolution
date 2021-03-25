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
    public class FilmGenreService : IFilmGenreService
    {
        private readonly MovieTheaterDBContext _context;
        public FilmGenreService(MovieTheaterDBContext context)
        {
            _context = context;
        }
        public async Task<ApiResultLite> CreateAsync(string name)
        {
            FilmGenre filmgenre = new FilmGenre()
            {
                Name = name
            };
            _context.FilmGenre.Add(filmgenre);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResultLite("Thêm thất bại");
            }

            return new ApiSuccessResultLite("Thêm thành công");
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            FilmGenre filmgenre = await _context.FilmGenre.FindAsync(id);
            if (filmgenre == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                _context.FilmGenre.Remove(filmgenre);
                await _context.SaveChangesAsync();
                return new ApiSuccessResultLite("Xóa thành công");
            }
        }

        public async Task<ApiResultLite> UpdateAsync(FilmGenreUpdateRequest request)
        {
            FilmGenre filmgenre = await _context.FilmGenre.FindAsync(request.Id);
            if (filmgenre == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                filmgenre.Id = request.Id;
                filmgenre.Name = request.Name;
                _context.Update(filmgenre);
                int result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    return new ApiErrorResultLite("Cập nhật thất bại");
                }
                return new ApiSuccessResultLite("Cập nhật thành công");
            }
        }
    }
}
