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
            if (!(userModel == null))//tester om en bruker med samme epost finnes fra før
                        {
                ModelState.AddModelError("", "Eposten du prøver å registrere finnes allerede. Vennligst benytt en annen adresse");
                return View(newCustomer);
            }
            if (!DBCustomer.add(newCustomer))//registrerer ny customer
                            {
                ModelState.AddModelError("", "Feil ved registrering av bruker");
                return View(newCustomer);
                            }
            Debug.WriteLine("Test2");
            userModel = new UserModel();
            userModel.username = newCustomer.email;
            userModel.passwordHash = base.getHash(newCustomer.password);
            userModel.customerID = newCustomer.customerID;

            if (!DBUser.add(userModel))//registrerer ny user
            {
                ModelState.AddModelError("", "Feil ved registrering av bruker");
                    return View(newCustomer);
                }
            return RedirectToAction("Loginview", "Security", new { area = "" });
        }


        public ActionResult accountView()
        {
            if (getActiveUserID() == -1)
                return RedirectToAction("Loginview", "Security", new { area = "" });
            var customer = DBCustomer.find((int)Session[CUSTOMER_ID]);
            return View(customer);
        }

        public ActionResult editAccountView()
        {
            if (getActiveUserID() == -1)
                return RedirectToAction("Loginview", "Security", new { area = "" });
            var customerModel = DBCustomer.find((int)Session[CUSTOMER_ID]);
            return View(customerModel);

        }

        [HttpPost]
        public ActionResult editAccountView(CustomerModel customerModel)
        {
            customerModel.customerID = getActiveUserID();

            var userModel = DBUser.get(customerModel.customerID);//henter ut user modellen
            userModel.username = customerModel.email;
            if (!(customerModel.password == null))//tester om passord skal endres
                userModel.passwordHash = base.getHash(customerModel.password);

            if(!DBUser.update(userModel))//registrerer endinger i user
            {
                ModelState.AddModelError("", "Epost finnes fra før");
                return RedirectToAction("editAccountView");
        }
            if (!DBCustomer.update(customerModel))//registrerer endring i customer
        {
                ModelState.AddModelError("", "Feil ved registrering av data");
                return RedirectToAction("editAccountView");
            }
            return RedirectToAction("accountView");

        }

    }
}
    