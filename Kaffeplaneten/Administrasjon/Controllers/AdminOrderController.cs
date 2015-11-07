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
    public class AdminOrderController : Controller
    {

        private OrderBLL _orderBLL;
        private LoggingBLL _loggingBLL;

        public AdminOrderController()
        {
            _orderBLL = new OrderBLL();
            _loggingBLL = new LoggingBLL();
        }
        public AdminOrderController(OrderBLL orderBLL)
        {
            _orderBLL = orderBLL;
        }

        public ActionResult AllOrders()
        {
            //egentlig helt dust metode om ikke den knytter ordre til kunde..
            var orders = _orderBLL.allOrders();
            if(orders!=null)
            return View(orders);

            return View();
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