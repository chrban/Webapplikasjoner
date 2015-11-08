using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Kaffeplaneten.BLL;
using Administrasjon.Controllers;

namespace Administrasjon.Controllers
{
    public class SecurityController : SuperController
    {
        private UserBLL _userBLL;
        private EmployeeBLL _EmployeeBLL;
        private LoggingBLL _loggingBLL;

        public SecurityController()
        {
            _userBLL = new UserBLL();
            _EmployeeBLL = new EmployeeBLL();
            _loggingBLL = new LoggingBLL();
        }
        public SecurityController(EmployeeBLL employeeBLL, UserBLL userBLL, LoggingBLL loggingBLL)
        {
            _EmployeeBLL = employeeBLL;
            _userBLL = userBLL;
            _loggingBLL = loggingBLL;
        }
        public ActionResult Loginview()
        {
            if (Session[LOGGED_INN] == null)
            {
                Session[LOGGED_INN] = false;
                ViewBag.LoggedOn = false;
            }
            else
            {
                ViewBag.LoggedOn = Session[LOGGED_INN];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Loginview(UserModel user)
        {
            user.passwordHash = base.getHash(user.password);
            if (_userBLL.verifyUser(user))
            {
                Session[LOGGED_INN] = true;
                ViewBag.LoggedOn = true;
                EmployeeModel Emp = _EmployeeBLL.find(user.username);
                if(Emp != null)
                {
                    Session[Employee] = Emp;
                        Session[employeeAdmin] = Emp.employeeAdmin;
                        Session[customerAdmin] = Emp.customerAdmin;
                        Session[orderAdmin] = Emp.orderAdmin;
                        Session[productAdmin] = Emp.productAdmin;
                        Session[databaseAdmin] = Emp.databaseAdmin;

                    Session[firstname] = Emp.firstName;
                    Session[lastname] = Emp.lastName;
                    Session[username] = user.username;
                    _loggingBLL.logToUser("Logget seg på systemet.", (EmployeeModel)Session["Employee"]);
                    return RedirectToAction("Home", "Layout");
                }
                Session[Feilmelding] = "Finner ikke brukerepost";
                return View();
            }
            Session[Feilmelding] = "Feil i brukernavn eller passord";
            _loggingBLL.logToUser("Prøvde å logge seg inn på systemet med feil brukernavn/passord.", (EmployeeModel)null);
            return View();
       
        }
        public ActionResult LoggedIn()
        {
            if (Session[LOGGED_INN] != null && (bool)Session[LOGGED_INN])
                    return View();
            return RedirectToAction("index");
        }
        public ActionResult LoggedOut()
        {
            Session[LOGGED_INN] = null;
            Session[employeeAdmin] = null;
            Session[customerAdmin] = null;
            Session[orderAdmin] = null;
            Session[productAdmin] = null;
            Session[databaseAdmin] = null;
            Session[firstname] = null;
            Session[lastname] = null;
            Session[username] = null;
            return RedirectToAction("Loginview");
        }

        public string ForgotPassword(string email)
        {
            var user = _userBLL.get(email);
            var tempPW = _userBLL.randomPassord();


            if (_userBLL.get(email) == null)
            {
                var hashetPw = getHash(tempPW);
                if (_userBLL.resetPassword(user, hashetPw, false)) // lykkes i lage nytt pw
                {

                    _userBLL.sendMail(user.username, user.ID.ToString(), "Glemt passord", "Logg inn med midlertidig passord: " + tempPW + "  -Hilsen KaffePlaneten");
                    return tempPW;
                }
            }
            return "NF"; //bruker ikke funnet 
        }

    }
}