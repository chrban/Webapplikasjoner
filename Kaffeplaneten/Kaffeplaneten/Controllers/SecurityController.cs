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
            if (Session["LoggedIn"] == null)
            {
                Session["LoggedIn"] = false;
                ViewBag.LoggedOn = false;
            }
            else
            {
                ViewBag.LoggedOn = (bool)Session["LoggedIn"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Loginview(UserModel user)
        {
            user.passwordHash = base.getHash(user.password);
            if (DBUser.varifyUser(user))
            {
                Debug.WriteLine("Test - Fant kunde");
                Session["LoggedIn"] = true;
                Session["CustomerID"] = DBUser.get(user.username).customerID;
                ViewBag.LoggedOn = true;
                return View();
            }
            Debug.WriteLine("Returnerer view!");
            ModelState.AddModelError("", "Feil brukernavn eller passord");
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["LoggedIn"] != null)
            {
                bool loggetInn = (bool)Session["LoggetInn"];
                if (loggetInn)
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult LoggedOut()
        {
            Session["LoggetInn"] = false;
            Session["CustomerID"] = -1;
            return RedirectToAction("index");
        }
    }
}