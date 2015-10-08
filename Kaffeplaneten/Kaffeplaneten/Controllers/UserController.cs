using System;
using System.Collections.Generic;
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
                    var newUser = new Models.Customer();
                    newUser.firstName = incList.["firstname"];
                    newUser.lastName = incList.["surname"];
                    newUser.adress = incList.["adress"];
                    newUser.payAdress = incList.["payAdress"];
                    newUser.firstName = incList.["firstname"];
                    newUser.firstName = incList.["firstname"];
                    newUser.firstName = incList.["firstname"];
                }
            }
        }
    }
}