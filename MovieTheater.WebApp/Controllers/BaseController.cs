﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieTheater.WebApp.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Request.Cookies["Token"] == null)
            {
                context.Result =  RedirectToAction("Index", "Login");
            }
           
        }
        public Guid GetUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string id = claimsIdentity.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault();
            return new Guid(id);
        }

    }
}
