using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administrasjon.Controllers
{
    public class LayoutController : SuperController
    {
        public ActionResult HeaderAndMenuBar(UserModel user)
        {
            if (Session[LOGGED_INN] != null)
            {
                if ((bool)Session[LOGGED_INN])
                {
                    ViewBag.Inlogged = true;
                }
            }

            ViewBag.notInlogged = false;
            return PartialView(user);
        }

        public ActionResult EmployeeAdminBar()
        {
            return PartialView();
        }

        public ActionResult Home(EmployeeModel emp)
        {
            return View(emp);
        }

       
    }

}