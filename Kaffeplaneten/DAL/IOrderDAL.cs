using System.Collections.Generic;
using Kaffeplaneten.Models;

namespace Kaffeplaneten.DAL
{
    public interface IOrderDAL
    {
        bool add(OrderModel orderModel);
        OrderModel find(int nr);
        List<OrderModel> findOrders(int id);
        List<OrderModel> allOrders();
        bool cancelOrder(int nr);
    }
}