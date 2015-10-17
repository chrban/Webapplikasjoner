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
            if (DBUser.verifyUser(user))
            {
                Session["LoggedIn"] = true;
                Session["CustomerID"] = DBUser.get(user.username).customerID;
                ViewBag.LoggedOn = true;
                //Session["User"] = user.username;
                return View(user);
            }
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
            Session["LoggedIn"] = null;
            Session["CustomerID"] = -1;
            return RedirectToAction("Loginview");
        }
    }
}