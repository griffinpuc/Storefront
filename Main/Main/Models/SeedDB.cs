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
                        pass = Validation.ComputeHash("password")
                    },

                    new LogInfo
                    {
                        uname = Validation.ComputeHash("griffin"),
                        pass = Validation.ComputeHash("griffpass")
                    }
                    );

                context.SaveChanges();

            }

        }
    }
}
