using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieApiClient _filmApiClient;

        public HomeController( MovieApiClient filmApiClient)
        {
            _filmApiClient = filmApiClient;
        }

        public IActionResult Index()
        {
            var playingFilms = _filmApiClient.GetAllPlayingFilmAsync();
            var upComingFilms =  _filmApiClient.GetAllUpcomingFilmAsync();
            Task.WaitAll(playingFilms, upComingFilms);
            ViewData["PlayingFilms"] = playingFilms.Result.ResultObj;
            ViewData["UpcomingFilms"] = upComingFilms.Result.ResultObj;
            return View();
        }
    }
}