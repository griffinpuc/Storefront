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

        [HttpGet]
        public ViewResult Admin() => View(repository.Items);

        [HttpPost]
        public IActionResult Admin(string code, string name, string desc, string wprice, string price, string quantity, string category)
        {

            Item item = new Item
            {
                Code = int.Parse(code),
                Name = name,
                Desc = desc,
                WPrice = Convert.ToDouble(wprice),
                Price = Convert.ToDouble(price),
                Quantity = int.Parse(quantity),
                Category = category
            };

            if (repository.AddItem(item))
            {

                return View(repository.Items);

            }

            return RedirectToAction("Home", "Index");
        }

        public ViewResult Mod() => View(repository.Items);


        public IActionResult Edit(int? id)
        {
            Item edit_item = repository.GetItems(id.Value);

            return View(edit_item);
        }

        public IActionResult Delete(string id)
        {

            if (repository.DelItem(id))
            {
                return RedirectToAction("Admin", "Home");
            }

            return RedirectToAction("Home", "Index");

        }
    }
}
