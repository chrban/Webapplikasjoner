using Kaffeplaneten.BLL;
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

        public AdminOrderController()
        {
            _orderBLL = new OrderBLL();
        }

        public ActionResult AllOrders()
        {
            //egentlig helt dust metode om ikke den knytter ordre til kunde..
            var orders = _orderBLL.allOrders();
            if(orders!=null)
            return View(orders);

            return View();
        }
    }
}