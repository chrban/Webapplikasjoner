using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;
using Newtonsoft.Json.Linq;

namespace Kaffeplaneten.Stubs
{
    public class LoggingDALStub : ALoggingDAL
    {
        public override bool createLog(string type)
        {
            if (type == "")
                return false;
            return true;
        }

        public override JObject findInDatabaseLog(string criteria)
        {
            if (criteria == "")
                return null;
            return new JObject("Melding");
        }

        public override JObject findInInteractionLog(string criteria)
        {
            if (criteria == "")
                return null;
            return new JObject("Melding");
        }

        public override bool logToDatabase(Exception ex)
        {
            if (ex == null)
                return false;
            return true;
        }

        public override bool logToDatabase(string message)
        {
            if (message == "")
                return false;
            return true;
        }

        public override bool logToUser(string message, CustomerModel model)
        {
            if (message == "")
                return false;
            return true;
        }

        public override bool logToUser(string message, EmployeeModel model)
        {
            if (message == "")
                return false;
            return true;
        }

        public override void outputLogToConsole()
        {
            
        }

        public override List<JObject> parseLogFile(string log)
        {
            var list = new List<JObject>();
            var jo = new JObject("LogData");
            list.Add(jo);
            list.Add(jo);
            list.Add(jo);
            list.Add(jo);
            return list;
        }

        public override JArray parseToArray(string log)
        {
            var array = new JArray();
            array.Add("LogData");
            return array;
        }
    }
}
