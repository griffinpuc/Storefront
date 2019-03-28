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
using System.Web;
using System.Text;

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


        public IActionResult Index(string Category)
        {
            
            var Cat = _context.GetAllCats();
            var Item = _context.GetAllItems();

            if (Category == "sortHL")
            {
                Item = _context.Highlow();
            }

            else if (Category == "sortLH")
            {
                Item = _context.LowHigh();
            }

            else if (Category != null && Category != "All")
            {

                Item = _context.GetFromCat(Category);

            }

            var MyView = new CatItemView
            {
                Items = Item,
                Cats = Cat
            };

            return View(MyView);
            
        }

        public IActionResult Sort(string category)
        {
            return RedirectToAction("Index", "Home", new { Category = category });
        }

        public IActionResult SortLH()
        {
            return RedirectToAction("Index", "Home", new { Category = "sortLH" });

        }

        public IActionResult SortHL()
        {
            return RedirectToAction("Index", "Home", new { Category = "sortHL" });

        }

        [HttpGet]
        public ViewResult Admin(bool access)
        {
            return View(_context.GetAllItems());
        }

        [HttpPost]
        public IActionResult Admin(int x, string code, string name, string desc, string wprice, string price, string quantity, string category, string ImageURL)
        {
            if(ImageURL == null)
            {
                ImageURL = "https://www.kurin.com/wp-content/uploads/placeholder-square.jpg";
            }

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
            string json = _context.ExportJSON();

            Stream memstream = new MemoryStream(Encoding.ASCII.GetBytes(json));
            return File(memstream, "application/json", "test.json");
        }

        [HttpPost]
        public IActionResult Import(IFormFile file)
        {

            var filepath = Path.GetTempFileName();

            if (file == null)
            {
                return RedirectToAction("Admin", "Home");
            }

            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            _context.ImportJSON(filepath);

            return RedirectToAction("Admin", "Home");
        }

    }
}
