using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaffeplaneten.Controllers
{
    public class SuperController : Controller
    {
        public const string CUSTOMER_ID = "CustomerID";
        public const string SHOPPING_CART = "ShoppingCart";
        public const string LOGGED_INN = "LoggedInn";
        public const string PRODUCT_LIST = "ProductList";
        public const string UNIQUE_CATEGORIES = "UniqueCategories";
        public const string USER = "User";

        // GET: Super

        //Controller som tar hånd om metoder som skal benyttes på tvers av controllerne
        public ActionResult Index()
        {
            return View();
        }
        public int getActiveUserID()//Returnerer customerID til inlogget customer. -1 hvis inger er innlogget
        {
            if (Session[CUSTOMER_ID] == null)
                return -1;
            return (int)Session[CUSTOMER_ID];
        }
        private static byte[] createHash(string incPassword)
        {
            var algorithm = System.Security.Cryptography.SHA512.Create();
            byte[] incData, outData;
            incData = System.Text.Encoding.ASCII.GetBytes(incPassword);
            outData = algorithm.ComputeHash(incData);
            return outData;
        }

        public byte[] getHash(string incPassword)
        {
           return createHash(incPassword);
        }
    }
}