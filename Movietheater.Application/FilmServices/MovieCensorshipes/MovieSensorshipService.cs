using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Film.MovieCensorships;
using MovieTheater.Models.Common.ApiResult;

namespace MovieTheater.Application.FilmServices.MovieCensorshipes
{
    public class MovieCensorshipService : IMovieCensorshipService
    {
        private readonly MoviesContext _context;

        public MovieCensorshipService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(MovieCensorshipCreateRequest request)
        {
            MovieCensorship movieCensorship = new MovieCensorship()
            {
                Name = request.Name,
                Id = request.Id,
            };

            _context.MovieCensorships.Add(movieCensorship);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            MovieCensorship movieCensorship = await _context.MovieCensorships.FindAsync(id);
            if (movieCensorship == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                _context.MovieCensorships.Remove(movieCensorship);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true);
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public async Task<ApiResult<bool>> UpdateAsync(MovieCensorshipUpdateRequest request)
        {
            MovieCensorship movieCensorship = await _context.MovieCensorships.FindAsync(request.Id);
            if (movieCensorship == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                movieCensorship.Id = request.Id;
                movieCensorship.Name = request.Name;
                _context.Update(movieCensorship);

                int result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }

                return new ApiSuccessResult<bool>(true);
            }
        }

        public async Task<ApiResult<List<MovieCensorshipVMD>>> GetAllBanAsync()
        {
            var movieCensorship = await _context.MovieCensorships.ToListAsync();
            var res = movieCensorship.Select(x => new MovieCensorshipVMD(x)).ToList();
            return new ApiSuccessResult<List<MovieCensorshipVMD>>(res);
        }
    }
}