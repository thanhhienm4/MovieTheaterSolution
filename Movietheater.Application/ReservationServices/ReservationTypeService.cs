using MovieTheater.Data.EF;
using MovieTheater.Data.Entities;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.ReservationServices
{
    public class ReservationTypeService : IReservationTypeService
    {
        private readonly MovieTheaterDBContext _context;
        public ReservationTypeService(MovieTheaterDBContext context)
        {
            _context = context;
        }
        public async Task<ApiResultLite> CreateAsync(string name)
        {
            ReservationType rvt  = new ReservationType()
            {
                Name = name
            };
            _context.ReservationTypes.Add(rvt);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResultLite("Thêm thất bại");
            }

            return new ApiSuccessResultLite("Thêm thành công");
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            ReservationType rvt = await _context.ReservationTypes.FindAsync(id);
            if (rvt == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                _context.ReservationTypes.Remove(rvt);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResultLite("Xóa thành công");
                }
                else return new ApiSuccessResultLite("Không xóa được");
            }
        }

        public async Task<ApiResultLite> UpdateAsync(ReservationTypeUpdateRequest request)
        {
            ReservationType rvt = await _context.ReservationTypes.FindAsync(request.Id);
            if (rvt == null)
            {
                return new ApiErrorResultLite("Không tìm thấy");
            }
            else
            {
                rvt.Id = request.Id;
                rvt.Name = request.Name;
                _context.Update(rvt);
                int result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    return new ApiErrorResultLite("Cập nhật thất bại");
                }
                return new ApiSuccessResultLite("Cập nhật thành công");
            }
        }
    }
}
