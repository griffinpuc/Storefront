using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Main.Models;

namespace Main.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        IItemRepo repository;

        public HomeController(IItemRepo repo)
        {
            repository = repo;
        }

        public ViewResult Shop() => View(repository.Items);

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
