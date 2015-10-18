using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Kaffeplaneten.Controllers
{
    public class SecurityController : SuperController
    {
        // GET: Security
        
        public ActionResult Loginview()
        {
            if (Session[LOGGED_INN] == null)
            {
                Session[LOGGED_INN] = false;
                ViewBag.LoggedOn = false;
            }
            else
            {
                ViewBag.LoggedOn = (bool)Session[LOGGED_INN];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Loginview(UserModel user)
        {
            user.passwordHash = base.getHash(user.password);
            if (DBUser.verifyUser(user))
            {
                Session[LOGGED_INN] = true;
                Session[CUSTOMER_ID] = DBUser.get(user.username).customerID;
                ViewBag.LoggedOn = true;
                //Session["User"] = user.username;
                return View(user);
            }
            ModelState.AddModelError("", "Feil brukernavn eller passord");
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
            Session[CUSTOMER_ID] = -1;
            return RedirectToAction("Loginview");
        }
    }
}