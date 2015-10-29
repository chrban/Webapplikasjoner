using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaffeplaneten.DAL
{
    public class LoggingDAL
    {
        public const string LOG_DATABASE = "../../Content/log_database.txt";
        public const string LOG_INTERACTION = "../../Content/log_interaction.txt";

        public bool logToUser(Persons user, string message)
        {
            createLog(LOG_INTERACTION);
            string logLine = "{ " +
                              "'User': '" + user.firstName + " " + user.lastName + "'," +
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
            catch (Exception)
            {
                System.Console.WriteLine("ERROR: COULD NOT LOG ACTION TO USER.");
                return false;
            }
        }
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
            catch (Exception)
            {
                System.Console.WriteLine("ERROR: COULD NOT LOG DATABASE ACTION.");
                return false;
            }
        }

        // Find messages with certain criteria.
        // Needs to be done.
        public bool findInDatabaseLog(string criteria)
        {
            return false;
        }
        public bool findInInteractionLog(string criteria)
        {
            return false;
        }

        public bool createLog(string type)
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
                        return true;
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
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
