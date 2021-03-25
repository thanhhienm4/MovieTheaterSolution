using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
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
        public ScreeningService(MovieTheaterDBContext context)
        {
            _context = context;
        }
        public async Task<ApiResultLite> CreateAsync(ScreeningCreateRequest request)
        {
            Screening screening = new Screening()
            {
                TimeStart = request.TimeStart,
                Surcharge = request.Surcharge,
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
                screening.Surcharge = request.Surcharge;
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
    }
}
