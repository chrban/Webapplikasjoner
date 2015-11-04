using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaffeplaneten.BLL;
using Kaffeplaneten.Models;

namespace Administrasjon.Controllers
{

    public class AdminEmployeeController : SuperController
    {
        private EmployeeBLL _employeeBLL;

        public AdminEmployeeController()
        {
            _employeeBLL = new EmployeeBLL();
        }
        public ActionResult AllEmployees()
        {

            return View();
        }
            

        public ActionResult EmployeeEditorView()
        {
            return View();
        }
    }
}