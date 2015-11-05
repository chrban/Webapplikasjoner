﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;

namespace Stubs
{
    class OrderDALStup : IOrderDAL
    {
        public bool add(OrderModel orderModel)
        {
            if (orderModel.customerID > 0)
                return true;
            return false;
        }
        public List<OrderModel> allOrders()
        {
            var orders = new List<OrderModel>();
            orders.Add(find(1));
            orders.Add(find(1));
            orders.Add(find(1));
            orders.Add(find(1));
            return orders;
        }

        public bool cancelOrder(int id)
        {
            if (id > 0)
                return true;
            return false;
        }

        public OrderModel find(int nr)
        {
            var orderModel = new OrderModel();
            orderModel.customerID = 1;
            orderModel.orderNr = 1;
            orderModel.total = 100;
            return orderModel;
        }

        public List<OrderModel> findOrders(int id)
        {
            var list = new List<OrderModel>();
            list.Add(find(1));
            list.Add(find(1));
            list.Add(find(1));
            list.Add(find(1));
            return list;
        }
    }
}
