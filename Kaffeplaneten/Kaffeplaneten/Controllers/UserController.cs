﻿using Kaffeplaneten.Models;
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
            if (ModelState.IsValid)
            {

                Debug.WriteLine("Test1");
                using (var db = new CustomerContext())
                {
                    var checkUser = (from c in db.Customers
                                     where c.email == newCustomer.email
                                     select c).FirstOrDefault();
                    if (checkUser == null)
                    {
                        var customerDB = new DBCustomer();
                        var Customerobject = customerDB.add(newCustomer, db);

                        if (Customerobject != null)
                        {
                            Debug.WriteLine("Test2");
                            byte[] passwordDB = base.getHash(newCustomer.password);
                            var userDB = new DBuser();

                            var insertOK = userDB.add(passwordDB, Customerobject, db);

                            if (insertOK)
                            {
                                db.SaveChanges();
                                return RedirectToAction("Loginview", "Security", new { area = "" });
                            }

                        }
                    }
                    ModelState.AddModelError("", "Eposten du prøver å registrere finnes allerede. Vennligst benytt en annen adresse");
                    return View(newCustomer);
                }
            }
            return View();
        }


        public ActionResult accountView()
        {
            //test
            var customer = DBCustomer.find(1);
            //var customer = Session["user"];
            //slutt test
            return View(customer);
        }




        public ActionResult editAccountView()
        {
            var customerModel = DBCustomer.find(1);
            return View(customerModel);

        }

        [HttpPost]
        public ActionResult editAccountView(CustomerModel customerModel)
        {
            //customerModel.customerID = Session[< signed in user >].customerID;
            customerModel.customerID = 1;

            var userModel = DBuser.get(customerModel.customerID);
            if (!(customerModel.password == null))
                userModel.passwordHash = base.getHash(customerModel.password);

            DBuser.update(userModel);
            DBCustomer.update(customerModel);
            return RedirectToAction("accountView");

        }

        public ActionResult orderHistoryView()
        {
            //test
            var order = DBOrder.find(1);
            //var customer = Session["user"];
            //slutt test
            return View(order);

        }

    }
}
    