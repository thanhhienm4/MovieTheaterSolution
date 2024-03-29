﻿using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public interface IFilmService
    {
        Task<ApiResultLite> CreateAsync(FilmCreateRequest model);
        Task<ApiResultLite> UpdateAsync(FilmUpdateRequest model);
        Task<ApiResultLite> DeleteAsync(int id);
        Task<PageResult<FilmVMD>> GetFilmPagingAsync(FilmPagingRequest request);
        Task<ApiResult<FilmVMD>> GetFilmById(int id);

        //ban service


    }
}
