﻿using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public class PositionService : IPositionService
    {
        public Task<ApiResultLite> CreateAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultLite> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultLite> UpdateAsync(PositionUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
