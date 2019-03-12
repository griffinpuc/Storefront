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

        private readonly AppDBContext _context;

        public HomeController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.GetAllItems());
        }

        [HttpGet]
        public ViewResult Admin()
        {
            return View(_context.GetAllItems());
        }

        [HttpPost]
        public IActionResult Admin(int x, string code, string name, string desc, string wprice, string price, string quantity, string category)
        {
            Item item = new Item
            {
                Code = x,
                Name = name,
                Descr = desc,
                WPrice = Convert.ToDouble(wprice),
                Price = Convert.ToDouble(price),
                Quantity = int.Parse(quantity),
                Category = category
            };

            _context.AddItem(item);

            return RedirectToAction("Admin", "Home");
        }

        public IActionResult Delete(int? id)
        {

            _context.RemoveItem(id);

            return RedirectToAction("Admin", "Home");

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Item item = _context.GetFromID(id);

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Item item)
        {
            _context.UpdateItem(item);

            return RedirectToAction("Admin", "Home");
        }

        public IActionResult 

    }
}
