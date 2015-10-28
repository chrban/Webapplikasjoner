using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/*
Superkontroller alle andre kontrollere arver. Har metoder som er felles for alle kontrollerene
*/
namespace Kaffeplaneten.Controllers
{
    public class SuperController : Controller
    {
        //const variabler for Session
        public const string SHOPPING_CART = "ShoppingCart";
        public const string LOGGED_INN = "LoggedInn";
        public const string PRODUCT_LIST = "ProductList";
        public const string UNIQUE_CATEGORIES = "UniqueCategories";
        public const string INITIAL_LOAD = "INITIAL";
        public const string CUSTOMER = "Customer";

        //Controller som tar hånd om metoder som skal benyttes på tvers av controllerne
        public ActionResult Index()
        {
            return View();
        }
        public int getActiveUserID()//Returnerer customerID til inlogget customer. -1 hvis inger er innlogget
        {
            if (Session[CUSTOMER] == null)
                return -1;
            return ((CustomerModel)Session[CUSTOMER]).customerID;
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