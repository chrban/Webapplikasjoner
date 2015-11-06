using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administrasjon.Controllers
{
    public class SuperController : Controller
    {
        public const string employeeAdmin = "employeeAdmin";
        public const string customerAdmin = "customerAdmin";
        public const string orderAdmin = "orderAdmin";
        public const string productAdmin = "productAdmin";
        public const string databaseAdmin = "databaseAdmin";
        public const string firstname = "firstname";
        public const string lastname = "lastname";
        public const string username = "username";
        public const string Feilmelding = "Feilmelding";
        public const string LOGGED_INN = "LoggedInn";
        public const string Employee = "Employee";   
        public ActionResult Index()
        {
            return View();
        }

        public int getActiveUserID()//Returnerer customerID til inlogget customer. -1 hvis inger er innlogget
        {
            if (Session[Employee] == null)
                return -1;
            return ((EmployeeModel)Session[Employee]).employeeID;
        }
        private static byte[] createHash(string incPassword)
        {
            var algorithm = System.Security.Cryptography.SHA512.Create();
            byte[] incData, outData = null;
            if (incPassword != null)
            {
            incData = System.Text.Encoding.ASCII.GetBytes(incPassword);
            outData = algorithm.ComputeHash(incData);
            }
            return outData;
        }

        public byte[] getHash(string incPassword)
        {
            return createHash(incPassword);
        }
    }
}