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
        private LoggingBLL _loggingBLL;

        public UserController()
        {
            _customerBLL = new CustomerBLL();
            _userBLL = new UserBLL();
            _loggingBLL = new LoggingBLL();
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

            var userModel = _userBLL.get(newCustomer.email);
            if (userModel != null)//tester om en bruker med samme epost finnes fra før
            {
                ModelState.AddModelError("", "Eposten du prøver å registrere finnes allerede. Vennligst benytt en annen adresse");
                _loggingBLL.logToUser("Prøvde å registrere seg med eksisterende epost: " + userModel.username, (CustomerModel)Session[CUSTOMER]);
                return View(newCustomer);
            }

            if (!_customerBLL.add(newCustomer))//registrerer ny customer
            {
                ModelState.AddModelError("", "Feil ved registrering av bruker");
                _loggingBLL.logToUser("Fikk en feil ved registrering av brukernavn: " + newCustomer.email, (CustomerModel)Session[CUSTOMER]);
                return View(newCustomer);
            }

            userModel = new UserModel();
            userModel.username = newCustomer.email;
            userModel.passwordHash = getHash(newCustomer.password);
            userModel.ID = newCustomer.customerID;

            if (!_userBLL.add(userModel))//registrerer ny user
            {
                ModelState.AddModelError("", "Feil ved registrering av bruker");
                _loggingBLL.logToUser("Fikk en feil ved registrering av brukernavn: " + userModel.username, (CustomerModel)Session[CUSTOMER]);
                return View(newCustomer);
            }

            _loggingBLL.logToUser("Opprettet bruker: " + newCustomer.email, (CustomerModel)Session[CUSTOMER]);
            _loggingBLL.logToDatabase("Bruker lagt til i database: " + newCustomer.email);
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
            if (customerModel.password != null)//tester om passord skal endres
            {
                userModel.passwordHash = getHash(customerModel.password);
            }

            if (!_userBLL.update(userModel))//registrerer endinger i user
            {
                ModelState.AddModelError("", "Epost finnes fra før");
                return RedirectToAction("editAccountView");
            }
            if (!_customerBLL.update(customerModel))//registrerer endring i customer
            {
                ModelState.AddModelError("", "Feil ved registrering av data");
                _loggingBLL.logToUser("Fikk en feil ved endring av data på bruker.", (CustomerModel)Session[CUSTOMER]);
                return RedirectToAction("editAccountView");
            }
            _loggingBLL.logToUser("Oppdaterte bruker: " + userModel.username + " (BrukerID: " + userModel.ID + ")", (CustomerModel)Session[CUSTOMER]);
            _loggingBLL.logToDatabase("Bruker: " + userModel.username + " (BrukerID: " + userModel.ID + ") ble oppdatert.");
            return RedirectToAction("accountView");

        }
    }
}

    