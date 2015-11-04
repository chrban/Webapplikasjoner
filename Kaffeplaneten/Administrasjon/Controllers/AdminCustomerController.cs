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

        public AdminCustomerController()
        {
            _customerBLL = new CustomerBLL();
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllCustomers()
        {
            var ut = _customerBLL.allCustomers();
            Debug.WriteLine("i controller");

            if(ut != null)
            {
                Debug.WriteLine("i RIKTIG if controller");
                return View(ut);

            }
            Debug.WriteLine("Feiler i IF i controller");
            return View();
        }

        public ActionResult Edit(int id)
        {

            var customerList = _customerBLL.allCustomers();
            foreach(var i in customerList)
            {
                if (i.customerID == id)
                {
                    Session["tempCID"] = id;
                    return View(i);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(CustomerModel customerModel)
        {
            customerModel.customerID = (Int32)Session["tempCID"];

            if (_customerBLL.update(customerModel))
            {
                return RedirectToAction("AllCustomers");
            }
            else
            {
                Debug.WriteLine("KUNDEEDIT FEILER");
                return View();
            }
        }

      
    }
}