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
        public ScreeningService(MovieTheaterDBContext context,IFilmService filmService)
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
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResultLite("Thêm thất bại");
            }

            return new ApiSuccessResultLite("Thêm thành công");
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
                _context.Screenings.Remove(screening);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResultLite("Xóa thành công");
                }
                else return new ApiSuccessResultLite("Không xóa được");
            }
        }

        public Task<PageResult<ScreeningVMD>> GetScreeningPagingAsync(ScreeningPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PageResult<ScreeningVMD>> GetScreeningPagingRequest(ScreeningPagingRequest request)
        {
            throw new NotImplementedException();
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
                int rs = await _context.SaveChangesAsync();
                if (rs == 0)
                {
                    return new ApiErrorResultLite("Cập nhật thất bại");
                }
                return new ApiSuccessResultLite("Cập nhật thành công");
            }
        }

        public async Task<ApiResult<ScreeningVMD>> GetScreeningByIdAsync(int id)
        {
            var screening = await _context.Screenings.FindAsync(id);
            if(screening == null)
            {
                return new ApiErrorResult<ScreeningVMD>("Không tìm thấy xuất chiếu");


            }else
            {
                var screeningVMD = new ScreeningVMD()
                {
                    Id = screening.Id,
                    FilmId = screening.FilmId,
                    RoomId = screening.RoomId,
                    TimeStart = screening.TimeStart,
                    KindOfScreeningId = screening.KindOfScreeningId

                };
                return new ApiSuccessResult<ScreeningVMD>(screeningVMD);
            }
        }

        public async Task<ApiResult<List<FilmScreeningVMD>>> GetFilmScreeningInday(DateTime? date)
           
        {
            if (date == null)
                date = DateTime.Now;

            var screenings = await  _context.Screenings.Where(x => x.TimeStart.Date == date.GetValueOrDefault().Date).
                                                Select(x => new ScreeningVMD() 
                                                { 
                                                    Id = x.Id,
                                                    TimeStart = x.TimeStart,
                                                    FilmId = x.FilmId,
                                                    RoomId = x.RoomId
                                                    
                                                }).ToListAsync();

            List<FilmScreeningVMD> filmScreenings = new List<FilmScreeningVMD>();
            Dictionary<int, List<ScreeningVMD>> dic = new Dictionary<int, List<ScreeningVMD>>();

            foreach (var screening in screenings)
            {
                if(!dic.ContainsKey(screening.FilmId))
                {
                    dic.Add(screening.Id, new List<ScreeningVMD>());                   
                }
                dic[screening.FilmId].Add(screening);
            }

            foreach( var pair in dic)
            {
                filmScreenings.Add(new FilmScreeningVMD()
                {
                    Film = (await _filmService.GetFilmById(pair.Key)).ResultObj,
                    ListScreening = pair.Value,
                }) ;
            }
            return new ApiSuccessResult<List<FilmScreeningVMD>>(filmScreenings);




        }
    }
}
