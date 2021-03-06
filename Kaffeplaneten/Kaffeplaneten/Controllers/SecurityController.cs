﻿using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Kaffeplaneten.BLL;

namespace Kaffeplaneten.Controllers
{
    public class SecurityController : SuperController
    {
        private UserBLL _userBLL;
        private CustomerBLL _customerBLL;
        private LoggingBLL _LoggingBLL;

        public SecurityController()
        {
            _userBLL = new UserBLL();
            _customerBLL = new CustomerBLL();
            _LoggingBLL = new LoggingBLL();
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
                Session[CUSTOMER] = _customerBLL.find(user.username);
                _LoggingBLL.logToUser("Logget inn i systemet.", (CustomerModel)Session[CUSTOMER]);
                return RedirectToAction("AllProducts", "Product", user.username);

            }
            ModelState.AddModelError("", "Feil brukernavn eller passord");
            CustomerModel nothing = null;
            _LoggingBLL.logToUser("Prøvde å logge seg inn på systemet med feil brukernavn/passord.", nothing);
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
            return RedirectToAction("Index");
        }
        public ActionResult LoggedOut()
        {
            Session[LOGGED_INN] = null;
            Session[CUSTOMER] = null;
            return RedirectToAction("Loginview");
        }
        public string ForgotPassword(string email)
        {
            var userModel = _userBLL.get(email);
            if (userModel == null)
                return "NF"; //bruker ikke funnet 

            string tempPW = _userBLL.randomPassord();
            userModel.passwordHash = getHash(tempPW);
            if (_userBLL.update(userModel)) // lykkes i lage nytt pw
            {
                _userBLL.sendMail(userModel.username, userModel.ID.ToString(), "Glemt passord", "Logg inn med midlertidig passord: " + tempPW + "  -Hilsen KaffePlaneten");
                return tempPW;
            }
            return "NF"; //bruker ikke funnet 
        }
    }
}