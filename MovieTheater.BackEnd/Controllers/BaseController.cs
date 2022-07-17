using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using MovieTheater.Application.UserServices;
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
    }
}