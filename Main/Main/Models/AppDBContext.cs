using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
    public class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Item> DBItems { get; set; }

        public DbSet<LogInfo> DBCreds { get; set; }

        public List<Item> GetAllItems()
        {
            return (from item in DBItems select item).ToList();
        }

        public void AddItem(Item item)
        {
            Add(item);
            SaveChanges();
        }

        public void UpdateItem(Item item)
        {
            Update(item);
            SaveChanges();
        }

        public void RemoveItem(int? id)
        {
            Item item = DBItems.FirstOrDefault(it => it.ID == id);

            if (item != null)
            {
                Remove(item);
                SaveChanges();
            }
            
        }

        public List<Item> GetFromCat(string cat)
        {
            return (from item in DBItems where item.Category == cat select item).ToList();
        }

        public Item GetFromID(int? id)
        {
            return (DBItems.FirstOrDefault(it => it.ID == id));
        }

    }
}
