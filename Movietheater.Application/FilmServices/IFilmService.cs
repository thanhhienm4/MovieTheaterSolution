using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public interface IFilmService
    {
        Task<ApiResultLite> CreateAsync(FilmCreateVMD model);
        Task<ApiResultLite> UpdateAsync(FilmVMD model);
        Task<ApiResultLite> DeleteAsync(int id);

        //ban service


    }
}
