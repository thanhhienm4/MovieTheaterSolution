using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTheater.Admin.Controllers
{
    //hello
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Request.Cookies["Token"] == null)
            {
                context.Result =  RedirectToAction("Index", "Login");
            }
           
        }

    }
}
