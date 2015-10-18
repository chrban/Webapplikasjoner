﻿using System.Web.Mvc;
using Kaffeplaneten;
using Kaffeplaneten.Models;
using System.Diagnostics;
using System.Collections.Generic;

namespace Kaffeplaneten.Controllers
{
    public class OrderController : SuperController
    {
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
            var customerModel = DBCustomer.find(getActiveUserID());
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
            var order = DBOrder.findOrders(getActiveUserID());
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
                return View(new OrderModel());
            }
            if(!saveOrder(orderModel))
            {
                ModelState.AddModelError("", "Feil ved registrering av data");
                return View(new OrderModel());
            }
            return View(orderModel);
        }
        public bool saveOrder(OrderModel orderModel)
        {
            if (orderModel == null)
                return false;
            Session[SHOPPING_CART] = null;
            return DBOrder.add(orderModel);
        }
    }
}