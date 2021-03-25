using MovieTheater.Models.Catalog.Reservation;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.ReservationServices.cs
{
    public class ReservationTypeService : IReservationTypeService
    {
        public async Task<ApiResultLite> CreateAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResultLite> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResultLite> UpdateAsync(ReservationTypeUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
