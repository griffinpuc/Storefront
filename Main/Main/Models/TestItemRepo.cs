using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
    public class TestItemRepo : IItemRepo
    {


        public IQueryable<Item> Items => new List<Item>
        {
            new Item {Code = 1, Name = "Plate", Desc = "A normal plate", Price = 15.99, Quantity = 150, Category = "Kitchen"},
            new Item {Code = 2, Name = "Bowl", Desc = "A normal bowl", Price = 10.29, Quantity = 150, Category = "Kitchen"},
            new Item {Code = 3, Name = "Napkins", Desc = "Pile of napkins", Price = 5.32, Quantity = 50, Category = "Kitchen"}

        }.AsQueryable<Item>();
    }
}
