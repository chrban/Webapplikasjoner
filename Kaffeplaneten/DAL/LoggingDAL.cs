using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Web;
using System.Net;

namespace Kaffeplaneten.DAL
{
    public class LoggingDAL:ALoggingDAL
    {

        public override bool logToUser(string message, CustomerModel model)
        {
            createLog(LOG_INTERACTION);
            string logLine = "";
            if(model == null)
            {

                string strHostName = System.Net.Dns.GetHostName();

                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

                string ipaddress = ipEntry.AddressList[2].ToString();
                model = new CustomerModel()
                {
                    customerID = 0,
                    firstName = ipaddress,
                    lastName = "",
                    email = "Anonymous"
                };
            }
            logLine = ",{ " +
                        "\"Date\": \"" + DateTime.Now.ToString("h:mm:ss tt") + "\"," +
                        "\"UserID\": \"" + model.customerID + "\"," +
                        "\"User\": \"" + model.firstName + " " + model.lastName + "\"," +
                        "\"Action\": \"" + message + "\" }";
            try
            {
                using (StreamWriter logWriter = File.AppendText(LOG_INTERACTION))
                {
                    logWriter.WriteLine(logLine);
                    logWriter.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                System.Console.WriteLine("ERROR: COULD NOT LOG ACTION TO USER.");
                return false;
            }
        }

        public override bool logToUser(string message, EmployeeModel model)
        {
            createLog(LOG_INTERACTION);
            string logLine = "";
            if (model == null)                                                  // Dersom personen er anonym vil dette skje.
            {
                
                string strHostName = System.Net.Dns.GetHostName();

                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

                string ipaddress = ipEntry.AddressList[2].ToString();
                model = new EmployeeModel()
                {
                    employeeID = 0,
                    firstName = ipaddress,
                    lastName = "",
                    username = "Anonymous (Employee)"
                };
            }
            logLine = ",{ " +
             "\"Date\": \"" + DateTime.Now.ToString("h:mm:ss tt") + "\"," +
             "\"UserID\": \"" + model.username + "\"," +
             "\"User\": \"" + model.firstName + " " + model.lastName + "\"," +
             "\"Action\": \"" + message + "\" }";
            try
            {
                using (StreamWriter logWriter = File.AppendText(LOG_INTERACTION))
                {
                    logWriter.WriteLine(logLine);
                    logWriter.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                System.Console.WriteLine("ERROR: COULD NOT LOG ACTION TO USER.");
                return false;
            }
        }
        public override bool logToDatabase(string message)
        {
            createLog(LOG_DATABASE);                            // Checks for log existence.
            string logLine = ",{ " +
                                  "\"Date\": \"" + DateTime.Now.ToString("h:mm:ss tt") + "\"," +
                                  "\"Action\": \"" + message + "\" }";
            try
            {
                using (StreamWriter logWriter = File.AppendText(LOG_DATABASE))
                {
                    logWriter.WriteLine(logLine);
                    logWriter.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                System.Console.WriteLine("ERROR: COULD NOT LOG DATABASE ACTION.");
                return false;
            }
        }

        public override bool logToDatabase(Exception ex)
        {
            createLog(LOG_DATABASE);                            // Checks for log existence.
            string logLine = ",{ " +
                                  "\"Date\": \"" + DateTime.Now.ToString("h:mm:ss tt") + "\"," +
                                  "\"Action\": \"FEIL: " + ex + "\" }";
            try
            {
                using (StreamWriter logWriter = File.AppendText(LOG_DATABASE))
                {
                    logWriter.WriteLine(logLine);
                    logWriter.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                System.Console.WriteLine("ERROR: COULD NOT LOG DATABASE ACTION.");
                return false;
            }
        }

        // Find messages with certain criteria.
        // Needs to be done.
        public override JObject findInDatabaseLog(string criteria)
        {
            List<JObject> log = parseLogFile(LOG_DATABASE);
            foreach(JObject message in log)
            {
                foreach (JProperty p in message.Properties())
                {
                    if (p.Value.Equals(criteria))
                    {
                        return message;
                    }
                }
            }
            return null;
        }
        public override JObject findInInteractionLog(string criteria)
        {
            List<JObject> log = parseLogFile(LOG_INTERACTION);
            foreach (JObject message in log)
            {
                foreach (JProperty p in message.Properties())
                {
                    if (p.Value.Equals(criteria))
                    {
                        return message;
                    }
                }
            }
            return null;
        }

        public override List<JObject> parseLogFile(string log)
        {
            string entireLog = File.ReadAllText(log) + "]";
            JArray a = JArray.Parse(entireLog);
            List<JObject> allMessages = new List<JObject>();
            foreach (JObject o in a.Children<JObject>())
            {
                allMessages.Add(o);
            }

            return allMessages;
        }

        public override JArray parseToArray(string log)
        {
            string entireLog = File.ReadAllText(log) + "]";
            JArray a = JArray.Parse(entireLog);
            return a;
        }

        public override bool createLog(string type)
        {
            if (type.Equals(LOG_DATABASE))
            {
                if (!File.Exists(LOG_DATABASE))
                {
                    File.Create(LOG_DATABASE).Dispose();
                    using (TextWriter logWriter = new StreamWriter(LOG_DATABASE))
                    {
                        logWriter.WriteLine("[ { " +
                                            "\"Date\": \"" + DateTime.Now.ToString("h:mm:ss tt") + "\"," +
                                            "\"Action\": \"" + "Logging has begun " + "\"" +
                                            " }");
                        logWriter.Close();
                        return true;
                    }
                }
            }
            else if (type.Equals(LOG_INTERACTION))
            {
                if (!File.Exists(LOG_INTERACTION))
                {
                    File.Create(LOG_INTERACTION).Dispose();
                    using (TextWriter logWriter = new StreamWriter(LOG_INTERACTION))
                    {
                        logWriter.WriteLine("[ { " +
                                  "\"Date\": \"" + DateTime.Now.ToString("h:mm:ss tt") + "\"," +
                                  "\"UserID\": \"" + "System" + "\"," +
                                  "\"User\": \"" + "System" + "\"," +
                                  "\"Action\": \"" + "Logging has begun " + "\"" +
                                    " }");
                        logWriter.Close();
                        return true;
                    }
                }
            }
            return false;
        }

        public override void outputLogToConsole()
        {
            List<JObject> log = parseLogFile(LOG_INTERACTION);
            foreach (JObject message in log)
            {
                Debug.WriteLine("");
                Debug.WriteLine("==============");
                Debug.WriteLine("");
                foreach (JProperty p in message.Properties())
                {
                    Debug.WriteLine("Log: " + p.Name + ": " + p.Value);
                }
                Debug.WriteLine("");
                Debug.WriteLine("==============");
                Debug.WriteLine("");
            }
        }
    }
}
