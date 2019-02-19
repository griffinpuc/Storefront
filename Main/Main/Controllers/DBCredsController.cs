using Main.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Controllers
{
    public class DBCredsController : Controller
    {

        private readonly AppDBContext _Context;

        public DBCredsController(AppDBContext context)
        {
            _Context = context;
        }

        public IActionResult Index()
        {
            return View(_Context.DBCreds.ToList());
        }
    }
}
