using Microsoft.AspNetCore.Mvc;
using MovieTheater.Application.UserServices;

namespace MovieTheater.BackEnd.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUserService _userService;

        public BaseController(IUserService userService)
        {
            _userService = userService;
        }
    }
}