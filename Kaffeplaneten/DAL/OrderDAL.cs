using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.DAL
{
    
    public class OrderDAL : IOrderDAL
    {
        private LoggingDAL _logging;
        public OrderDAL()
        {
            _logging = new LoggingDAL();
        }

        public bool add(OrderModel orderModel)/*Legger Orders og ProductOrders inn i databasen. CustomerID og pruductID-ene må være med i modellen*/
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
                    _logging.logToDatabase(ex);
                }
                return false;
            }
        }
        private bool addProductOrders(OrderModel orderModel)/*Legger ProductOrders inn i databasen. OrderNr og pruductID-ene må være med i modellen*/
            {
            using (var db = new CustomerContext())
                {
                try
                {
                    var order = db.Orders.Find(orderModel.orderNr);
                    if (order == null)//tester om ordren eksisterer
                        return false;
                    var productDAL = new ProductDAL();
                    foreach (var p in orderModel.products)//Opretter ProductOrders fra modellen
                    {
                        p.stock -= p.quantity;
                        productDAL.updateQuantity(p);
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
                    _logging.logToDatabase(ex);
                }
                return false;
            }
        }

        public OrderModel find(int nr)//Henter ut en OrderModel fra en ordre med ordreNr lik nr
        {
            var orderModel = new OrderModel();
            using (var db = new CustomerContext())
            {
                try
                {
                    var order = (from o in db.Orders
                                 where o.orderNr == nr
                                 select o).FirstOrDefault();
                    return createOrderModel(order);
                }
                catch (Exception ex)
                {
                    _logging.logToDatabase(ex);
                }
            }
            return null;
        }

        private OrderModel createOrderModel(Orders order)
        {
            var orderModel = new OrderModel();
            using (var db = new CustomerContext())
            {
                if (order == null)//tester om orderen finnes
                    return null;
                try
                {
                    orderModel.orderNr = order.orderNr;
                    orderModel.customerID = order.personID;

                    var productOrders = (from p in db.ProductOrders
                                         where p.orderNr == order.orderNr
                                         select p).ToList();
                    orderModel.total = 0;
                    var productDAL = new ProductDAL();
                    foreach (var o in productOrders)//legger produktene til i order modellen
                    {
                        var productModel = productDAL.find(o.products.productID);
                        productModel.quantity = o.quantity;
                        orderModel.products.Add(productModel);
                        orderModel.total += o.price;
                    }
                    return orderModel;
                }
                catch (Exception ex)
                {
                    _logging.logToDatabase(ex);
                }
            }
            return null;
        }

        public List<OrderModel> findOrders(int id)//Henter ut en liste med alle ordre for kunde med customerID lik id
        {
            var orderModelList = new List<OrderModel>();
            using (var db = new CustomerContext())
            {
                try
                {
                    var orders = (from o in db.Orders
                                 where o.personID == id
                                 select o).ToList();
                    foreach (var o in orders)//legger order modellene inn i listen
                        orderModelList.Add(find(o.orderNr));
                    return orderModelList;
                }
                catch (Exception ex)
                {
                    _logging.logToDatabase(ex);
                }
            }//end using
            return null;
        }//end findOrders()

        public List<OrderModel> findOrders(CustomerModel customerModel)
        {
            var orderModelList = new List<OrderModel>();
            using(var db = new CustomerContext())
            {
                try
                {
                    var orders = (from o in db.Orders
                                  where o.personID == customerModel.customerID ||
                                  o.Customers.email.Equals(customerModel.email) ||
                                  o.Customers.phone.Equals(customerModel.phone)
                                  select o).OrderBy(o => o.orderNr).ToList();
                    foreach (var o in orders)
                        orderModelList.Add(createOrderModel(o));
                    return orderModelList;
                }
                catch (Exception ex)
                {
                    _logging.logToDatabase(ex);
                }
                return null;
            }
        }

        public List<OrderModel> allOrders()
        {
            var orderList = new List<OrderModel>();
            using (var db = new CustomerContext())
            {
                try
                {
                    var orders = (from o in db.Orders select o).ToList();
                    var orderModel = new OrderModel();

                    foreach (var o in orders) // finner alle ordre
                        orderList.Add(createOrderModel(o));
                    return orderList;
                }
                catch (Exception ex)
                {
                    _logging.logToDatabase(ex);
                }
            }
            return null;
        }

        public bool cancelOrder(int nr)
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var order = db.Orders.Find(nr);
                    if (order == null)
                        return false;
                    var productOrders = (from p in db.ProductOrders
                                         where p.orderNr == nr
                                         select p).ToList();
                    foreach(var po in productOrders)
                    {
                        db.Products.Find(po.productID).stock += po.quantity;
                        db.ProductOrders.Remove(po);
                    }
                    db.Orders.Remove(order);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    _logging.logToDatabase(ex);
                }
                return false;
            }
        }



      


    } //end namespace
}//end class