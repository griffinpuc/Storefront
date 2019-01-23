using System;
using System.Linq;

namespace Main.Models
{
    public interface IItemRepo
    {
        IQueryable<Item> Items { get; }

    }
}

