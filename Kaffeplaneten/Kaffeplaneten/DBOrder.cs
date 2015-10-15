using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Kaffeplaneten
{
    public class DBOrder
    {
        public static OrderModel find(int nr)
        {
            try
            {
                var orderModel = new OrderModel();
                var db = new CustomerContext();
                var order = (from o in db.Orders
                             where o.orderNr == nr
                             select o).FirstOrDefault();

                orderModel.orderNr = order.orderNr;
                orderModel.customerID = order.customerID;
                var orders = (from p in db.ProductOrders
                                     where p.orderNr == nr
                                     select p).ToList();

                orderModel.total = 0;
                orderModel.products = new List<ProductModel>();
                foreach(var o in orders)
                {
                    orderModel.products.Add(DBProduct.toObject(o.products));
                    orderModel.total += o.price;
                }
                return orderModel;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nSource:\n" + ex.Source);
                Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                //Environment.Exit(1);
            }
            return null;
        }
    }
}