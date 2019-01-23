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
            new Item {ID = 1, Name = "Plate", Desc = "A normal plate", Price = 15.00, Quantity = 150},
            new Item {ID = 1, Name = "Bowl", Desc = "A normal bowl", Price = 10.00, Quantity = 150},
            new Item {ID = 1, Name = "Napkins", Desc = "Pile of napkins", Price = 5.00, Quantity = 50}

        }.AsQueryable<Item>();
    }
}
