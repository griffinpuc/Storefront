﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

        public DbSet<Contact> DBContacts { get; set; }


        public List<Contact> GetAllContacts()
        {
            return (from contact in DBContacts select contact).ToList();
        }

        public void AddContact(Contact contact)
        {
            Add(contact);
            SaveChanges();
        }

        
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

        //Remove contact
        public void RemoveCont(int? id)
        {
            Contact contact = DBContacts.FirstOrDefault(it => it.ID == id);

            Remove(contact);
            SaveChanges();
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
            LogInfo log = new LogInfo()
            {
                uname = ComputeHash("admin"),
                pass = ComputeHash("password"),
                permission = true

            };
        

            Add(log);
            SaveChanges();

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


        //METHOD: Export JSON
        public string ExportJSON()
        {
            List<Item> Items = GetAllItems();
            return JsonConvert.SerializeObject(Items, Formatting.Indented);

        }

        //METHOD: Import JSON
        public void ImportJSON(string path)
        {
            using (TransactionScope txnScop = new TransactionScope())
            {
                //Database.BeginTransaction();

                Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[DBItems] OFF");

                string json = File.ReadAllText(path);
                List<Item> Items = JsonConvert.DeserializeObject<List<Item>>(json);

                foreach (Item item in GetAllItems())
                {
                    Remove(item);
                }

                foreach (Item item in Items)
                {
                    Add(item);
                }

                Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[DBItems] ON");
                SaveChanges();
                txnScop.Complete();
            }
        }


        //METHOD: Return list of all categories
        public List<string> GetAllCats()
        {
            return (from item in DBItems select item.Category).ToList();
        }


        //METHOD: Sort by high to low
        public List<Item> Highlow()
        {
            return (from item in DBItems orderby item.Price ascending select item).ToList();
            
        }


        //METHOD: Sort by low to high
        public List<Item> LowHigh()
        {
            return (from item in DBItems orderby item.Price descending select item).ToList();
        }


    }
}
