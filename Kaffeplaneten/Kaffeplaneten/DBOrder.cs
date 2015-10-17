﻿using Kaffeplaneten.Controllers;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Kaffeplaneten
{
    
    public class DBOrder
    {
        public static bool add(OrderModel orderModel)/*Legger Orders og ProductOrders inn i databasen. CustomerID og pruductID-ene må være med i modellen*/
        {
            using (var db = new CustomerContext())
            {
            try
            {
                    var customer = db.Customers.Find(orderModel.customerID);
                    if (customer == null)
                        return false;
                    var order = new Orders();
                    order.Customers = customer;
                    db.Orders.Add(order);
                    db.SaveChanges();
                    orderModel.orderNr = order.orderNr;
                    return addProductOrders(orderModel);
                    
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                }
                return false;
            }
        }

        public static bool addProductOrders(OrderModel orderModel)/*Legger ProductOrders inn i databasen. OrderNr og pruductID-ene må være med i modellen*/
            {
            using (var db = new CustomerContext())
                {
                try
                {
                    var order = db.Orders.Find(orderModel.orderNr);
                    if (order == null)
                        return false;
                    var productOrders = new List<ProductOrders>();
                    foreach (var p in orderModel.products)
                    {
                        var productOrder = new ProductOrders();
                        productOrder.orders = order;
                        productOrder.price = p.price;
                        productOrder.products = db.Products.Find(p.productID);
                        productOrder.quantity = 1;
                        var temp = productOrders.Find(x => x.products.productID == p.productID);
                        if (temp == null)
                        {
                            productOrder.price = p.price;
                            productOrders.Add(productOrder);
                        }
                        else
                        {
                            temp.price += p.price;
                            temp.quantity++;
                        }
                    }
                    foreach (var po in productOrders)
                    {
                        db.ProductOrders.Add(po);
                        Debug.WriteLine(po.productID);
                    }
                    db.SaveChanges();
                    return true;
                    }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                }
                return false;
            }
        }
        public bool add(ShoppingCartModel cart, Customers customer, CustomerContext db)            // Adds a new order.
        {

            try
            {
                var newOrders = new Orders();
                newOrders.customerID = customer.customerID;                                          
                newOrders.Customers = db.Customers.Find(customer.customerID);                        // Finds the actual customer object from the database.
                List<ProductOrders> listOfProducts = new List<ProductOrders>();                      // Converts Shopping Cart into ProductOrders.
                /*foreach (var cartItem in cart.ItemsInShoppingCart)              
                {
                    var newProductOrder = new ProductOrders();
                    newProductOrder.price = cartItem.product.price;
                    newProductOrder.productID = cartItem.product.productID;
                    newProductOrder.products = cartItem.product;
                    newProductOrder.quantity = cartItem.Quanitity;
                    listOfProducts.Add(newProductOrder);
                }*/
                newOrders.Products = listOfProducts;                                                   // New order are assigned the list of products to be ordered.
                db.Orders.Add(newOrders);                                                              // Allows it to get a OrderNr 
                db.SaveChanges();
                return true;                                                                           // Returns to OrderController to be saved.
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }
        public static OrderModel find(int nr)
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var order = (from o in db.Orders
                                 where o.orderNr == nr
                                 select o).FirstOrDefault();
                    var orderModel = new OrderModel();

                    orderModel.orderNr = order.orderNr;
                    orderModel.customerID = order.customerID;
                    var productOrders = (from p in db.ProductOrders
                                  where p.orderNr == nr
                                  select p).ToList();

                    orderModel.total = 0;
                    foreach (var o in productOrders)
                    {
                        for (int i = 0; i < o.quantity; i++)
                            orderModel.products.Add(DBProduct.find(o.products.productID));
                        orderModel.total += o.price;
                    }
                    return orderModel;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nSource:\n" + ex.Source);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                    //Environment.Exit(1);
                }
            }
            return null;
        }

        public static List<OrderModel> findOrders(int id)
        {

            using (var db = new CustomerContext())
            {
                try
                {
                    var orders = (from o in db.Orders
                                 where o.orderNr == id
                                 select o).ToList();
                    var orderModelList = new List<OrderModel>();
                    foreach (var o in orders)
                        orderModelList.Add(find(o.orderNr));
                    return orderModelList;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                }
            }
            return null;
        }
    } 
}