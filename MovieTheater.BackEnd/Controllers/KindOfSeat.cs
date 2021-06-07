using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    public class KindOfSeat : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
