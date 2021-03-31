using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.ReservationServices.cs
{
    public interface IReservationTypeService
    {
        Task<ApiResultLite> CreateAsync(string name);
        Task<ApiResultLite> UpdateAsync(ReservationTypeUpdateRequest request);
        Task<ApiResultLite> DeleteAsync(int id);
    }
}
