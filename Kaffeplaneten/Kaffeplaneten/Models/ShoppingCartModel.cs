using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.Models
{
    public class ShoppingCartModel
    {
        public List<CartItem> ItemsInShoppingCart { get; set; }
    }
    public class CartItem
    {
        public Products product { get; set; }
        public int Quanitity { get; set; }
    }
}