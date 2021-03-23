using MovieTheater.Data.EF;
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
        public Task<ApiResultLite> CreateAsync(ScreeningCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultLite> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultLite> UpdateAsync(ScreeningUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
