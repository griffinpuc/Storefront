using System;
using System.Collections.Generic;
using System.Linq;

namespace Main.Models
{
    public interface IItemRepo
    {

        List<Item> Get();

        IQueryable<Item> Items { get; }

        Item GetItems(int id);

        bool AddItem(Item item);

        bool DelItem(string code);


    }

}

