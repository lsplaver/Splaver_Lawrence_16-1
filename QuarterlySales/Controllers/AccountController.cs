using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuarterlySales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public AccountController(UserManager<User> userMngr, SignInManager<User> signInMngr)
        {
            userManager = userMngr;
            signInManager = signInMngr;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}
