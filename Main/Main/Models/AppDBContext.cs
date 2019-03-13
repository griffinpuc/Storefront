using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
    public class AppDBContext : DbContext
    {

        //Database tables
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Item> DBItems { get; set; }

        public DbSet<LogInfo> DBCreds { get; set; }

        
        //Return all items in DBITEMS
        public List<Item> GetAllItems()
        {
            return (from item in DBItems select item).ToList();
        }


        //Add item to DBITEMS
        public void AddItem(Item item)
        {
            Add(item);
            SaveChanges();
        }


        //Update item in DBITEMS
        public void UpdateItem(Item item)
        {
            Update(item);
            SaveChanges();
        }


        //Remove item from DBITEMS
        public void RemoveItem(int? id)
        {
            Item item = DBItems.FirstOrDefault(it => it.ID == id);

            if (item != null)
            {
                Remove(item);
                SaveChanges();
            }
        }


        //Get items from category in DBITEMS
        public List<Item> GetFromCat(string cat)
        {
            return (from item in DBItems where item.Category == cat select item).ToList();
        }


        //Get item based on ID from DBITEMS
        public Item GetFromID(int? id)
        {
            return (DBItems.FirstOrDefault(it => it.ID == id));
        }


        //Get all login info from DBCREDS
        public List<LogInfo> GetAllCreds()
        {
            return (from cred in DBCreds select cred).ToList();
        }

        //METHOD: Generate hashed SHA256 from string
        public static string ComputeHash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //METHOD: Validate the entered data against stored database creds
        public List<bool> Validate(string uname, string pass)
        {

            List<LogInfo> CredList = GetAllCreds();

            List<bool> Return;

            foreach (LogInfo i in CredList)
            {
                if ((ComputeHash(uname) == i.uname) && (ComputeHash(pass) == i.pass))
                {
                    Return = new List<bool>() { true, i.permission };
                    return Return;
                }
            }

            Return = new List<bool>() { false, false };
            return Return;
        }


        //METHOD: Export CSV
        public void ExportCSV()
        {
            List<Item> _data = new List<Item>();

            foreach (Item item in GetAllItems())
            {
                _data.Add(item);
            }
            
            string json = JsonConvert.SerializeObject(_data.ToArray());

            //write string to file
            System.IO.File.WriteAllText(@"D:\path.txt", json);

            //Response.ContentType = "image/jpeg";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=SailBig.jpg");
            //Response.TransmitFile(Server.MapPath("~/images/sailbig.jpg"));
            //Response.End();
        }

    }
}
