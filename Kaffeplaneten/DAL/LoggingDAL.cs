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

namespace Kaffeplaneten.DAL
{
    public class LoggingDAL
    {
        public string LOG_DATABASE = AppDomain.CurrentDomain.BaseDirectory + "\\log_database.txt";
        public string LOG_INTERACTION = AppDomain.CurrentDomain.BaseDirectory + "..\\log_interaction.txt";

        public bool logToUser(string message)
        {
            createLog(LOG_INTERACTION);
            CustomerModel user;
            EmployeeModel employee;
            string logLine = "";
                if (HttpContext.Current.Session == null || HttpContext.Current.Session["LoggedInn"] == null || (bool)HttpContext.Current.Session["LoggedInn"] == false)
                {
                    user = new CustomerModel()
                    {
                        customerID = 0,
                        firstName = "Anonymous",
                        lastName = "",
                        email = "Anonymous"
                    };
                    logLine = ",{ " +
                                "\"Date\": \"" + DateTime.Now.ToString("h:mm:ss tt") + "\"," +
                                "\"UserID\": \"" + user.customerID + "\"," +
                                "\"User\": \"" + user.firstName + " " + user.lastName + "\"," +
                                "\"Action\": \"" + message + "\" }";
                }
                else if ((bool)HttpContext.Current.Session["LoggedInn"] == true && HttpContext.Current.Session["Customer"] != null)
                {
                    user = (CustomerModel)HttpContext.Current.Session["Customer"];
                    logLine = ",{ " +
                                 "\"Date\": \"" + DateTime.Now.ToString("h:mm:ss tt") + "\"," +
                                 "\"UserID\": \"" + user.customerID + "\"," +
                                 "\"User\": \"" + user.firstName + " " + user.lastName + "\"," +
                                 "\"Action\": \"" + message + "\" }";
                }
                else if ((bool)HttpContext.Current.Session["LoggedInn"] == true && HttpContext.Current.Session["Customer"] == null)
                {
                    logLine = ",{ " +
                             "\"Date\": \"" + DateTime.Now.ToString("h:mm:ss tt") + "\"," +
                             "\"UserID\": \"" + (string)HttpContext.Current.Session["username"] + "\"," +
                             "\"User\": \"" + (string)HttpContext.Current.Session["firstname"] + " " + (string)HttpContext.Current.Session["lastname"] + "\"," +
                             "\"Action\": \"" + message + "\" }";
                }
            else
            {
            }
        

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
        public bool logToDatabase(string message)
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
                    Debug.WriteLine("TESTED: " + message);
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
        public JObject findInDatabaseLog(string criteria)
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
        public JObject findInInteractionLog(string criteria)
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

        public List<JObject> parseLogFile(string log)
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

        public JArray parseToArray(string log)
        {
            string entireLog = File.ReadAllText(log) + "]";
            JArray a = JArray.Parse(entireLog);
            return a;
        }

        public bool createLog(string type)
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

        public void outputLogToConsole()
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
