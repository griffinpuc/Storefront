using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
    public class SeedDB
    {

        public static void PopulateDB(AppDBContext context)
        {
            context.Database.EnsureCreated();

            if (!context.DBCreds.Any())
            {
                context.DBCreds.AddRange(

                    new LogInfo
                    {
                        uname = Validation.ComputeHash("admin"),
                        pass = Validation.ComputeHash("password"),
                        permission = true
                    },

                    new LogInfo
                    {
                        uname = Validation.ComputeHash("griffin"),
                        pass = Validation.ComputeHash("griffpass"),
                        permission = false
                    }
                    );

                context.SaveChanges();

            }      

        }

        public static void PopulateDB2(AppDBContext context)
        {
            context.Database.EnsureCreated();

            if (!context.DBItems.Any())
            {
                context.DBItems.AddRange(

                    new Item { Code = 1, Name = "Plate", Desc = "A normal plate", WPrice = 9.30, Price = 15.99, Quantity = 150, Category = "Kitchen" },
                    new Item { Code = 2, Name = "Bowl", Desc = "A normal bowl", WPrice = 3.20, Price = 10.29, Quantity = 150, Category = "Kitchen" },
                    new Item { Code = 9, Name = "Orange Cotton Shirt", Desc = "A normal plain shirt", WPrice = 1.23, Price = 5.99, Quantity = 32, Category = "Clothing" }
                    );

                context.SaveChanges();

            }
        }
    }
}
