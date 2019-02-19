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

        public ViewResult Admin() => View(repository.Items);


        public IActionResult Edit(int? id)
        {
            Item edit_item = repository.GetItems(id.Value);

            return View(edit_item);
        }
    }
}
