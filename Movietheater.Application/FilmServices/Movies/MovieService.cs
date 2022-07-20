using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieTheater.Application.Common;
using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;

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
                    }
                    catch
                    {
                        // ignored
                    }

                    string newPosterPath = await SaveFile(request.Poster);
                    movie.Poster = newPosterPath;
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
            using var transaction = _context.Database.BeginTransaction();
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
                    _context.Joinings.RemoveRange(_context.Joinings.Where(x => x.MovieId == id));
                    _context.SaveChanges();

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
                Ban = x.c.Name,
                Poster = $"{_configuration["BackEndServer"]}/" +
                         $"{FileStorageService.UserContentFolderName}/{x.m.Poster}",
                Description = x.m.Description,
                TrailerURL = x.m.TrailerUrl
            }).ToListAsync();

            return new ApiSuccessResult<List<MovieVMD>>(Movies);
        }

        public async Task<ApiResult<List<MovieVMD>>> GetAllPlayingAsync()
        {
            var query = from m in _context.Movies
                join c in _context.MovieCensorships on m.CensorshipId equals c.Id
                join s in _context.Screenings on m.Id equals s.MovieId
                where s.StartTime.Date == DateTime.Now.Date
                select new { m, c };

            var Movies = await query.Distinct().Select(x => new MovieVMD()
            {
                Id = x.m.Id,
                Name = x.m.Name,
                PublishDate = x.m.PublishDate,
                Ban = x.c.Name,
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
                Ban = x.c.Name,
                Poster = $"{_configuration["BackEndServer"]}/" +
                         $"{FileStorageService.UserContentFolderName}/{x.m.Poster}",
                Description = x.m.Description,
                TrailerURL = x.m.TrailerUrl,
                Length = x.m.Length
            }).ToListAsync();

            return new ApiSuccessResult<List<MovieVMD>>(Movies);
        }

        public async Task<ApiResult<PageResult<MovieVMD>>> GetPagingAsync(FilmPagingRequest request)
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
                    Ban = x.c.Name,
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

        public async Task<ApiResult<MovieVMD>> GetFilmVMDById(string id)
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

                var movieVMD = await query.Select(x => new MovieVMD()
                {
                    Id = x.m.Id,
                    Length = x.m.Length,
                    Name = x.m.Name,
                    PublishDate = x.m.PublishDate,
                    Ban = x.c.Name,
                    Poster = $"{_configuration["BackEndServer"]}/" +
                             $"{FileStorageService.UserContentFolderName}/{x.m.Poster}",
                    Description = x.m.Description,
                    TrailerURL = x.m.TrailerUrl,
                }).FirstOrDefaultAsync();
                movieVMD.Genres = GetGenres(movieVMD.Id);
                movieVMD.Directors = GetDirectors(movieVMD.Id);
                movieVMD.Actors = GetActors(movieVMD.Id);
                return new ApiSuccessResult<MovieVMD>(movieVMD);
            }
        }

        //public async Task<ApiResult<bool>> GenreAssignAsync(GenreAssignRequest request)
        //{
        //    var film = await _context.Movies.FindAsync(request.MovieId);
        //    if (film == null)
        //    {
        //        return new ApiErrorResult<bool>("Phim không tồn tại");
        //    }

        //    // check genres available
        //    List<int> genres = request.Genres.Select(x => int.Parse(x.Id)).ToList();
        //    if (!CheckGenres(genres))
        //    {
        //        return new ApiErrorResult<bool>("Yêu cầu không hợp lệ");
        //    }

        //    var filmInGenres = _context.MovieInGenres.Where(X => X.MovieId == request.MovieId).Select(x => x);
        //    _context.MovieInGenres.RemoveRange(filmInGenres);

        //    var activeGenres = request.Genres.Where(x => x.Selected == true).Select(x => new MovieInGenre()
        //    {
        //        MovieId = request.MovieId,
        //        GenreId = x.Id
        //    });
        //    _context.MovieInGenres.AddRange(activeGenres);

        //    await _context.SaveChangesAsync();

        //    return new ApiSuccessResult<bool>(true, "Gán danh mục phim thành công");
        //}

        //public async Task<ApiResult<bool>> PosAssignAsync(PosAssignRequest request)
        //{
        //    if (await _context.Movies.FindAsync(request.Id) == null)
        //        return new ApiErrorResult<bool>("Không tìm thấy phim");
        //    if (await _context.Peoples.FindAsync(request.PeopleId) == null)
        //        return new ApiErrorResult<bool>("Không tìm thấy nghệ sĩ");
        //    if (await _context.Positions.FindAsync(request.PosId) == null)
        //        return new ApiErrorResult<bool>("Không tìm vai trò");

        //    var joining = await _context.Joinings.FindAsync(request.Id, request.PeopleId, request.PosId);
        //    if (joining != null)
        //        return new ApiErrorResult<bool>("Đã tồn tại");

        //    try
        //    {
        //        Joining jn = new Joining()
        //        {
        //            Id = request.Id,
        //            PeppleId = request.PeopleId,
        //            PositionId = request.PosId
        //        };

        //        await _context.Joinings.AddAsync(jn);
        //        await _context.SaveChangesAsync();
        //        return new ApiSuccessResult<bool>(true);
        //    }
        //    catch (DbUpdateException)
        //    {
        //        return new ApiErrorResult<bool>("Thêm thất bại");
        //    }
        //}

        //public async Task<ApiResult<bool>> DeletePosAssignAsync(PosAssignRequest request)
        //{
        //    var joining = await _context.Joinings.Where(x => x.Id == request.Id &&
        //                                                 x.PositionId == request.PosId &&
        //                                                 x.PeppleId == request.PeopleId).FirstOrDefaultAsync();
        //    if (joining == null)
        //        return new ApiErrorResult<bool>("Yêu cầu không hợp lệ");
        //    else
        //    {
        //        _context.Joinings.Remove(joining);
        //        _context.SaveChanges();
        //        return new ApiSuccessResult<bool>(true, "Xóa phim thành công");
        //    }
        //}

        //public async Task<ApiResult<List<JoiningPosVMD>>> GetJoiningAsync(int id)
        //{
        //    var query = from j in _context.Joinings
        //                join p in _context.Peoples on j.PeppleId equals p.Id
        //                where j.Id == id
        //                select new { j, p };

        //    var res = new List<JoiningPosVMD>();

        //    var positions = await _context.Positions.ToListAsync();
        //    foreach (var position in positions)
        //    {
        //        JoiningPosVMD joiningPos = new JoiningPosVMD();
        //        joiningPos.RowName = position.RowName;
        //        joiningPos.Joinings = query.Where(x => x.j.PositionId == position.Id).Select(x =>
        //                                    new JoiningVMD()
        //                                    {
        //                                        Id = x.j.Id,
        //                                        PeopleId = x.j.PeppleId,
        //                                        PosId = x.j.PositionId,
        //                                        RowName = x.p.RowName
        //                                    }).ToList();
        //        res.Add(joiningPos);
        //    }

        //    return new ApiSuccessResult<List<JoiningPosVMD>>(res);
        //}

        //private bool CheckGenres(List<int> genres)
        //{
        //    var query = genres.Join(_context.FilmGenre,
        //        x => x,
        //        fg => fg.Id,
        //        (x, fg) => x).ToList();

        //    HashSet<int> setGenres = new HashSet<int>(genres);
        //    if (setGenres.Count == query.Count)
        //        return true;
        //    else
        //        return false;
        //}

        private async Task<string> SaveFile(IFormFile file)
        {
            string originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        private List<string> GetGenres(string id)
        {
            return _context.MovieInGenres.Where(x => x.MovieId == id).Join(_context.MovieGenres,
                fig => fig.MovieId,
                fg => fg.Id,
                (fig, fg) => fg.Name).ToList();
        }

        private List<string> GetActors(string id)
        {
            return _context.Joinings.Where(x => x.MovieId == id).Join(_context.Actors,
                fig => fig.ActorId,
                p => p.Id,
                (fig, p) => p.Name).ToList();
        }

        private List<string> GetDirectors(string id)
        {
            return _context.Joinings.Where(x => x.MovieId == id).Join(_context.Actors,
                fig => fig.ActorId,
                p => p.Id,
                (fig, p) => p.Name).ToList();
        }
    }
}