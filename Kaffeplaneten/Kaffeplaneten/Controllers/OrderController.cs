using System.Web.Mvc;
using Kaffeplaneten;
using Kaffeplaneten.Models;
using System.Diagnostics;
using System.Collections.Generic;
using Kaffeplaneten.BLL;

namespace Kaffeplaneten.Controllers
{
    public class OrderController : SuperController
    {

        private OrderBLL _orderBLL;
        private LoggingBLL _loggingBLL;

        public OrderController()
        {
            _orderBLL = new OrderBLL();
            _loggingBLL = new LoggingBLL();
        }
        public ActionResult confirmOrderView()
        {
            var orderModel = (OrderModel)Session[SHOPPING_CART];
            if (orderModel == null)
                return RedirectToAction("AllProducts", "Product", new { area = "" });
            foreach(var p in orderModel.products)
                if(p.quantity>p.stock)
                {
                    ModelState.AddModelError("", "Ikke nok " + p.productName + " på lager. Antall på lager er " + p.stock+"." );
                    return RedirectToAction("ShoppingCartView", "ShoppingCart", new { area = "" });
                }
            if (orderModel.products.Count == 0)
            {
                ModelState.AddModelError("", "Handlevognen er tom");
                return RedirectToAction("ShoppingCartView", "ShoppingCart", new { area = "" });
            }
            var customerModel = (CustomerModel)Session[CUSTOMER];
            if (customerModel == null)
            {
                ModelState.AddModelError("", "Du må være logget inn for å fortsette");
                return RedirectToAction("Loginview", "Security", new { area = "" });
            }
            ViewBag.customerModel = customerModel;
            orderModel.customerID = customerModel.customerID;
            return View(orderModel);
        }
        public ActionResult orderHistoryView()
        {
            var order = _orderBLL.findOrders(getActiveUserID());
            if(order == null)
                return RedirectToAction("Loginview", "Security", new { area = "" });
            return View(order);
        }
        public ActionResult receiptView()
        {
            var orderModel = (OrderModel)Session[SHOPPING_CART];
            if (orderModel == null)
            {
                ModelState.AddModelError("", "Orderen er allerede registrert");
                _loggingBLL.logToUser("Prøvde å legge til ny ordre som allerede var registrert.", (CustomerModel)Session[CUSTOMER]);
                return View(new OrderModel());
            }
            if(!saveOrder(orderModel))
            {
                ModelState.AddModelError("", "Feil ved registrering av data");
                _loggingBLL.logToDatabase("Det var en feil ved registrerting av data fra en ordre: " + orderModel.orderNr);
                return View(new OrderModel());
            }
            return View(orderModel);
        }
        public bool saveOrder(OrderModel orderModel)
        {
            if (orderModel == null)
                return false;
            Session[SHOPPING_CART] = null;
            _loggingBLL.logToDatabase("Kunde: " + orderModel.customerID + " bestilte ny ordre som ble lagt til i databasen.");
            _loggingBLL.logToUser("Bestilte en ny ordre.", (CustomerModel)Session[CUSTOMER]);
            return _orderBLL.add(orderModel);
        }
    }
}