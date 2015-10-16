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
        public ActionResult ShoppingCartView()              // Returns the Shopping Cart View. Shows all current products in the cart.
        {
            return View(getShoppingCart());
        }

        public ShoppingCartModel getShoppingCart()              // Gets the ShoppingCart object through the current Session. This object contains all the products.
        {       
            if ((Session == null) || (this.Session["ShoppingCart"]) == null)           // Create a new Shopping Cart if the user doesn't have one yet.
            {
                var cart = new ShoppingCartModel();
                Session["ShoppingCart"] = cart;
                ((ShoppingCartModel)Session["ShoppingCart"]).ItemsInShoppingCart = new List<CartItem>();
            }
            return Session["ShoppingCart"] as ShoppingCartModel;
        }

        public bool addToCart(Products newProd, int quantity)
        {
            try
            {
                foreach (var item in getShoppingCart().ItemsInShoppingCart)
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
        }

        public void removeFromCart(int prodId)
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
        }

        public double calculateTotal()
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
        }

        public void TESTAddProducts()
        {
            // -------------- TESTING SHIT ----------------------------
            Products testProdukt = new Products();
            testProdukt.productID = 0;
            testProdukt.productName = "TestProdukt";
            testProdukt.description = "Hei, jeg er et test!";
            testProdukt.imageURL = "img1.jpg";
            testProdukt.price = 200;
            testProdukt.stock = 2;
            addToCart(testProdukt, 2);

            Products testProdukt2 = new Products();
            testProdukt2.productID = 5;
            testProdukt2.imageURL = "img2.jpg";
            testProdukt2.productName = "TestProdukt2";
            testProdukt2.description = "Hei, jeg er et test!";
            testProdukt2.price = 400;
            testProdukt2.stock = 5;
            addToCart(testProdukt2, 3);
            // -------------- END OF TEST ------------------------------
        }
    }
}