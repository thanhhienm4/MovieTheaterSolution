using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using Movietheater.Application.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUserService _userService;
        public BaseController(IUserService userService)
        {
            _userService = userService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string id = claimsIdentity.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault();
            if(id != null)
            {
                var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1];
                var res = _userService.CheckToken(new Guid(id), accessToken);
                if (res.IsSuccessed != true)
                    context.HttpContext.Response.StatusCode = 401;


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
