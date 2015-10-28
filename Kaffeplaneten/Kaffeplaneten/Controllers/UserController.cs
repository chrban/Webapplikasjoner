using Kaffeplaneten.BLL;
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
        private CustomerBLL _customerBLL;
        private UserBLL _userBLL;

        public UserController()
        {
            _customerBLL = new CustomerBLL();
            _userBLL = new UserBLL();
        }
        public ActionResult createUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult createUser(CustomerModel newCustomer)
        {
            if (!ModelState.IsValid)
                return View();

            var customerModel = _userBLL.get(newCustomer.email);
            if (customerModel == null)//tester om en bruker med samme epost finnes fra før
            {
                ModelState.AddModelError("", "Eposten du prøver å registrere finnes allerede. Vennligst benytt en annen adresse");
                return View(newCustomer);
            }
            if (!_customerBLL.add(newCustomer))//registrerer ny customer
            {
                ModelState.AddModelError("", "Feil ved registrering av bruker");
                return View(newCustomer);
            }
            customerModel = new UserModel();
            customerModel.username = newCustomer.email;
            customerModel.passwordHash = base.getHash(newCustomer.password);
            customerModel.customerID = newCustomer.customerID;

            if (!_userBLL.add(customerModel))//registrerer ny user
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
            var customer = _customerBLL.find(getActiveUserID());
            return View(customer);
        }

        public ActionResult editAccountView()
        {
            if (getActiveUserID() == -1)
                return RedirectToAction("Loginview", "Security", new { area = "" });
            var customerModel = _customerBLL.find(getActiveUserID());
            return View(customerModel);

        }

        [HttpPost]
        public ActionResult editAccountView(CustomerModel customerModel)
        {
            customerModel.customerID = getActiveUserID();

            var userModel = _userBLL.get(customerModel.customerID);//henter ut user modellen
            userModel.username = customerModel.email;
            if (!(customerModel.password == null))//tester om passord skal endres
                userModel.passwordHash = base.getHash(customerModel.password);

            if (!_userBLL.update(userModel))//registrerer endinger i user
            {
                ModelState.AddModelError("", "Epost finnes fra før");
                return RedirectToAction("editAccountView");
            }
            if (!_customerBLL.update(customerModel))//registrerer endring i customer
            {
                ModelState.AddModelError("", "Feil ved registrering av data");
                return RedirectToAction("editAccountView");
            }
            return RedirectToAction("accountView");

        }
    }
}

    