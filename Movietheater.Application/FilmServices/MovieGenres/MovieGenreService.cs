using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Film.MovieGenres;

namespace MovieTheater.Application.FilmServices.MovieGenres
{
    public class MovieGenreService : IMovieGenreService
    {
        private readonly MoviesContext _context;

        public MovieGenreService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(MovieGenreCreateRequest request)
        {
            MovieGenre movieGenre = new MovieGenre()
            {
                Id = request.Id,
                Name = request.Name
            };

            _context.MovieGenres.Add(movieGenre);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            MovieGenre movieGenre = await _context.MovieGenres.FindAsync(id);
            if (movieGenre == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                _context.MovieGenres.Remove(movieGenre);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true);
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public async Task<ApiResult<bool>> UpdateAsync(MovieGenreUpdateRequest request)
        {
            MovieGenre movieGenre = await _context.MovieGenres.FindAsync(request.Id);
            if (movieGenre == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                movieGenre.Id = request.Id;
                movieGenre.Name = request.Name;
                _context.Update(movieGenre);
                int result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }
                return new ApiSuccessResult<bool>(true);
            }
        }

        public async Task<ApiResult<List<MovieGenreVMD>>> GetAllMovieGenreAsync()
        {
            var genres = await _context.MovieGenres.Select(x => new MovieGenreVMD()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return new ApiSuccessResult<List<MovieGenreVMD>>(genres);
        }
    }
}