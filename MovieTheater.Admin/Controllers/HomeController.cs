using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieTheater.Admin.Models;
using MovieTheater.Api;
using MovieTheater.Models.Common.ChartTable;
using System;
using System.Diagnostics;

namespace MovieTheater.Admin.Controllers
{
    
    [Authorize(Roles ="Admin")]
   
    public class HomeController : BaseController
    {
        private readonly StatiticApiClient _statiticApiClient;
        public HomeController(StatiticApiClient statiticApiClient)
        {
            _statiticApiClient = statiticApiClient;
        }
        public async System.Threading.Tasks.Task<IActionResult> Index()
        {
            
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