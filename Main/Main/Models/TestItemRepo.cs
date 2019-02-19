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
            new Item {Code = 1, Name = "Plate", Desc = "A normal plate", WPrice = 9.30, Price = 15.99, Quantity = 150, Category = "Kitchen"},
            new Item {Code = 2, Name = "Bowl", Desc = "A normal bowl",  WPrice = 3.20, Price = 10.29, Quantity = 150, Category = "Kitchen"},
            new Item {Code = 3, Name = "Napkins", Desc = "Pile of napkins",  WPrice = 1.49, Price = 5.32, Quantity = 50, Category = "Kitchen"},
            new Item {Code = 4, Name = "Shower Gel", Desc = "Bottle of shower gel",  WPrice = 0.76, Price = 3.99, Quantity = 43, Category = "Bath"},
            new Item {Code = 5, Name = "Carrot", Desc = "Orange vegetable",  WPrice = 0.09, Price = 0.39, Quantity = 150, Category = "Produce"},
            new Item {Code = 6, Name = "White Cotton Shirt", Desc = "A normal plain shirt",  WPrice = 1.23, Price = 5.99, Quantity = 32, Category = "Clothing"},
            new Item {Code = 7, Name = "Blue Cotton Shirt", Desc = "A normal plain shirt",  WPrice = 1.23, Price = 5.99, Quantity = 32, Category = "Clothing"},
            new Item {Code = 8, Name = "Red Cotton Shirt", Desc = "A normal plain shirt",  WPrice = 1.23, Price = 5.99, Quantity = 32, Category = "Clothing"},
            new Item {Code = 9, Name = "Orange Cotton Shirt", Desc = "A normal plain shirt",  WPrice = 1.23, Price = 5.99, Quantity = 32, Category = "Clothing"}



        }.AsQueryable<Item>();

        public Item GetItems(int id)
        {
            foreach(Item item in Items)
            {
                if(id == item.Code)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
