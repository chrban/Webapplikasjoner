using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaffeplaneten.Controllers
{
    public class LayoutController : Controller
    {


       public ActionResult HeaderAndMenuBar()
       {
             if(Session["LoggedIn"] != null)
             {
                 if ((bool)Session["LoggedIn"])
                 {
                     ViewBag.Inlogged = true;
                 }
            }

            ViewBag.notInlogged = false;
            return PartialView();
       }
    }
}