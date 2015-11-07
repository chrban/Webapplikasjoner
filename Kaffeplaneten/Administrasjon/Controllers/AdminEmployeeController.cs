using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaffeplaneten.BLL;
using Kaffeplaneten.Models;
using System.Diagnostics;
using System.Text;

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
        public AdminEmployeeController(EmployeeBLL employeeBLL, UserBLL userBLL)
        {
            _employeeBLL = employeeBLL;
            _userBLL = userBLL;
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
            if (!ModelState.IsValid)
                return View();
            string username = employee.username + "@kaffeplaneten.no";
            var employeeModel = _userBLL.get(username);
            if (employeeModel != null)
            {
                Session["userExists"] = "Brukernavn(Epost) du prøver å registere finnes allerede!";
                return View(employee);
            }
            var personExist = _employeeBLL.find(username);
            if (personExist != null)
            {
                Session["employeeExists"] = "Ansattbrukeren eksisterer allerede!";
                return View(employee);
            }
            if (!_employeeBLL.add(employee))
            {
                Session["employeeError"] = "Feil ved registrering av ansatt";
                return View(employee);
            }
            employeeModel = new UserModel();
            employeeModel.username = username;
            employeeModel.passwordHash = base.getHash(employee.password);
            employeeModel.ID = employee.employeeID;

            if (!_userBLL.add(employeeModel)) //registrerer ny bruker
            {
                Session["userError"] = "Feil ved registrering av bruker";
                return View(employee);
            }
            return RedirectToAction("AllEmployees", "AdminEmployee");
        }
        public ActionResult deleteEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult deleteEmployee(EmployeeModel employee)
        {
            Session["cantDelete"] = null;
            Session["noUser"] = null;
            Session["isDeleted"] = null;
            var user = _employeeBLL.find(employee.employeeID);
            if (user == null) //sjekker om bruker finnes
            {
                Session["noUser"] = "Brukereposten eksisterer ikke!";
                return View();
            }
            if(user.username.Equals(Session["username"])) //Sjekker om det er en selv
            {
                Session["cantDelete"] = "Ikke lov å slette seg selv fra ansatte";
                return View();
            }
            if(user.employeeAdmin && user.customerAdmin && user.productAdmin && user.orderAdmin && user.databaseAdmin)
            {
                Session["cantDelete"] = "Du kan ikke slette en hovedadministrator fra systemet. Personen har alle rettigheter.";
                return View();
            }

            bool deleted = _employeeBLL.delete(user.employeeID);
            if (deleted)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Du har nå fjernet: ");
                sb.Append(user.firstName);
                sb.Append(" ");
                sb.Append(user.lastName);
                sb.Append("\n med Brukernavn: ");
                sb.Append(user.username);
                Session["isDeleted"] = sb.ToString();
                return View();
            }
            Session["cantDelete"] = "Kunne ikke slette brukeren!";
            return View();
        }
    }
}