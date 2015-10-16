using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Kaffeplaneten.Controllers
{

    public class UserController : SuperController
    {
        //GET : User
        public ActionResult createUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult createUser(CustomerModel newCustomer)
        {
            Debug.WriteLine("Test0");
            if (!ModelState.IsValid)
                return View();
            Debug.WriteLine("Test1");

            var userModel = DBUser.get(newCustomer.email);
            if (!(userModel == null))
            {
                ModelState.AddModelError("", "Eposten du prøver å registrere finnes allerede. Vennligst benytt en annen adresse");
                return View(newCustomer);
            }
            if (DBCustomer.add(newCustomer))
            {
                Debug.WriteLine("Test2");
                userModel = new UserModel();
                userModel.username = newCustomer.email;
                userModel.passwordHash = base.getHash(newCustomer.password);
                userModel.customerID = newCustomer.customerID;

                if (DBUser.add(userModel))
                    return RedirectToAction("Loginview", "Security", new { area = "" });
            }
            return View();
        }


        public ActionResult accountView()
        {
            if (Session["CustomerID"] == null)
                return RedirectToAction("Loginview", "Security", new { area = "" });
            var customer = DBCustomer.find((int)Session["CustomerID"]);
            return View(customer);
        }




        public ActionResult editAccountView()
        {
            if (Session["CustomerID"] == null)
                return RedirectToAction("Loginview", "Security", new { area = "" });
            var customerModel = DBCustomer.find((int)Session["CustomerID"]);
            return View(customerModel);

        }

        [HttpPost]
        public ActionResult editAccountView(CustomerModel customerModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Feil ved registrering av data");
                return RedirectToAction("editAccountView");
            }
            //customerModel.customerID = Session[< signed in user >].customerID;
            customerModel.customerID = getActiveUserID();

            var userModel = DBUser.get(customerModel.customerID);
            userModel.username = customerModel.email;
            if (!(customerModel.password == null))
                userModel.passwordHash = base.getHash(customerModel.password);

            if(!DBUser.update(userModel))
            {
                ModelState.AddModelError("", "Epost finnes fra før");
                return RedirectToAction("editAccountView");
            }
            if (!DBCustomer.update(customerModel))
            {
                ModelState.AddModelError("", "Feil ved registrering av data");
                return RedirectToAction("editAccountView");
            }
            return RedirectToAction("accountView");

        }

    }
}
    