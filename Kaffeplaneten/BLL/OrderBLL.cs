﻿using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.BLL
{
    
    public class OrderBLL
    {
        private IOrderDAL _orderDAL;
        private LoggingBLL _loggingBLL;
        public OrderBLL(IOrderDAL iOrderDAL)
        {
            _orderDAL = iOrderDAL;
            _loggingBLL = new LoggingBLL();
        }
        public OrderBLL()
        {
            _orderDAL = new OrderDAL();
            _loggingBLL = new LoggingBLL();
        }
        public bool add(OrderModel orderModel)/*Legger Orders og ProductOrders inn i databasen. CustomerID og pruductID-ene må være med i modellen*/
        {
            return _orderDAL.add(orderModel);
        }
        public OrderModel find(int nr)//Henter ut en OrderModel fra en ordre med ordreNr lik nr
        {
            return _orderDAL.find(nr);
        }

        public List<OrderModel> findOrders(int id)//Henter ut en liste med alle ordre for kunde med customerID lik id
        {
            return _orderDAL.findOrders(id);
        }//end findOrders()

        public List<OrderModel> findOrders(CustomerModel customerModel)
        {
            return _orderDAL.findOrders(customerModel);
        }

        public List<OrderModel> allOrders()
        {
            return _orderDAL.allOrders();
        }

        public bool cancelOrder(int nr)
        {
            return _orderDAL.cancelOrder(nr);
        }
    } //end namespace
}//end class