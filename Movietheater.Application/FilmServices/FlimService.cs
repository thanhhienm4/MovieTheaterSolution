using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieTheater.Application.Common;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MovieTheater.Application.FilmServices
{
    public class FlimService : IFilmService
    {
        private readonly MovieTheaterDBContext _context;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;

        public FlimService(MovieTheaterDBContext context, IStorageService storageService, IConfiguration configuration)
        {
            _context = context;
            _storageService = storageService;
            _configuration = configuration;
        }

        public async Task<ApiResult<bool>> CreateAsync(FilmCreateRequest request)
        {
            string posterPath = await SaveFile(request.Poster);
            var room = new Film()
            {
                Name = request.Name,
                Description = request.Description,
                BanId = request.BanId,
                Length = request.Length,
                PublishDate = request.PublishDate,
                TrailerURL = request.TrailerURL,
                Poster = posterPath
            };

            await _context.AddAsync(room);
            if (await _context.SaveChangesAsync() == 0)
            {
                return new ApiErrorResult<bool>("Không thể thêm phim");
            }
            return new ApiSuccessResult<bool>(true,"Thêm phim thành công");
        }

        public async Task<ApiResult<bool>> UpdateAsync(FilmUpdateRequest request)
        {
            Film film = await _context.Films.FindAsync(request.Id);
            if (film == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy phim");
            }
            else
            {
                film.Name = request.Name;
                film.Description = request.Description;
                film.BanId = request.BanId;
                film.Length = request.Length;
                film.PublishDate = request.PublishDate;
                film.TrailerURL = request.TrailerURL;

                if (request.Poster != null)
                {
                    try
                    {
                        await _storageService.DeleteFileAsync(film.Poster);
                    }
                    catch (Exception)
                    {
                    }
                    string newPosterPath = await this.SaveFile(request.Poster);
                    film.Poster = newPosterPath;
                }

                _context.Films.Update(film);
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }

                return new ApiSuccessResult<bool>(true,"Cập nhật phim thành công");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            Film film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy phim");
            }
            else
            {
                if (_context.Screenings.Count(x => x.FilmId == film.Id) != 0)
                    return new ApiErrorResult<bool>("Xóa thất bại");
                try
                {
                    _context.Joinings.RemoveRange(_context.Joinings.Where(x => x.FilmId == id));
                    _context.SaveChanges();

                    string poster = film.Poster;
                    _context.Films.Remove(film);
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

        public async Task<ApiResult<List<FilmVMD>>> GetAllFilmAsync()
        {
            var query = from f in _context.Films
                        join b in _context.Bans on f.BanId equals b.Id
                        select new { f, b };

            var films = await query.Select(x => new FilmVMD()
            {
                Id = x.f.Id,
                Name = x.f.Name,
                PublishDate = x.f.PublishDate,
                Ban = x.b.Name,
                Poster = $"{_configuration["BackEndServer"]}/" +
                   $"{FileStorageService.UserContentFolderName}/{x.f.Poster}",
                Description = x.f.Description,
                TrailerURL = x.f.TrailerURL
            }).ToListAsync();

            return new ApiSuccessResult<List<FilmVMD>>(films);
        }

        public async Task<ApiResult<List<FilmVMD>>> GetAllPlayingFilmAsync()
        {
            var query = from f in _context.Films
                        join b in _context.Bans on f.BanId equals b.Id
                        join s in _context.Screenings on f.Id equals s.FilmId
                        where s.StartTime.Date == DateTime.Now.Date

                        select new { f, b };

            var films = await query.Distinct().Select(x => new FilmVMD()
            {
                Id = x.f.Id,
                Name = x.f.Name,
                PublishDate = x.f.PublishDate,
                Ban = x.b.Name,
                Poster = $"{_configuration["BackEndServer"]}/" +
                   $"{FileStorageService.UserContentFolderName}/{x.f.Poster}",
                Description = x.f.Description,
                Length = x.f.Length,
                TrailerURL = x.f.TrailerURL
            }).ToListAsync();

            return new ApiSuccessResult<List<FilmVMD>>(films);
        }

        public async Task<ApiResult<List<FilmVMD>>> GetAllUpcomingFilmAsync()
        {
            var query = from f in _context.Films
                        join b in _context.Bans on f.BanId equals b.Id
                        where f.PublishDate.Date > DateTime.Now.Date
                        select new { f, b };

            var films = await query.Select(x => new FilmVMD()
            {
                Id = x.f.Id,
                Name = x.f.Name,
                PublishDate = x.f.PublishDate,
                Ban = x.b.Name,
                Poster = $"{_configuration["BackEndServer"]}/" +
                   $"{FileStorageService.UserContentFolderName}/{x.f.Poster}",
                Description = x.f.Description,
                TrailerURL = x.f.TrailerURL,
                Length = x.f.Length
            }).ToListAsync();

            return new ApiSuccessResult<List<FilmVMD>>(films);
        }

        public async Task<ApiResult<PageResult<FilmVMD>>> GetFilmPagingAsync(FilmPagingRequest request)
        {
            var query = from f in _context.Films
                        join b in _context.Bans on f.BanId equals b.Id
                        select new { f, b };

            if (!string.IsNullOrWhiteSpace(request.Keyword))
                query = query.Where(x => x.f.Name.Contains(request.Keyword)
                                        || x.f.Id.ToString().Contains(request.Keyword)
                                        || x.b.Name.Contains(request.Keyword));

            int totalRow = await query.CountAsync();
            var films = query.OrderBy(x => x.f.Name).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new FilmVMD()
                {
                    Id = x.f.Id,
                    Name = x.f.Name,
                    PublishDate = x.f.PublishDate,
                    Ban = x.b.Name,
                    Poster = $"{_configuration["BackEndServer"]}/" +
                   $"{FileStorageService.UserContentFolderName}/{x.f.Poster}",
                    Description = x.f.Description,
                    TrailerURL = x.f.TrailerURL
                }).ToList();

            if (films != null)
            {
                foreach (var film in films)
                {
                    film.Genres = GetGenres(film.Id);
                }
            }

            var pageResult = new PageResult<FilmVMD>()
            {
                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = films,
            };

            return new ApiSuccessResult<PageResult<FilmVMD>>(pageResult);
        }

        public async Task<ApiResult<FilmMD>> GetFilmMDById(int id)
        {
            Film film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return new ApiErrorResult<FilmMD>("Không tìm thấy phim");
            }
            else
            {
                var result = new FilmMD()
                {
                    Id = film.Id,
                    Name = film.Name,
                    BanId = film.BanId,
                    Description = film.Description,
                    Length = film.Length,
                    PublishDate = film.PublishDate,
                    TrailerURL = film.TrailerURL,
                    Poster = $"{_configuration["BackEndServer"]}/" +
                    $"{FileStorageService.UserContentFolderName}/{film.Poster}"
                };
                return new ApiSuccessResult<FilmMD>(result);
            }
        }

        public async Task<ApiResult<FilmVMD>> GetFilmVMDById(int id)
        {
            Film film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return new ApiErrorResult<FilmVMD>("Không tìm thấy phim");
            }
            else
            {
                var query = from f in _context.Films
                            join b in _context.Bans on f.BanId equals b.Id
                            where f.Id == id
                            select new { f, b };

                var filmVMD = await query.Select(x => new FilmVMD()
                {
                    Id = x.f.Id,
                    Length = x.f.Length,
                    Name = x.f.Name,
                    PublishDate = x.f.PublishDate,
                    Ban = x.b.Name,
                    Poster = $"{_configuration["BackEndServer"]}/" +
                    $"{FileStorageService.UserContentFolderName}/{x.f.Poster}",
                    Description = x.f.Description,
                    TrailerURL = x.f.TrailerURL,
                }).FirstOrDefaultAsync();
                filmVMD.Genres = GetGenres(filmVMD.Id);
                filmVMD.Directors = GetDirectors(filmVMD.Id);
                filmVMD.Actors = GetActors(filmVMD.Id);
                return new ApiSuccessResult<FilmVMD>(filmVMD);
            }
        }

        public async Task<ApiResult<bool>> GenreAssignAsync(GenreAssignRequest request)
        {
            var film = await _context.Films.FindAsync(request.FilmId);
            if (film == null)
            {
                return new ApiErrorResult<bool>("Phim không tồn tại");
            }

            // check genres available
            List<int> genres = request.Genres.Select(x => Int32.Parse(x.Id)).ToList();
            if (!CheckGenres(genres))
            {
                return new ApiErrorResult<bool>("Yêu cầu không hợp lệ");
            }

            var filmInGenres = _context.FilmInGenres.Where(X => X.FilmId == request.FilmId).Select(x => x);
            _context.FilmInGenres.RemoveRange(filmInGenres);

            var activeGenres = request.Genres.Where(x => x.Selected == true).Select(x => new FilmInGenre()
            {
                FilmId = request.FilmId,
                FilmGenreId = Int32.Parse(x.Id)
            });
            _context.FilmInGenres.AddRange(activeGenres);

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>(true,"Gán danh mục phim thành công");
        }

        public async Task<ApiResult<bool>> PosAssignAsync(PosAssignRequest request)
        {
            if ((await _context.Films.FindAsync(request.FilmId)) == null)
                return new ApiErrorResult<bool>("Không tìm thấy phim");
            if ((await _context.Peoples.FindAsync(request.PeopleId)) == null)
                return new ApiErrorResult<bool>("Không tìm thấy nghệ sĩ");
            if ((await _context.Positions.FindAsync(request.PosId)) == null)
                return new ApiErrorResult<bool>("Không tìm vai trò");

            var joining = await _context.Joinings.FindAsync(request.FilmId, request.PeopleId, request.PosId);
            if (joining != null)
                return new ApiErrorResult<bool>("Đã tồn tại");

            try
            {
                Joining jn = new Joining()
                {
                    FilmId = request.FilmId,
                    PeppleId = request.PeopleId,
                    PositionId = request.PosId
                };

                await _context.Joinings.AddAsync(jn);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>(true);
            }
            catch (DbUpdateException)
            {
                return new ApiErrorResult<bool>("Thêm thất bại");
            }
        }

        public async Task<ApiResult<bool>> DeletePosAssignAsync(PosAssignRequest request)
        {
            var joining = await _context.Joinings.Where(x => x.FilmId == request.FilmId &&
                                                         x.PositionId == request.PosId &&
                                                         x.PeppleId == request.PeopleId).FirstOrDefaultAsync();
            if (joining == null)
                return new ApiErrorResult<bool>("Yêu cầu không hợp lệ");
            else
            {
                _context.Joinings.Remove(joining);
                _context.SaveChanges();
                return new ApiSuccessResult<bool>(true,"Xóa phim thành công");
            }
        }

        public async Task<ApiResult<List<JoiningPosVMD>>> GetJoiningAsync(int id)
        {
            var query = from j in _context.Joinings
                        join p in _context.Peoples on j.PeppleId equals p.Id
                        where j.FilmId == id
                        select new { j, p };

            var res = new List<JoiningPosVMD>();

            var positions = await _context.Positions.ToListAsync();
            foreach (var position in positions)
            {
                JoiningPosVMD joiningPos = new JoiningPosVMD();
                joiningPos.Name = position.Name;
                joiningPos.Joinings = query.Where(x => x.j.PositionId == position.Id).Select(x =>
                                            new JoiningVMD()
                                            {
                                                FilmId = x.j.FilmId,
                                                PeopleId = x.j.PeppleId,
                                                PosId = x.j.PositionId,
                                                Name = x.p.Name
                                            }).ToList();
                res.Add(joiningPos);
            }

            return new ApiSuccessResult<List<JoiningPosVMD>>(res);
        }

        private bool CheckGenres(List<int> genres)
        {
            var query = genres.Join(_context.FilmGenre,
                x => x,
                fg => fg.Id,
                (x, fg) => x).ToList();

            HashSet<int> setGenres = new HashSet<int>(genres);
            if (setGenres.Count == query.Count)
                return true;
            else
                return false;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            string originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        private List<string> GetGenres(int id)
        {
            return _context.FilmInGenres.Where(x => x.FilmId == id).Join(_context.FilmGenre,
                                                            fig => fig.FilmGenreId,
                                                            fg => fg.Id,
                                                            (fig, fg) => fg.Name).ToList();
        }

        private List<string> GetActors(int id)
        {
            return _context.Joinings.Where(x => x.FilmId == id && x.PositionId == 1).Join(_context.Peoples,
                                                            fig => fig.PeppleId,
                                                            p => p.Id,
                                                            (fig, p) => p.Name).ToList();
        }

        private List<string> GetDirectors(int id)
        {
            return _context.Joinings.Where(x => x.FilmId == id && x.PositionId == 2).Join(_context.Peoples,
                                                             fig => fig.PeppleId,
                                                             p => p.Id,
                                                             (fig, p) => p.Name).ToList();
        }
    }
}