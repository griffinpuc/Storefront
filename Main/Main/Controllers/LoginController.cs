using Main.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string uname, string pass)
        {
            if (Validation.Validate(uname, pass))
            {
                return RedirectToAction("Admin", "Home");
            }

            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
