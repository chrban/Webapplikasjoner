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
        public static bool add(OrderModel orderModel)/*Legger Orders og ProductOrders inn i databasen. CustomerID og pruductID-ene må være med i modellen*/
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var customer = db.Customers.Find(orderModel.customerID);
                    if (customer == null)//tester om ordren tilhører en eksisterende kunde
                        return false;
                    var order = new Orders();
                    order.Customers = customer;
                    db.Orders.Add(order);
                    db.SaveChanges();
                    orderModel.orderNr = order.orderNr;//lagrer ordrenummeret i modellen for senere bruk
                    return addProductOrders(orderModel);//legger til produkt ordrene
                    
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
                    if (order == null)//tester om ordren eksisterer
                        return false;
                    foreach (var p in orderModel.products)//Opretter ProductOrders fra modellen
                    {
                        p.stock -= p.quantity;
                        DBProduct.updateQuantity(p);
                        var productOrder = new ProductOrders();
                        productOrder.orders = order;
                        productOrder.price = p.price;
                        productOrder.products = db.Products.Find(p.productID);
                        productOrder.quantity = p.quantity;
                        productOrder.price = p.price * p.quantity;
                        db.ProductOrders.Add(productOrder);
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

        public static OrderModel find(int nr)//Henter ut en OrderModel fra en ordre med ordreNr lik nr
        {
            var orderModel = new OrderModel();
            using (var db = new CustomerContext())
            {
                try
                {
                    var order = (from o in db.Orders
                                 where o.orderNr == nr
                                 select o).FirstOrDefault();
                    if (order == null)//tester om orderen finnes
                        return null;

                    orderModel.orderNr = order.orderNr;
                    orderModel.customerID = order.customerID;

                    var productOrders = (from p in db.ProductOrders
                                  where p.orderNr == nr
                                  select p).ToList();
                    orderModel.total = 0;
                    foreach (var o in productOrders)//legger produktene til i order modellen
                    {
                        var productModel = DBProduct.find(o.products.productID);
                        productModel.quantity = o.quantity;
                        orderModel.products.Add(productModel);
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

        public static List<OrderModel> findOrders(int id)//Henter ut en liste med alle ordre for kunde med customerID lik id
        {
            var orderModelList = new List<OrderModel>();
            using (var db = new CustomerContext())
            {
                try
                {
                    var orders = (from o in db.Orders
                                 where o.customerID == id
                                 select o).ToList();
                    foreach (var o in orders)//legger order modellene inn i listen
                        orderModelList.Add(find(o.orderNr));
                    return orderModelList;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                }
            }//end using
            return null;
        }//end findOrders()
    } //end namespace
}//end class