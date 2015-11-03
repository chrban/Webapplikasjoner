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
                Session[Employee] = _EmployeeBLL.find(user.username);
                EmployeeModel Emp = _EmployeeBLL.find(user.username);
                LoggedIn(Emp);
            }
            ModelState.AddModelError("", "Feil brukernavn eller passord");
            return View();
        }
        public ActionResult LoggedIn(EmployeeModel employee)
        {
            if (Session[LOGGED_INN] != null)
            {
                bool loggetInn = (bool)Session[LOGGED_INN];
                if (loggetInn)
                {
                    return View(Employee);
                }
            }
            return RedirectToAction("");
        }
        public ActionResult LoggedOut()
        {
            Session[LOGGED_INN] = null;
            Session[Employee] = null;
            return RedirectToAction("Loginview");
        }
    }
}