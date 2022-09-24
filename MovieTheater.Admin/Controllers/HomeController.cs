using Microsoft.AspNetCore.Mvc;
using MovieTheater.Admin.Models;
using MovieTheater.Api;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            if (!User.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == "Admin"))
            {
                return RedirectToAction("Index", "Retail");
            }

            ViewBag.SuccessMsg = TempData["Result"];
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}