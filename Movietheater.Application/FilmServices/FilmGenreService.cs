using MovieTheater.Data.EF;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public class FilmGenreService : IFilmGenreService
    {
        private readonly MovieTheaterDBContext _context;
        public FilmGenreService (MovieTheaterDBContext context)
        {
            _context = context;
        }
        public Task<ApiResultLite> CreateAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultLite> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResultLite> UpdateAsync(FilmGenreUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
