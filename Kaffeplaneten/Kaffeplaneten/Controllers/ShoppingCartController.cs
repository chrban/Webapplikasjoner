using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaffeplaneten.Controllers
{
    public class ShoppingCartController : SuperController
    {

        public ActionResult ShoppingCartView()                  // Returns the Shopping Cart View. Shows all current products in the cart.
        {
            var orderModel = (OrderModel)Session[SHOPPING_CART];
            if (orderModel == null)
                return View();
            //return View(getShoppingCart());
            return View(orderModel);
        }
        [HttpPost]
        public ActionResult ShoppingCartView(OrderModel orderModel)
        {
            if (orderModel == null)
                return View();
            Session[SHOPPING_CART] = null;
            Session[CHECKOUT_ORDER] = orderModel;
            return RedirectToAction("confirmOrderView", "Order");
        }
        [HttpPost]
        public void createCart()
        {

            if (Session["SHOPPING_CART"] == null)                                    // Already created?
            {
                Session["SHOPPING_CART"] = new OrderModel();                          // This is the cart. It will be initialized by the createCart() method.
                var cart = ((OrderModel)Session["SHOPPING_CART"]);
                cart.products = new List<ProductModel>();
                Debug.WriteLine("KLARTE Å LAGE EN NY CART!");
                testProducts();
                return;
            }
            Debug.WriteLine("FAILED TO MAKE NEW CART!");
        }

        [HttpGet]
        public List<ProductModel> getShoppingCartItems()              // Gets the ShoppingCart object through the current Session. This object contains all the products.
        {
            var cart = ((OrderModel)Session["SHOPPING_CART"]);
            if (cart != null)
            {
                return cart.products;
            }
            return null;
        }

        public bool addToCart(ProductModel newProd, int quantity)
        {
            var cart = ((OrderModel)Session["SHOPPING_CART"]);
            try
            {
                foreach(var productInList in cart.products)
                {                
                    if (productInList.productID == newProd.productID)
                    {
                        productInList.quantity += quantity;
                        calculateTotal();
                        return true;
                    }
                }
                cart.products.Add(newProd);
                Debug.WriteLine("Added" + newProd.productName);
                calculateTotal();
                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine("FAILED TO ADD ITEM TO CART!");
            };
            return false;
        }

        public bool removeFromCart(ProductModel productToBeRemoved, int quantity)
        {
            var cart = ((OrderModel)Session["SHOPPING_CART"]);
            try
            {
                foreach (var productInList in cart.products)
                {
                    if (productInList.productID == productToBeRemoved.productID)
                    {
                        if ((productInList.quantity - quantity) < 0)                    // Product needs to have one or more quantity to be relevant
                            cart.products.Remove(productInList);                        // Otherwise remove entirely.
                        else
                            productInList.quantity -= quantity;                         // Only a certain amount has been removed, not the entire product.
                        
                        calculateTotal();
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

        public void calculateTotal()
        {
            double currentTotal = 0;
            foreach(var item in ((OrderModel)Session["SHOPPING_CART"]).products)
            {
                currentTotal += (item.price * item.quantity);
            }
            Debug.WriteLine("Nå er total: " + currentTotal);
            ((OrderModel)Session["SHOPPING_CART"]).total = currentTotal;
        }

        //Denne kan vel slettes?? (christer)
        // Ja, det skal den etter at den er confirmed working (Sondre)

        public void testProducts()
        {
            var one = new ProductModel();
            one.category = "Kaffe";
            one.description = "God kaffe";
            one.imageURL = "img1.jpg";
            one.price = 10000;
            one.productName = "KaffeKaffe";
            one.stock = 1;
            one.productID = 1;
            one.quantity = 1;

            var two = new ProductModel();
            two.category = "Kaffe2";
            two.description = "God kaffe2";
            two.imageURL = "img2.jpg";
            two.price = 30;
            two.productName = "KaffeKaffe2";
            two.stock = 3;
            two.productID = 2;
            two.quantity = 3;

            var three = new ProductModel();
            three.category = "Kaffe3";
            three.description = "God kaffe3";
            three.imageURL = "img3.jpg";
            three.price = 40;
            three.productName = "KaffeKaffe3";
            three.stock = 6;
            three.productID = 3;
            three.quantity = 2;

            addToCart(one, 1);
            Debug.WriteLine("1 ADDED!");
            addToCart(two, 2);
            Debug.WriteLine("2 ADDED!");
            addToCart(three, 3);
            Debug.WriteLine("3 ADDED!");
        }
    }
}