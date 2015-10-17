using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaffeplaneten.Controllers
{
    public class ShoppingCartController : Controller
    {

        public ShoppingCartModel cart;                          // This is the cart. It will be initialized by the createCart() method.

        public ActionResult ShoppingCartView()                  // Returns the Shopping Cart View. Shows all current products in the cart.
        {
            //return View(getShoppingCart());
            return View();
        }
        [HttpPost]
        public void createCart()
        {
            if(cart == null)                                    // Already created?
            {
                cart = new ShoppingCartModel();
                Debug.WriteLine("KLARTE Å LAGE EN NY CART!");
                testProducts();
                return;
            }
            Debug.WriteLine("FAILED TO MAKE NEW CART!");
        }

        public List<JsonResult> getShoppingCartItems()              // Gets the ShoppingCart object through the current Session. This object contains all the products.
        {
            if(cart != null)
            {
                return cart.ItemsInCart;
            }
            return null;
        }
        
        public bool addToCart(JsonResult newProd, int quantity)
        {
            try
            {
                for(int i = 0; i < cart.ItemsInCart.Count; i++)
                {                
                    if (cart.ItemsInCart[i].Equals(newProd))
                    {
                        cart.Quantity[i] += quantity;
                        return true;
                    }
                }
                cart.ItemsInCart.Add(newProd);
                cart.Quantity.Add(quantity);
                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine("FAILED TO ADD ITEM TO CART!");
            };
            return false;
        }

        public bool removeFromCart(JsonResult productToBeRemoved, int quantity)
        {
            try
            {
                foreach (var item in cart.ItemsInCart)
                {
                    if (item.Equals(productToBeRemoved))
                    {
                        cart.ItemsInCart.Remove(item);
                        return true;
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("FAILED TO REMOVE ITEM TO CART!");
            };
            return false;
        }

        public int amountOfItems()
        {
            return cart.ItemsInCart.Count;
        }

        public void testProducts()
        {
            var productDB = new DBProduct();

            JsonResult one = Json(productDB.getProductsByCategory("Test"), JsonRequestBehavior.AllowGet);
            JsonResult two = Json(productDB.getProductsByCategory("Test2"), JsonRequestBehavior.AllowGet);
            JsonResult three = Json(productDB.getProductsByCategory("Test3"), JsonRequestBehavior.AllowGet);

            addToCart(one, 1);
            Debug.WriteLine("1 ADDED!");
            addToCart(two, 2);
            Debug.WriteLine("2 ADDED!");
            addToCart(three, 3);
            Debug.WriteLine("3 ADDED!");
        }
    }
}