using Kaffeplaneten.BLL;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administrasjon.Controllers
{
    public class AdminOrderController : SuperController
    {

        private OrderBLL _orderBLL;
        private LoggingBLL _loggingBLL;

        public AdminOrderController()
        {
            _orderBLL = new OrderBLL();
            _loggingBLL = new LoggingBLL();
        }
        public AdminOrderController(OrderBLL orderBLL, LoggingBLL loggingBLL)
        {
            _orderBLL = orderBLL;
            _loggingBLL = loggingBLL;
        }
        public ActionResult CustomerOrders(int id = 0)
        {
            var orders = _orderBLL.findOrders(id);
            return View(orders);
        }
        public ActionResult cancelOrder(int nr)
        {

            if (_orderBLL.cancelOrder(nr))
            {
                _loggingBLL.logToDatabase("Slettet ordre: " + nr);
                _loggingBLL.logToUser("Slettet ordre: " + nr, (EmployeeModel)Session["Employee"]);
                return RedirectToAction("AllOrders");
            }
            return RedirectToAction("AllOrders");
        }

        public ActionResult findOrders(int id)
        {
            var orders = _orderBLL.findOrders(id);
            if (orders != null)
                return View("HelpView", orders);

            return RedirectToAction("AllCustomers","AdminCustomer");
        }

    }
}