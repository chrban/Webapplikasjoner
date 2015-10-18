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
            {
                createCart();
                return View(Session[SHOPPING_CART]);
            }
            //return View(getShoppingCart());
            return View(orderModel);
        }
        /*[HttpPost]
        public ActionResult ShoppingCartView(OrderModel orderModel)
        {
            if (orderModel == null)
                return View();
            Session[SHOPPING_CART] = null;
            Session[CHECKOUT_ORDER] = orderModel;
            return RedirectToAction("confirmOrderView", "Order");
        }*/
        [HttpPost]
        public void createCart()
        {

            if (Session[SHOPPING_CART] == null)                                    // Already created?
            {
                Session[SHOPPING_CART] = new OrderModel();                          // This is the cart. It will be initialized by the createCart() method.
                var cart = ((OrderModel)Session[SHOPPING_CART]);
                //cart.products = new List<ProductModel>();//gjøres av konstruktør
                Debug.WriteLine("KLARTE Å LAGE EN NY CART!");
                testProducts();                                         // ---- DENNE MÅ FJERNES FØR INNLEVERING. TESTEMETODE!
                return;
            }
            Debug.WriteLine("FAILED TO MAKE NEW CART!");
        }

        [HttpGet]
        public List<ProductModel> getShoppingCartItems()              // Gets the ShoppingCart object through the current Session. This object contains all the products.
        {
            var cart = ((OrderModel)Session[SHOPPING_CART]);
            if (cart != null)
            {
                return cart.products;
            }
            return null;
        }

        public bool addToCart(ProductModel newProd, int quantity)
        {
            var cart = ((OrderModel)Session[SHOPPING_CART]);
            if (cart == null)
                cart = new OrderModel();
           
            foreach(var productInList in cart.products)
            {                
                if (productInList.productID == newProd.productID)
                {
                    productInList.quantity += quantity;
                    calculateTotal();
                    return true;
                }
            }
            newProd.quantity = quantity;
            cart.products.Add(newProd);
            calculateTotal();
            return true;
        }

        public bool removeFromCart(ProductModel productToBeRemoved, int quantity)
        {
            var cart = ((OrderModel)Session[SHOPPING_CART]);
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
            foreach(var item in ((OrderModel)Session[SHOPPING_CART]).products)
        {
                currentTotal += (item.price * item.quantity);
            }
            Debug.WriteLine("Nå er total: " + currentTotal);
            ((OrderModel)Session[SHOPPING_CART]).total = currentTotal;
        }
        public bool addToCart(ProductModel productModel)
        {
            var cart = ((OrderModel)Session[SHOPPING_CART]);
            if (cart == null)
                cart = new OrderModel();

            foreach (var productInList in cart.products)
            {
                if (productInList.productID == productModel.productID)
                {
                    productInList.quantity += productModel.quantity;
                    calculateTotal();
                    return true;
                }
            }
            cart.products.Add(productModel);
            calculateTotal();
            return true;
        }


        public void testProducts()
            /* Oppgradert slik at den pruker faktiske produkter i databasen. 
            Bruk TestClass til å generere produkter hvis det trengs -> Slå den på i Global.asax, kjør 3 ganger, slå den av igjen*/
        //Denne kan vel slettes?? (christer)
        // Ja, det skal den etter at alt er confirmed working (Sondre)
        {
            var one = DBProduct.find(1);

            var two = DBProduct.find(2);

            var three = DBProduct.find(3);
            if(one != null)
                addToCart(one, 1);
            if(two != null)
                addToCart(two, 2);
            if(three != null)
                addToCart(three, 3);
        }
    }
}