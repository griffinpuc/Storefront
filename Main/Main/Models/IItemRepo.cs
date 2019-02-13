using System;
using System.Collections.Generic;
using System.Linq;

namespace Main.Models
{
    public interface IItemRepo
    {
        IQueryable<Item> Items { get; }

        Item GetItems(int id);
    }

}

