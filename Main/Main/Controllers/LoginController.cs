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

        private readonly AppDBContext _context;

        public LoginController(AppDBContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string uname, string pass)
        {

            List<bool> Temp = _context.Validate(uname, pass);
            if (Temp[0] && !Temp[1])
            {
                return RedirectToAction("Mod", "Home");
            }

            else if (Temp[0] && Temp[1])
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
