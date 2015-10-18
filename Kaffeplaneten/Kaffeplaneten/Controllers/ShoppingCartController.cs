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

            if (Session["ShoppingCart"] == null)                                    // Already created?
            {
                Session["ShoppingCart"] = new ShoppingCartModel();                          // This is the cart. It will be initialized by the createCart() method.
                var cart = ((ShoppingCartModel)Session["ShoppingCart"]);
                cart.ItemsInCart = new List<JsonResult>();
                cart.Quantity = new List<int>();
                Debug.WriteLine("KLARTE Å LAGE EN NY CART!");
                //testProducts();
                return;
            }
            Debug.WriteLine("FAILED TO MAKE NEW CART!");
        }

        [HttpGet]
        public List<JsonResult> getShoppingCartItems()              // Gets the ShoppingCart object through the current Session. This object contains all the products.
        {
            var cart = ((ShoppingCartModel)Session["ShoppingCart"]);
            if (cart != null)
            {
                return cart.ItemsInCart;
            }
            return null;
        }

        public JsonResult getItemAt(int spot)              // Gets the ShoppingCart object through the current Session. This object contains all the products.
        {
            var cart = ((ShoppingCartModel)Session["ShoppingCart"]);
            if (cart != null)
            {
                return cart.ItemsInCart.ElementAt(spot);
            }
            return null;
        }

        public bool addToCart(JsonResult newProd, int quantity)
        {
            var cart = ((ShoppingCartModel)Session["ShoppingCart"]);
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
                Debug.WriteLine("Added" + quantity);
                Debug.WriteLine("Amount:" + amountOfItems());
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
            var cart = ((ShoppingCartModel)Session["ShoppingCart"]);
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
            return ((ShoppingCartModel)Session["ShoppingCart"]).ItemsInCart.Count;
        }
        public void addToCart(ProductModel productModel)
        {
            var orderModel = (OrderModel)Session[SHOPPING_CART];
            if (orderModel == null)
                orderModel = new OrderModel();
            foreach (var p in orderModel.products)
                if (p.productID == productModel.productID)
                {
                    p.quantity += productModel.quantity;
                    return;
                }
            orderModel.products.Add(productModel);
            Session[SHOPPING_CART] = orderModel;
        }
        //Denne kan vel slettes?? (christer)
        /* public void testProducts()
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
         */
    }
}