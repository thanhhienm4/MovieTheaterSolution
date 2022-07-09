using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Application.FilmServices
{
    public class FilmGenreService : IFilmGenreService
    {
        private readonly MovieTheaterDBContext _context;

        public FilmGenreService(MovieTheaterDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(string name)
        {
            FilmGenre filmgenre = new FilmGenre()
            {
                Name = name
            };
            _context.FilmGenre.Add(filmgenre);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            FilmGenre filmgenre = await _context.FilmGenre.FindAsync(id);
            if (filmgenre == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                _context.FilmGenre.Remove(filmgenre);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true);
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public async Task<ApiResult<bool>> UpdateAsync(FilmGenreUpdateRequest request)
        {
            FilmGenre filmgenre = await _context.FilmGenre.FindAsync(request.Id);
            if (filmgenre == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                filmgenre.Id = request.Id;
                filmgenre.Name = request.Name;
                _context.Update(filmgenre);
                int result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }
                return new ApiSuccessResult<bool>(true);
            }
        }

        public async Task<ApiResult<List<FilmGenreVMD>>> GetAllFilmGenreAsync()
        {
            var genres = await _context.FilmGenre.Select(x => new FilmGenreVMD()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return new ApiSuccessResult<List<FilmGenreVMD>>(genres);
        }
    }
}