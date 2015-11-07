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

        public bool logToUser(string action, CustomerModel model) {
            return _loggingDAL.logToUser(action, model);
        }
        public bool logToUser(string action, EmployeeModel model)
        {
            return _loggingDAL.logToUser(action, model);
        }

        public bool logToDatabase(string action)
        {
            return _loggingDAL.logToDatabase(action);
        }

        public JArray getInteractionMessages()
        {
            createLog(_loggingDAL.LOG_INTERACTION);         // SAFETYNET
            return _loggingDAL.parseToArray(_loggingDAL.LOG_INTERACTION);
        }

        public JArray getDatabaseMessages()
        {
            createLog(_loggingDAL.LOG_DATABASE);        // SAFETYNET
            return _loggingDAL.parseToArray(_loggingDAL.LOG_DATABASE);
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
