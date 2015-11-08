using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaffeplaneten.BLL;
using Kaffeplaneten.Models;
using System.Diagnostics;

namespace Administrasjon.Controllers
{
    public class AdminCustomerController : Controller
    {
        private CustomerBLL _customerBLL;
        private LoggingBLL _loggingBLL;

        public AdminCustomerController()
        {
            _customerBLL = new CustomerBLL();
            _loggingBLL = new LoggingBLL();
        }
        public AdminCustomerController(CustomerBLL customerBLL, LoggingBLL loggingBLL)
        {
            _customerBLL = customerBLL;
            _loggingBLL = loggingBLL;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllCustomers()
        {
            var ut = _customerBLL.allCustomers();

            if(ut != null)
            {
                return View(ut);

            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var customerModel = _customerBLL.find(id);
            if (customerModel != null)
            {
                Session["tempCID"] = id;
                return View(customerModel);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(CustomerModel customerModel)
        {
            customerModel.customerID = (int)Session["tempCID"];

            if (_customerBLL.update(customerModel))
            {
                _loggingBLL.logToUser("Oppdaterte bruker: " + customerModel.email + " (BrukerID: " + customerModel.customerID + ")", (EmployeeModel)Session["Employee"]);
                _loggingBLL.logToDatabase("Bruker oppdatert: " + customerModel.email + " (BrukerID: " + customerModel.customerID + ")");
                return RedirectToAction("AllCustomers");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {

            if (_customerBLL.delete(id))
            {
                return RedirectToAction("AllCustomers");
            }
            else
            {
                return View();
            }

        }
        public ActionResult Details(int id)
        {
            var customerModel = _customerBLL.find(id);
            if (customerModel != null)
            {
                Session["tempCID"] = id;
                return View(customerModel);
            }

            return RedirectToAction("AllCustomers");

        }

      
    }
}