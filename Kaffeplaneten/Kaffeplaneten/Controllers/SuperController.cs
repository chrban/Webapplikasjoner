using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;                        // ------- REMOVE AFTER MOVING LOG METHODS -------

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
        public const string LOG_DATABASE = "../../Content/log_database.txt";
        public const string LOG_INTERACTION = "../../Content/log_interaction.txt";
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

        // ----------------------------   EVERYTHING UNDERNEATH THIS LINE WILL BE MOVED TO BLL WHEN COMPLETED       ---------------------------------
        // ------------------------------------------------------------------------------------------------------------------------------------------

        public bool logToDatabase(string message)
        {
            createLog(LOG_DATABASE);                            // Checks for log existence.
            string logLine = "{ " +
                              "'Date': '" + DateTime.Now.ToString("h:mm:ss tt") + "'," +
                              "'Action': '" + message + "'," +
                          " }";
            try
            {
                using (TextWriter logWriter = new StreamWriter(LOG_DATABASE))
                {
                    logWriter.WriteLine(logLine);
                    logWriter.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("ERROR: COULD NOT LOG DATABASE ACTION.");
                return false;
            }
        }

        /*public bool logToUser(Persons employee, string message)
        {
            createLog(LOG_INTERACTION);
            string logLine = "{ " +
                              "'User': '" + employee.firstName + " " + employee.lastName + "'," +
                              "'Date': '" + DateTime.Now.ToString("h:mm:ss tt") + "'," +
                              "'Action': '" + message + "'," +
                          " }";
            try
            {
                using (TextWriter logWriter = new StreamWriter(LOG_INTERACTION))
                {
                    logWriter.WriteLine(logLine);
                    logWriter.Close();
                    return true;
                }
    }
            catch (Exception e)
            {
                System.Console.WriteLine("ERROR: COULD NOT LOG ACTION TO USER.");
                return false;
            }
        }*/

        // WILL BE MOVED TO BLL WHEN COMPLETED
        public void createLog(string type)
        {
            if (type.Equals(LOG_DATABASE))
            {
                if (!System.IO.File.Exists(LOG_DATABASE))
                {
                    System.IO.File.Create(LOG_DATABASE).Dispose();
                    using (TextWriter logWriter = new StreamWriter(LOG_DATABASE))
                    {
                        logWriter.WriteLine("Log file created: " + DateTime.Now.ToString("h:mm:ss tt"));
                        logWriter.Close();
                    }
                }
            }
            else if (type.Equals(LOG_INTERACTION))
            {
                if (!System.IO.File.Exists(LOG_INTERACTION))
                {
                    System.IO.File.Create(LOG_INTERACTION).Dispose();
                    using (TextWriter logWriter = new StreamWriter(LOG_INTERACTION))
                    {
                        logWriter.WriteLine("Log file created: " + DateTime.Now.ToString("h:mm:ss tt"));
                        logWriter.Close();
                    }
                }
            }
        }

        // ----------------------------     END OF MOVABLE METHODS       ---------------------------------

    }
}   