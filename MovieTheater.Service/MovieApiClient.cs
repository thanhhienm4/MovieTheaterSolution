using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieTheater.Models.Catalog.Film;
using MovieTheater.Models.Catalog.Film.MovieGenres;
using MovieTheater.Models.Common.ApiResult;
using MovieTheater.Models.Common.Paging;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using MovieTheater.Common.Constants;

namespace MovieTheater.Api
{
    public class MovieApiClient : BaseApiClient
    {
        public MovieApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
           IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration,
            httpContextAccessor)
        { }

        public async Task<ApiResult<bool>> CreateAsync(MovieCreateRequest request)
        {
            var requestContent = new MultipartFormDataContent();

            if (request.Poster != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Poster.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Poster.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Poster", request.Poster.FileName);
            }
            requestContent.Add(new StringContent(request.Description.ToString()), "Description");
            requestContent.Add(new StringContent(request.CensorshipId.ToString()), "CensorshipId");
            requestContent.Add(new StringContent(request.Length.ToString()), "Length");
            requestContent.Add(new StringContent(request.Name.ToString()), "Name");
            requestContent.Add(new StringContent(request.PublishDate.ToString()), "PublishDate");
            requestContent.Add(new StringContent(request.TrailerURL.ToString()), "TrailerURL");
            HttpClient client = GetHttpClient();

            var response = await client.PostAsync($"Api/Film/Create", requestContent);

            return await GetResultAsync<bool>(response);
        }

        public async Task<ApiResult<bool>> UpdateAsync(MovieUpdateRequest request)
        {
            var requestContent = new MultipartFormDataContent();

            if (request.Poster != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Poster.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Poster.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "Poster", request.Poster.FileName);
            }
            requestContent.Add(new StringContent(request.Description.ToString()), "Description");
            requestContent.Add(new StringContent(request.Id.ToString()), "Id");
            requestContent.Add(new StringContent(request.CensorshipId), "CensorshipId");
            requestContent.Add(new StringContent(request.Length.ToString()), "Length");
            requestContent.Add(new StringContent(request.Name.ToString()), "Name");
            requestContent.Add(new StringContent(request.PublishDate.ToString()), "PublishDate");
            requestContent.Add(new StringContent(request.TrailerURL.ToString()), "TrailerURL");
            //return await PutAsync<bool>("Api/Movie/Update", requestContent);

            HttpClient client = GetHttpClient();
            var response = await client.PutAsync($"Api/Film/Update", requestContent);

            return await GetResultAsync<bool>(response);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            return await DeleteAsync<bool>($"Api/Movie/Delete/{id}");
        }

        public async Task<ApiResult<PageResult<MovieVMD>>> GetFilmPagingAsync(FilmPagingRequest request)
        {
            return await PostAsync<PageResult<MovieVMD>>($"{APIConstant.ApiMovie}/{APIConstant.GetMoviePaging}", request);
        }

        public async Task<ApiResult<MovieVMD>> GetFilmVMDByIdAsync(string id)
        {
            return await GetAsync<MovieVMD>($"Api/Movie/GetFilmVMDById/{id}");
        }

        public async Task<ApiResult<MovieMD>> GetFilmMDByIdAsync(int id)
        {
            return await GetAsync<MovieMD>($"Api/Movie/GetFilmMDById/{id}");
        }

        public async Task<ApiResult<List<MovieVMD>>> GetAllFilmAsync()
        {
            return await GetAsync<List<MovieVMD>>($"Api/Movie/GetAllFilm");
        }

        public async Task<ApiResult<bool>> AssignGenre(GenreAssignRequest request)
        {
            return await PostAsync<bool>($"Api/Movie/GenreAssign", request);
        }

        public async Task<ApiResult<List<MovieVMD>>> GetAllPlayingFilmAsync()
        {
            return await GetAsync<List<MovieVMD>>($"Api/Movie/getAllPlayingFilm");
        }

        public async Task<ApiResult<List<MovieVMD>>> GetAllUpcomingFilmAsync()
        {
            return await GetAsync<List<MovieVMD>>($"Api/Movie/getAllUpcomingFilm");
        }

        public async Task<ApiResult<List<MovieGenreVMD>>> GetAllFilmGenreAsync()
        {
            return await GetAsync<List<MovieGenreVMD>>($"Api/FilmGenre/GetAllFilmGenre");
        }

        public async Task<ApiResult<bool>> PosAssignAsync(PosAssignRequest request)
        {
            return await PostAsync<bool>($"Api/Movie/PosAssign", request);
        }

        public async Task<ApiResult<bool>> DeletePosAssignAsync(PosAssignRequest request)
        {
            return await PostAsync<bool>($"Api/Movie/DeletePosAssign", request);
        }

        public async Task<ApiResult<List<JoiningPosVMD>>> GetJoiningAsync(int id)
        {
            return await GetAsync<List<JoiningPosVMD>>($"Api/Movie/GetJoining/{id}");
        }
    }
}