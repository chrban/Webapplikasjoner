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
            if (findCustomer(user))
            {
                Debug.WriteLine("Test - Fant kunde");
                Session["LoggedIn"] = true;
                ViewBag.LoggedOn = true;
                
            }
            Debug.WriteLine("Returnerer view!");
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
            return RedirectToAction("index");
        }



        private  bool findCustomer(UserModel incUser)
        {

                using (var db = new CustomerContext())
                {
                    byte[] PasswordDB = base.getHash(incUser.password);
                    Users foundUser = db.Users.SingleOrDefault(
                    u => ((u.password == PasswordDB) && (u.email == incUser.username)));

                    if (foundUser == null)
                    {
                        return false;
                    }
                    return true;
                
                }

        }



    }
}