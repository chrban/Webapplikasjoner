using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaffeplaneten.Controllers
{
    public class ShoppingCartController : Controller
    {

        ShoppingCartModel cart = new ShoppingCartModel();

        public ActionResult ShoppingCartView()              // Returns the Shopping Cart View. Shows all current products in the cart.
        {
            //return View(getShoppingCart());
            return View();
        }

        public void createCart()
        {

        }

        /*public ActionResult getShoppingCartItems(ShoppingCartModel model)              // Gets the ShoppingCart object through the current Session. This object contains all the products.
        {
            foreach (var items in model)
            {
                var test = new JsonResult();  
            }
            return View(outputString);
        }*/

        /*public bool addToCart(JsonResult newProd, int quantity)
        {
            try
            {
                foreach (var item in ItemsInShoppingCart)
                {
                    if (item.product.productID == newProd.productID)
                    {
                        item.Quanitity += quantity;
                        return true;
                    }
                }
                var newCartItem = new CartItem();
                newCartItem.product = newProd;
                newCartItem.Quanitity = quantity;
                getShoppingCart().ItemsInShoppingCart.Add(newCartItem);
            }
            catch (Exception error)
            {
                Console.WriteLine("FAILED TO ADD ITEM: " + newProd.productID + " TO CART!");
            };
            return false;
        }*/

        /*public void removeFromCart(int prodId, int quantity)
        {
            try
            {
                foreach (var item in getShoppingCart().ItemsInShoppingCart)
                {
                    if (item.product.productID == prodId)
                    {
                        getShoppingCart().ItemsInShoppingCart.Remove(item);
                        return;
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("FAILED TO REMOVE ITEM: " + prodId + " TO CART!");
            };
        }*/

        /*public double calculateTotal()
        {
            double totalPrice = 0;
            foreach (var item in getShoppingCart().ItemsInShoppingCart)
            {
                totalPrice += (item.product.price * item.Quanitity);
            }
            return totalPrice;
        }

        public int amountOfItems()
        {
            return getShoppingCart().ItemsInShoppingCart.Count;
        }*/
    }
}