using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaffeplaneten.Controllers
{
    public class LayoutController : SuperController
    {


       public ActionResult HeaderAndMenuBar()
       {
             if(Session[LOGGED_INN] != null)
             {
                 if ((bool)Session[LOGGED_INN])
                 {
                     ViewBag.Inlogged = true;
                 }
            }

            ViewBag.notInlogged = false;
            return PartialView();
       }
    }
}