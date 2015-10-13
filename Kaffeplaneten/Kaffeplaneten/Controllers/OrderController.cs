using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaffeplaneten.Controllers
{
    public class OrderController : Controller
    {
        // GET: Ordre
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderView()
        {
            return View();
        }
    }
}