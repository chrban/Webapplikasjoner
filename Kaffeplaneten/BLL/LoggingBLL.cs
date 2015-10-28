using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;

namespace Kaffeplaneten.BLL
{
    class LoggingBLL
    {
        private LoggingDAL _loggingDAL;

        public LoggingBLL()
        {
            _loggingDAL = new LoggingDAL();
        }

        public bool logToUser(Persons user, string action) {
            return _loggingDAL.logToUser(user, action);
        }
        public bool logToDatabase(string action)
        {
            return _loggingDAL.logToDatabase(action);
        }

        // Find messages with certain criteria.
        // Needs to be done.
        public bool findInDatabaseLog(string criteria)
        {
            return _loggingDAL.findInDatabaseLog(criteria);
        }
        public bool findInInteractionLog(string criteria)
        {
            return _loggingDAL.findInInteractionLog(criteria);
        }

        public bool createLog(string type)
        {
            return _loggingDAL.createLog(type);
        }


    }
}
