using MovieTheater.Data.Models;
using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System.Threading.Tasks;

namespace MovieTheater.Application.ReservationServices.ReservationTypes
{
    public class ReservationTypeService : IReservationTypeService
    {
        private readonly MoviesContext _context;

        public ReservationTypeService(MoviesContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateAsync(string name)
        {
            ReservationType rvt = new ReservationType()
            {
                Name = name
            };
            _context.ReservationTypes.Add(rvt);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return new ApiErrorResult<bool>("Đặt vé thất bại");
            }

            return new ApiSuccessResult<bool>(true, "Đặt vé thành công");
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            ReservationType rvt = await _context.ReservationTypes.FindAsync(id);
            if (rvt == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                _context.ReservationTypes.Remove(rvt);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return new ApiSuccessResult<bool>(true);
                }
                else return new ApiErrorResult<bool>("Không xóa được");
            }
        }

        public async Task<ApiResult<bool>> UpdateAsync(ReservationTypeUpdateRequest request)
        {
            ReservationType rvt = await _context.ReservationTypes.FindAsync(request.Id);
            if (rvt == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }
            else
            {
                rvt.Id = request.Id;
                rvt.Name = request.Name;
                _context.Update(rvt);
                int result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    return new ApiErrorResult<bool>("Cập nhật thất bại");
                }

                return new ApiSuccessResult<bool>(true);
            }
        }
    }
}