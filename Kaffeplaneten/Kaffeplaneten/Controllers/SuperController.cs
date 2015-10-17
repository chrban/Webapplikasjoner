using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaffeplaneten.Controllers
{
    public class SuperController : Controller
    {
        // GET: Super

            //Controller som tar hånd om metoder som skal benyttes på tvers av controllerne
        public ActionResult Index()
        {
            return View();
        }
        public int getActiveUserID()
        {
            if (Session["CustomerID"] == null)
                return -1;
            return (int)Session["CustomerID"];
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