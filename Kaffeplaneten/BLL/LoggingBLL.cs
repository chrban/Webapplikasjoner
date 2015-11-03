using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Web;

namespace Kaffeplaneten.BLL
{
    public class LoggingBLL
    {
        private LoggingDAL _loggingDAL;

        public LoggingBLL()
        {
            _loggingDAL = new LoggingDAL();
        }

        public bool logToUser(string action) {
            CustomerModel user;
            if (HttpContext.Current.Session["LoggedInn"] == null || (bool)HttpContext.Current.Session["LoggedInn"] == false)
            {
                user = new CustomerModel()
                {
                    customerID = 0,
                    firstName = "Anonymous",
                    lastName = "",
                    email = "Anonymous"
                };
            }
            else{
                user = (CustomerModel)HttpContext.Current.Session["Customer"];
            }     
            return _loggingDAL.logToUser(user, action);
        }
        public bool logToDatabase(string action)
        {
            return _loggingDAL.logToDatabase(action);
        }

        // Find messages with certain criteria.
        // Needs to be done.
        public JObject findInDatabaseLog(string criteria)
        {
            return _loggingDAL.findInDatabaseLog(criteria);
        }
        public JObject findInInteractionLog(string criteria)
        {
            return _loggingDAL.findInInteractionLog(criteria);
        }

        public bool createLog(string type)
        {
            return _loggingDAL.createLog(type);
        }

        public void outputLog()
        {
            _loggingDAL.outputLogToConsole();
        }
    }
}
