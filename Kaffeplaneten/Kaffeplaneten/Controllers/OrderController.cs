using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaffeplaneten;

namespace Kaffeplaneten.Controllers
{
    public class OrderController : SuperController
    {
        // GET: Order
        public ActionResult ShoppingCartView()
        {
            return View(getShoppingCart());
        }

        public ShoppingCart getShoppingCart()
        {
            if (Session["ShoppingCart"] == null)
            {
                Session["ShoppingCart"] = new ShoppingCart();
                ((ShoppingCart)Session["ShoppingCart"]).createShoppingCart();
            }
            return Session["ShoppingCart"] as ShoppingCart;
        }

        public ActionResult testView() {
            return View();
        }

        public ActionResult OrderView()
        {
            return View();
        }
    }
}