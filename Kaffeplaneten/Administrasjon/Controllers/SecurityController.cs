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

        public SecurityController()
        {
            _userBLL = new UserBLL();
            _EmployeeBLL = new EmployeeBLL();
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

                    if(Session[employeeAdmin] == null)
                        Session[employeeAdmin] = Emp.employeeAdmin;
                    if(Session[customerAdmin]== null)
                        Session[customerAdmin] = Emp.customerAdmin;
                    if (Session[orderAdmin] == null)
                        Session[orderAdmin] = Emp.orderAdmin;
                    if (Session[productAdmin] == null)
                        Session[productAdmin] = Emp.productAdmin;
                    if (Session[databaseAdmin] == null)
                        Session[databaseAdmin] = Emp.databaseAdmin;

                    Session[firstname] = Emp.firstName;
                    Session[lastname] = Emp.lastName;
                    Session[username] = user.username;
                    return RedirectToAction("Home", "Layout");
                }
                Session[Feilmelding] = "Finner ikke brukerepost";
                return View();
            }
            Session[Feilmelding] = "Feil i brukernavn eller passord";
            return View();
       
        }
        public ActionResult LoggedIn()
        {
            if (Session[LOGGED_INN] != null)
            {
                bool loggetInn = (bool)Session[LOGGED_INN];
                if (loggetInn)
                {
                    return View();
                }
            }
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
                return "NF";
            }
            else
            {
                _userBLL.resetPassword(user, base.getHash(tempPW));

                return tempPW;
            }

        }

    }
}