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
                }

                ModelState.AddModelError("", "Eposten du prøver å registrere finnes allerede. Vennligst benytt en annen adresse");
                return View(newCustomer);


            }
            return View();
          
         }

        }
    }
    