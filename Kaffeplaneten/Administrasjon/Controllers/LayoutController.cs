using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administrasjon.Controllers
{
    public class LayoutController : Controller
    {
        // GET: Layout
        public ActionResult HeaderAndMenuBar()
        {
            return View();
        }

        public ActionResult AdministratorBar()
        {
            return View();
        }
    }

}