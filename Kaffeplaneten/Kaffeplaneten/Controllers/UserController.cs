using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Kaffeplaneten.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Loginview()
        {
            return View();
        }

        public ActionResult createUser()
        {

            return View();
        }
        [HttpPost]
        public ActionResult createUser(FormCollection incList)
        {
            try
            {
                using (var db = new Models.CustomerContext())
                {
                    var newUser = new Models.Customers();
                    newUser.firstName = incList["firstname"];
                    newUser.lastName = incList["surname"];
                    newUser.email = incList["email"];
                    newUser.phone = incList["cellphone"];
                    newUser.adress = incList["adress"];
                    newUser.payAdress = incList["payAdress"];

                    string zipcode = incList["zipcode"];

                    var findProvince = db.Provinces.FirstOrDefault(z => z.zipCode == zipcode);

                    if(findProvince == null )
                    {
                        var newProvince = new Models.Provinces();
                        newProvince.zipCode = incList["zipcode"];
                        newProvince.province = incList["province"];
                        db.Provinces.Add(newProvince);

                        newUser.provinces = newProvince;
                    }
                    else
                    {   
                        newUser.provinces = findProvince;
                    }
                    newUser.zipCode = zipcode;

                    //Samme koden igjen for betalingsadressen
                    string PayZipcode = incList["zipcode"];

                    var findPayProvince = db.Provinces.FirstOrDefault(z => z.zipCode == zipcode);

                    if (findPayProvince == null)
                    {
                        var newProvince = new Models.Provinces();
                        newProvince.zipCode = incList["zipcode"];
                        newProvince.province = incList["province"];
                        db.Provinces.Add(newProvince);

                        newUser.payProvince = newProvince.province;
                    }
                    else
                    {
                        newUser.payProvince = findPayProvince.province;
                    }
                    newUser.payZipCode = PayZipcode;
                    db.Customers.Add(newUser);
                    db.SaveChanges();
                    Debug.WriteLine("test1");
                    return RedirectToAction("LoginView");

                }
            }

            catch(Exception feil)
            {
                Debug.WriteLine("TEST2!!!!");
                return View();
            }

        }
    }
}