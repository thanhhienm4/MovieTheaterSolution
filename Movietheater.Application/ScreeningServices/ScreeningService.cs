using Microsoft.EntityFrameworkCore;
using Movietheater.Application.FilmServices;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.ScreeningServices
{
    public class ScreeningService : IScreeningService
    {
        private readonly MovieTheaterDBContext _context;
        private readonly IFilmService _filmService;
        public ScreeningService(MovieTheaterDBContext context, IFilmService filmService)
        {
            _context = context;
            _filmService = filmService;
        }
        public async Task<ApiResultLite> CreateAsync(ScreeningCreateRequest request)
        {
            Screening screening = new Screening()
            {
                TimeStart = request.TimeStart,
                // Surcharge = request.Surcharge,
                FilmId = request.FilmId,
                RoomId = request.RoomId,
                KindOfScreeningId = request.KindOfScreeningId

            };
            _context.Screenings.Add(screening);

            try
            {
                int result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    return new ApiErrorResultLite("Thêm thất bại");
                }
                return new ApiSuccessResultLite("Thêm thành công");
            }
            catch(DbUpdateException e)
            {
                return new ApiErrorResultLite("Thêm thất bại");
            }
           

           
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            Screening screening = await _context.Screenings.FindAsync(id);
            if (screening == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else

            {
                try
                {
                    if (screening.Tickets != null)
                    {
                        screening.Active = false;
                        _context.Screenings.Update(screening);
                    }
                    else
                    {
                        _context.Screenings.Remove(screening);
                    }

                    await _context.SaveChangesAsync();
                    return new ApiSuccessResultLite("Xóa thành công");

                }catch(DbUpdateException e)
                {
                    return new ApiErrorResultLite("Xóa thất bại");
                }
                

              
              
            }
        }

        public async Task<ApiResult<PageResult<ScreeningVMD>>> GetScreeningPagingAsync(ScreeningPagingRequest request)
        {

            var query = from s in _context.Screenings
                        join f in _context.Films on s.FilmId equals f.Id
                        join r in _context.Rooms on s.RoomId equals r.Id
                        join kos in _context.KindOfScreenings on s.KindOfScreeningId equals kos.Id
                        select new { s, f, r, kos };

            if (request.Keyword != null)
            {
                query = query.Where(x => x.s.Id.ToString().Contains(request.Keyword) ||
                                         x.s.TimeStart.ToString().Contains(request.Keyword) ||
                                         x.f.Name.Contains(request.Keyword));

            }
            if (request.Date != null)
                query = query.Where(x => x.s.TimeStart.Date == request.Date);

            PageResult<ScreeningVMD> result = new PageResult<ScreeningVMD>();
            result.TotalRecord = await query.CountAsync();
            result.PageIndex = request.PageIndex;
            result.PageSize = request.PageSize;

            var rooms = query.Select(x => new ScreeningVMD()
            {
                Id = x.s.Id,
                Film = x.f.Name,
                Room = x.r.Name,
                TimeStart = x.s.TimeStart,
                KindOfScreening = x.kos.Name

            }).OrderByDescending(x => x.TimeStart).Skip((request.PageIndex - 1) * (request.PageSize)).Take(request.PageSize).ToList();
            result.Item = rooms;

            return new ApiSuccessResult<PageResult<ScreeningVMD>>(result);
        }



        public Task<PageResult<FilmScreeningVMD>> GetScreeningTimePagingAsync(ScreeningPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResultLite> UpdateAsync(ScreeningUpdateRequest request)
        {
            Screening screening = await _context.Screenings.FindAsync(request.Id);
            if (screening == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                screening.Id = request.Id;
                screening.TimeStart = request.TimeStart;
                //screening.Surcharge = request.Surcharge;
                screening.FilmId = request.FilmId;
                screening.RoomId = request.RoomId;
                screening.KindOfScreeningId = request.KindOfScreeningId;

                _context.Update(screening);

                try
                {
                    int rs = await _context.SaveChangesAsync();
                    if (rs == 0)
                    {
                        return new ApiErrorResultLite("Cập nhật thất bại");
                    }
                    return new ApiSuccessResultLite("Cập nhật thành công");
                }catch(DbUpdateException e)
                {
                    return new ApiErrorResultLite("Cập nhật thất bại");
                }
            
            }
        }

        public async Task<ApiResult<ScreeningMD>> GetScreeningMDByIdAsync(int id)
        {
            var screening = await _context.Screenings.FindAsync(id);
            if (screening == null)
            {
                return new ApiErrorResult<ScreeningMD>("Không tìm thấy xuất chiếu");


            }
            else
            {
                var screeningVMD = new ScreeningMD()
                {
                    Id = screening.Id,
                    FilmId = screening.FilmId,
                    RoomId = screening.RoomId,
                    TimeStart = screening.TimeStart,
                    KindOfScreeningId = screening.KindOfScreeningId

                };
                return new ApiSuccessResult<ScreeningMD>(screeningVMD);
            }
        }

        public async Task<ApiResult<ScreeningVMD>> GetScreeningVMDByIdAsync(int id)
        {
            var screening = await _context.Screenings.FindAsync(id);
            if (screening == null)
            {
                return new ApiErrorResult<ScreeningVMD>("Không tìm thấy xuất chiếu");
            }
            else
            {
                var query = from s in _context.Screenings
                            join f in _context.Films on s.FilmId equals f.Id
                            join r in _context.Rooms on s.RoomId equals r.Id
                            join kos in _context.KindOfScreenings on s.KindOfScreeningId equals kos.Id
                            select new { s, f, r, kos };

                var screeningVMD = await query.Select(x => new ScreeningVMD()
                {
                    Id = x.s.Id,
                    Film = x.f.Name,
                    Room = x.r.Name,
                    TimeStart = x.s.TimeStart,
                    KindOfScreening = x.kos.Name

                }).FirstOrDefaultAsync();
                return new ApiSuccessResult<ScreeningVMD>(screeningVMD);
            }
        }

        public async Task<ApiResult<List<FilmScreeningVMD>>> GetFilmScreeningInday(DateTime? date)

        {
            if (date == null)
                date = DateTime.Now;

            var screenings = await _context.Screenings.Where(x => x.TimeStart.Date == date.GetValueOrDefault().Date).
                                                Select(x => new ScreeningMD()
                                                {
                                                    Id = x.Id,
                                                    TimeStart = x.TimeStart,
                                                    FilmId = x.FilmId,
                                                    RoomId = x.RoomId

                                                }).ToListAsync();

            List<FilmScreeningVMD> filmScreenings = new List<FilmScreeningVMD>();
            Dictionary<int, List<ScreeningMD>> dic = new Dictionary<int, List<ScreeningMD>>();

            foreach (var screening in screenings)
            {
                if (!dic.ContainsKey(screening.FilmId))
                {
                    dic.Add(screening.FilmId, new List<ScreeningMD>());
                }
                dic[screening.FilmId].Add(screening);
            }

            foreach (var pair in dic)
            {
                filmScreenings.Add(new FilmScreeningVMD()
                {
                    Film = (await _filmService.GetFilmVMDById(pair.Key)).ResultObj,
                    ListScreening = pair.Value,
                });
            }
            return new ApiSuccessResult<List<FilmScreeningVMD>>(filmScreenings);




        }

        public async Task<ApiResult<ScreeningOfFilmInWeekVMD>> GetListCreeningOfFilmInWeek(int filmId)
        {
            var screenings = await _context.Screenings.Where(x => x.TimeStart.Date >= DateTime.Now && 
                                                    x.TimeStart <= DateTime.Now.AddDays(6).Date &&
                                                    x.FilmId == filmId).
                                               Select(x => new ScreeningMD()
                                               {
                                                   Id = x.Id,
                                                   TimeStart = x.TimeStart,
                                                   FilmId = x.FilmId,
                                                   RoomId = x.RoomId

                                               }).ToListAsync();

            ScreeningOfFilmInWeekVMD sof = new ScreeningOfFilmInWeekVMD();
            sof.Film = (await _filmService.GetFilmVMDById(filmId)).ResultObj;
            sof.Screenings = new List<List<ScreeningMD>>();
            for (int i=0;i<=6;i++)
            {
                var listScrening = screenings.Where(x => x.TimeStart.Date == DateTime.Now.AddDays(i).Date).ToList();
                sof.Screenings.Add(listScrening);
            }

            return new ApiSuccessResult<ScreeningOfFilmInWeekVMD> (sof);
        }


    }
}
