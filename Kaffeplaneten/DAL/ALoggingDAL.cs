using System.Collections.Generic;
using Kaffeplaneten.Models;
using Newtonsoft.Json.Linq;
using System;

namespace Kaffeplaneten.DAL
{
    public abstract class ALoggingDAL
    {
        public string LOG_DATABASE = AppDomain.CurrentDomain.BaseDirectory + "..\\log_database.txt";
        public string LOG_INTERACTION = AppDomain.CurrentDomain.BaseDirectory + "..\\log_interaction.txt";
        public abstract bool createLog(string type);
        public abstract JObject findInDatabaseLog(string criteria);
        public abstract JObject findInInteractionLog(string criteria);
        public abstract bool logToDatabase(string message);
        public abstract bool logToUser(string message, EmployeeModel model);
        public abstract bool logToUser(string message, CustomerModel model);
        public abstract void outputLogToConsole();
        public abstract List<JObject> parseLogFile(string log);
        public abstract JArray parseToArray(string log);
    }
}