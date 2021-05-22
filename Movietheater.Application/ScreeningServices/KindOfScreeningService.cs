using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Screening;
using MovieTheater.Models.Common.ApiResult;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movietheater.Application.ScreeningServices
{
    public class KindOfScreeningService : IkindOfScreeningService
    {
        private readonly MovieTheaterDBContext _context;

        public KindOfScreeningService(MovieTheaterDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResultLite> CreateAsync(KindOfScreeningCreateRequest request)
        {
            KindOfScreening kindOfScreening = new KindOfScreening()
            {
                Name = request.Name,
                Surcharge = request.Surcharge
            };
            _context.KindOfScreenings.Add(kindOfScreening);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResultLite("Thêm thất bại");
            }

            return new ApiSuccessResultLite("Thêm thành công");
        }

        public async Task<ApiResultLite> UpdateAsync(KindOfScreeningUpdateRequest request)
        {
            KindOfScreening screening = await _context.KindOfScreenings.FindAsync(request.Id);
            if (screening == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                screening.Name = request.Name;
                screening.Surcharge = request.Surcharge;
                _context.Update(screening);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResultLite("Cập nhật thành công");
                }
                else
                {
                    return new ApiErrorResultLite("Cập nhật thất bại");
                }
            }
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            KindOfScreening screening = await _context.KindOfScreenings.FindAsync(id);
            if (screening == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                _context.KindOfScreenings.Remove(screening);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResultLite("Xóa thành công");
                }
                else
                {
                    return new ApiErrorResultLite("Xóa thất bại");
                }
            }
        }

        public async Task<ApiResult<List<KindOfScreeningVMD>>> GetAllKindOfScreeningAsync()
        {
            var kindOfScreenings = await _context.KindOfScreenings.Select(x => new KindOfScreeningVMD()
            {
                Id = x.Id,
                Name = x.Name,
                Surcharge = x.Surcharge
            }).ToListAsync();

            return new ApiSuccessResult<List<KindOfScreeningVMD>>(kindOfScreenings);
        }
    }
}