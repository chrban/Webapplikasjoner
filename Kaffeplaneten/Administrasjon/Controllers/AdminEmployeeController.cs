using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaffeplaneten.BLL;
using Kaffeplaneten.Models;
using System.Diagnostics;

namespace Administrasjon.Controllers
{

    public class AdminEmployeeController : SuperController
    {
        private EmployeeBLL _employeeBLL;
        private UserBLL _userBLL;

        public AdminEmployeeController()
        {
            _employeeBLL = new EmployeeBLL();
            _userBLL = new UserBLL();
        }
        public ActionResult AllEmployees()
        {
            var EmployeeList = _employeeBLL.getAllEmployees();

            if (EmployeeList != null)
            {
                return View(EmployeeList);

            }
            return View();
        }

        public ActionResult createEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createEmployee(EmployeeModel employee)
        {   
            if(!ModelState.IsValid)
                return View();
            string username = employee.username + "@kaffeplaneten.no";
            var employeeModel = _userBLL.get(username);
            if(employeeModel != null)
            {
                Session["userExists"] = "Brukernavn(Epost) du prøver å registere finnes allerede!";
                return View(employee);
            }
            var personExist = _employeeBLL.find(username);
            if(personExist != null)
            {
                Session["employeeExists"] = "Ansattbrukeren eksisterer allerede!";
                return View(employee);
            }
            if(!_employeeBLL.add(employee))
            {
                Session["employeeError"] = "Feil ved registrering av ansatt";
                return View(employee);
            }
            employeeModel = new UserModel();
            employeeModel.username = username;
            employeeModel.passwordHash = base.getHash(employee.password);
            employeeModel.ID = employee.employeeID;

            if(!_userBLL.add(employeeModel)) //registrerer ny bruker
            {
                Session["userError"] = "Feil ved registrering av bruker";
                return View(employee);
            }
            return RedirectToAction("AllEmployees", "AdminEmployee");
        }
    }
}