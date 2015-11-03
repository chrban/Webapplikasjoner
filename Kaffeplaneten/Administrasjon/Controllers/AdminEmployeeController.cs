using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaffeplaneten.BLL;
using Kaffeplaneten.Models;

namespace Administrasjon.Controllers
{

    public class AdminEmployeeControllerr : Controller
    {
        private EmployeeBLL _employeeBLL;

        public AdminEmployeeControllerr()
        {
            _employeeBLL = new EmployeeBLL();
        }


        public ActionResult EmployeeEditorView()
        {
            return View();
        }
    }
}