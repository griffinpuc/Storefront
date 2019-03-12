using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
    public class CartItem
    {

        public string ItemId { get; set; }
        public int ProductId { get; set; }
        public string CartId { get; set; }
        public Item Product { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
