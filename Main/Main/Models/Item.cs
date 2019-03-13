using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
    public class Item
    {
        //Item class constructor
        public int ID { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }
        public double WPrice { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }

    }
}

