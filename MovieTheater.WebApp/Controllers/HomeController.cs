using Microsoft.AspNetCore.Mvc;
using MovieTheater.Api;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScreeningApiClient _screeningApiClient;
        private readonly FilmApiClient _filmApiClient;

        public HomeController(ScreeningApiClient screeningApiClient, FilmApiClient filmApiClient)
        {
            _screeningApiClient = screeningApiClient;
            _filmApiClient = filmApiClient;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["PlayingFilms"] = (await _filmApiClient.GetAllPlayingFilmAsync()).ResultObj;
            ViewData["UpcomingFilms"] = (await _filmApiClient.GetAllUpcomingFilmAsync()).ResultObj;
            return View();
        }
    }
}