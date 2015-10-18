﻿using Kaffeplaneten.Models;
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
                Session[SHOPPING_CART] = new OrderModel();                          // This is the cart. It will be initialized by the createCart() method.
        }

        [HttpGet]
        public List<ProductModel> getShoppingCartItems()              // Gets the ShoppingCart object through the current Session. This object contains all the products.
        {
            var cart = ((OrderModel)Session[SHOPPING_CART]);
            if (cart != null)
                return cart.products;
            return new List<ProductModel>();
        }

        [HttpPost]
        public bool addToCart(int newProd ,int inQuantity)
        {
            var ProductList = (List<ProductModel>)Session[PRODUCT_LIST];
            var cart = ((OrderModel)Session[SHOPPING_CART]);
            if (cart == null)
                cart = new OrderModel();
            if (ProductList == null)
                ProductList = new List<ProductModel>();
           
            foreach(var productInList in cart.products)
            {                
                if (productInList.productID == newProd)
                {
                    productInList.quantity += inQuantity;
                    calculateTotal();
                    return true;                                                       // Product already exists in cart.
                }
            }
            foreach (var product in ProductList)
            {
                if (product.productID == newProd)
                {
                    product.quantity = inQuantity;
                    cart.products.Add(product);
                    Debug.WriteLine("Produkt lagt til: " + product.productName);
            calculateTotal();
            return true;
        }

            }

            return false;    
        }

            

        public bool removeFromCart(int productToBeRemoved)
        {

            var cart = ((OrderModel)Session[SHOPPING_CART]);
            if (cart == null)
                return false;
            try
            {
                foreach (var productInList in cart.products)
                {
                    if (productInList.productID == productToBeRemoved)
                    {
                       cart.products.Remove(productInList);                        // Otherwise remove entirely.
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

        [HttpGet]
        public int updateQuantity(int productId, int quantity)
        {
            if (Session[SHOPPING_CART] == null)
                return 0;
            foreach (var productInList in ((OrderModel)Session[SHOPPING_CART]).products)
            {
                if(productInList.productID == productId)
                {
                    if(quantity < 0)
                    {
                        removeFromCart(productInList.productID);
                        return 0;
                    }
                    productInList.quantity = quantity;
                    return productInList.quantity;
                }
            }
            return 0;
        }

        public double getSubTotal(int prodId)
        {
            if (Session[SHOPPING_CART] == null)
                return 0;
            foreach (var productInList in ((OrderModel)Session[SHOPPING_CART]).products)
            {
                if (productInList.productID == prodId)
                {
                    return (productInList.price * productInList.quantity);
                }
            }
            return 0.00;
        }

        [HttpGet]
        public double calculateTotal()
        {
            if (Session[SHOPPING_CART] == null)
                return 0;
            double currentTotal = 0;

            foreach(var item in ((OrderModel)Session[SHOPPING_CART]).products)
                currentTotal += (item.price * item.quantity);

            Debug.WriteLine("Nå er total: " + currentTotal);
            ((OrderModel)Session[SHOPPING_CART]).total = currentTotal;
            return currentTotal;
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
                addToCart(one.productID,1);
            if(two != null)
                addToCart(two.productID,1);
            if(three != null)
            addToCart(three.productID,1);
        }
    }
}