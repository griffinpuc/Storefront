using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Main.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Main.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostEnvironment;

        private readonly AppDBContext _context;

        public HomeController(AppDBContext context, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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
        public IActionResult Admin(int x, string code, string name, string desc, string wprice, string price, string quantity, string category, string ImageURL)
        {
            Item item = new Item
            {
                Code = x,
                Name = name,
                Descr = desc,
                WPrice = Convert.ToDouble(wprice),
                Price = Convert.ToDouble(price),
                Quantity = int.Parse(quantity),
                Category = category,
                ImageURL = ImageURL
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

        public IActionResult Export()
        {
            _context.ExportJSON();

            return RedirectToAction("Admin", "Home");
        }

        [HttpPost]
        public IActionResult Import(IFormFile file)
        {

            var filepath = Path.GetTempFileName();

            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            _context.ImportJSON(filepath);

            return RedirectToAction("Admin", "Home");
        }

    }
}
