using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace MovieTheater.Admin.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Request.Cookies["Token"] == null)
            {
                context.Result = RedirectToAction("Index", "Login");
            }
        }

        public string GetUserId()
        {
            if (User.Identity is ClaimsIdentity claimsIdentity)
            {
                string id = claimsIdentity.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value)
                    .FirstOrDefault();
                return id;
            }

            return null;
        }
    }
}