using Kaffeplaneten.Controllers;
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
        public bool add(List<ProductOrders> incomingOrder, Customers customer, CustomerContext db)      // Adds a new order.
        {
            try
            {
                var newOrders = new Orders();
                newOrders.customerID = customer.customerID;                                             // Apparently it needs the customer ID inside the order... ?
                newOrders.Customers = db.Customers.Find(customer.customerID);                          // Finds the actual customer object from the database.
                db.Orders.Add(newOrders);                                                              // Allows it to get a OrderNr
                foreach (var product in incomingOrder)
                {
                    product.orderNr = newOrders.orderNr;                                               // Adding orderNr to all products.
                    product.orders = newOrders;                                                        // Adding the order object to the products
                }
                newOrders.Products = incomingOrder;                                                    // New order are assigned the list of products to be ordered.
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
        public bool add(Customers customer, CustomerContext db)            // Adds a new order.
        {
            var cartController = new ShoppingCartController();
            try
            {
                var newOrders = new Orders();
                newOrders.customerID = customer.customerID;                                          
                newOrders.Customers = db.Customers.Find(customer.customerID);                        // Finds the actual customer object from the database.
                List<ProductOrders> listOfProducts = new List<ProductOrders>();                      // Converts Shopping Cart into ProductOrders.
                foreach (var cartItem in cartController.getShoppingCart().ItemsInShoppingCart)              
                {
                    var newProductOrder = new ProductOrders();
                    newProductOrder.price = cartItem.product.price;
                    newProductOrder.productID = cartItem.product.productID;
                    newProductOrder.products = cartItem.product;
                    newProductOrder.quantity = cartItem.Quanitity;
                    listOfProducts.Add(newProductOrder);
                }
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
                    var orderModel = new OrderModel();
                    var order = (from o in db.Orders
                                 where o.orderNr == nr
                                 select o).FirstOrDefault();

                    orderModel.orderNr = order.orderNr;
                    orderModel.customerID = order.customerID;
                    var orders = (from p in db.ProductOrders
                                  where p.orderNr == nr
                                  select p).ToList();

                    orderModel.total = 0;
                    foreach (var o in orders)
                    {
                        for (int i = 0; i < o.quantity; i++)
                            orderModel.products.Add(DBProduct.toObject(o.products));
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
    } 
}