using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieTheater.Application.Common;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MovieTheater.Application.FilmServices.Movies
{
    public class MovieService : IMovieService
    {
        private readonly MoviesContext _context;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;

        public MovieService(MoviesContext context, IStorageService storageService, IConfiguration configuration)
        {
            _context = context;
            _storageService = storageService;
            _configuration = configuration;
        }

        public async Task<ApiResult<bool>> CreateAsync(MovieCreateRequest request)
        {
            string posterPath = await SaveFile(request.Poster);
            var movie = new Movie()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                CensorshipId = request.CensorshipId,
                Length = request.Length,
                PublishDate = request.PublishDate,
                TrailerUrl = request.TrailerURL,
                Poster = posterPath
            };

            await _context.AddAsync(movie);
            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("Không thể thêm phim");
            }

            return new ApiSuccessResult<bool>(true, "Thêm phim thành công");
        }

        public async Task<ApiResult<bool>> UpdateAsync(MovieUpdateRequest request)
        {
            Movie movie = await _context.Movies.FindAsync(request.Id);
            if (movie == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy phim");
            }
            else
            {
                movie.Name = request.Name;
                movie.Description = request.Description;
                movie.CensorshipId = request.CensorshipId;
                movie.Length = request.Length;
                movie.PublishDate = request.PublishDate;
                movie.TrailerUrl = request.TrailerURL;

                if (request.Poster != null)
                {
                    try
                    {
                        await _storageService.DeleteFileAsync(movie.Poster);
                        string newPosterPath = await SaveFile(request.Poster);
                        movie.Poster = newPosterPath;
                    }
                    catch
                    {

                    }

                    
                }

                _context.Movies.Update(movie);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }

                return new ApiSuccessResult<bool>(true, "Cập nhật phim thành công");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(string id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            Movie movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy phim");
            }
            else
            {
                if (_context.Screenings.Count(x => x.MovieId == movie.Id) != 0)
                    return new ApiErrorResult<bool>("Xóa thất bại");
                try
                {
                    string poster = movie.Poster;
                    _context.Movies.Remove(movie);
                    _context.SaveChanges();
                    transaction.Commit();

                    await _storageService.DeleteFileAsync(poster);
                    return new ApiSuccessResult<bool>(true, "Xóa phim thành công");
                }
                catch (Exception)
                {
                    return new ApiErrorResult<bool>("Xóa thất bại");
                }
            }
        }

        public async Task<ApiResult<List<MovieVMD>>> GetAllAsync()
        {
            var query = from m in _context.Movies
                join c in _context.MovieCensorships on m.CensorshipId equals c.Id
                select new { m, c };

            var Movies = await query.Select(x => new MovieVMD()
            {
                Id = x.m.Id,
                Name = x.m.Name,
                PublishDate = x.m.PublishDate,
                Ban = x.c.Id,
                Poster = $"{_configuration["BackEndServer"]}/" +
                         $"{FileStorageService.UserContentFolderName}/{x.m.Poster}",
                Description = x.m.Description,
                TrailerURL = x.m.TrailerUrl
            }).ToListAsync();

            return new ApiSuccessResult<List<MovieVMD>>(Movies);
        }

        public async Task<ApiResult<List<MovieVMD>>> GetAllPlayingAsync()
        {
            var nextWeek = DateTime.Now.Date.AddDays(7);
            var query = from m in _context.Movies
                join c in _context.MovieCensorships on m.CensorshipId equals c.Id
                join s in _context.Screenings on m.Id equals s.MovieId
                where s.StartTime.Date >= DateTime.Now.Date
                      && s.StartTime.Date <= nextWeek
                select new { m, c };

            var Movies = await query.Distinct().Select(x => new MovieVMD()
            {
                Id = x.m.Id,
                Name = x.m.Name,
                PublishDate = x.m.PublishDate,
                Ban = x.c.Id,
                Poster = $"{_configuration["BackEndServer"]}/" +
                         $"{FileStorageService.UserContentFolderName}/{x.m.Poster}",
                Description = x.m.Description,
                Length = x.m.Length,
                TrailerURL = x.m.TrailerUrl
            }).ToListAsync();

            return new ApiSuccessResult<List<MovieVMD>>(Movies);
        }

        public async Task<ApiResult<List<MovieVMD>>> GetAllUpcomingAsync()
        {
            var query = from m in _context.Movies
                join c in _context.MovieCensorships on m.CensorshipId equals c.Id
                where m.PublishDate.Date > DateTime.Now.Date
                select new { m, c };

            var Movies = await query.Select(x => new MovieVMD()
            {
                Id = x.m.Id,
                Name = x.m.Name,
                PublishDate = x.m.PublishDate,
                Ban = x.c.Id,
                Poster = $"{_configuration["BackEndServer"]}/" +
                         $"{FileStorageService.UserContentFolderName}/{x.m.Poster}",
                Description = x.m.Description,
                TrailerURL = x.m.TrailerUrl,
                Length = x.m.Length
            }).ToListAsync();

            return new ApiSuccessResult<List<MovieVMD>>(Movies);
        }

        public async Task<ApiResult<PageResult<MovieVMD>>> GetPagingAsync(MoviePagingRequest request)
        {
            var query = from m in _context.Movies
                join c in _context.MovieCensorships on m.CensorshipId equals c.Id
                select new { m, c };

            if (!string.IsNullOrWhiteSpace(request.Keyword))
                query = query.Where(x => x.m.Name.Contains(request.Keyword)
                                         || x.m.Id.ToString().Contains(request.Keyword)
                                         || x.c.Name.Contains(request.Keyword));

            int totalRow = await query.CountAsync();

            var movies = query.OrderBy(x => x.m.Name).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new MovieVMD()
                {
                    Id = x.m.Id,
                    Name = x.m.Name,
                    PublishDate = x.m.PublishDate,
                    Ban = x.c.Id,
                    Poster = $"{_configuration["BackEndServer"]}/" +
                             $"{FileStorageService.UserContentFolderName}/{x.m.Poster}",
                    Description = x.m.Description,
                    TrailerURL = x.m.TrailerUrl
                }).ToList();

            foreach (var movie in movies)
            {
                movie.Genres = GetGenres(movie.Id);
            }

            var pageResult = new PageResult<MovieVMD>()
            {
                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = movies,
            };

            return new ApiSuccessResult<PageResult<MovieVMD>>(pageResult);
        }

        public async Task<ApiResult<MovieMD>> GetById(string id)
        {
            Movie movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return new ApiErrorResult<MovieMD>("Không tìm thấy phim");
            }
            else
            {
                var result = new MovieMD()
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Censorship = movie.CensorshipId,
                    Description = movie.Description,
                    Length = movie.Length,
                    PublishDate = movie.PublishDate,
                    TrailerURL = movie.TrailerUrl,
                    Poster = $"{_configuration["BackEndServer"]}/" +
                             $"{FileStorageService.UserContentFolderName}/{movie.Poster}"
                };
                return new ApiSuccessResult<MovieMD>(result);
            }
        }

        public async Task<ApiResult<MovieVMD>> GetMovieVmdById(string id)
        {
            Movie movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return new ApiErrorResult<MovieVMD>("Không tìm thấy phim");
            }
            else
            {
                var query = from m in _context.Movies
                    join c in _context.MovieCensorships on m.CensorshipId equals c.Id
                    where m.Id == id
                    select new { m, c };

                var movieVmd = await query.Select(x => new MovieVMD()
                {
                    Id = x.m.Id,
                    Length = x.m.Length,
                    Name = x.m.Name,
                    PublishDate = x.m.PublishDate,
                    Ban = x.c.Id,
                    Poster = $"{_configuration["BackEndServer"]}/" +
                             $"{FileStorageService.UserContentFolderName}/{x.m.Poster}",
                    Description = x.m.Description,
                    TrailerURL = x.m.TrailerUrl,
                }).FirstOrDefaultAsync();
                movieVmd.Genres = GetGenres(movieVmd.Id);
                return new ApiSuccessResult<MovieVMD>(movieVmd);
            }
        }

        public async Task<ApiResult<bool>> GenreAssignAsync(GenreAssignRequest request)
        {
            var film = await _context.Movies.FindAsync(request.MovieId);
            if (film == null)
            {
                return new ApiErrorResult<bool>("Phim không tồn tại");
            }

            var filmInGenres = _context.MovieInGenres.Where(X => X.MovieId == request.MovieId).Select(x => x);
            _context.MovieInGenres.RemoveRange(filmInGenres);

            var activeGenres = request.Genres.Where(x => x.Selected == true).Select(x => new MovieInGenre()
            {
                MovieId = request.MovieId,
                GenreId = x.Id
            });
            _context.MovieInGenres.AddRange(activeGenres);

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>(true, "Gán danh mục phim thành công");
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            string originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        private List<string> GetGenres(string id)
        {
            return _context.MovieInGenres.Where(x => x.MovieId == id).Select(x => x.Genre.Name).ToList();
        }

    }
}