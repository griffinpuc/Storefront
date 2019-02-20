using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main.Models;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    public class DBItemsController : Controller
    {
        private readonly AppDBContext _Context;

        public DBItemsController(AppDBContext context)
        {
            _Context = context;
        }

        public IActionResult Index()
        {
            return View(_Context.DBItems.ToList());
        }
    }
}