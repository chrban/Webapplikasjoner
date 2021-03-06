﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaffeplaneten.BLL;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Administrasjon.Controllers
{
    public class AdminLoggingController : Controller
    {

        private LoggingBLL _loggingBLL;

        public AdminLoggingController()
        {
            _loggingBLL = new LoggingBLL();
        }
        public AdminLoggingController(LoggingBLL loggingBLL)
        {
            _loggingBLL = loggingBLL;
        }

        // GET: AdminLogging
        public ActionResult Logging()
        {
            return View("Logging");
        }

        [HttpPost]
        public JArray getInteractionMessages()
        {
            return _loggingBLL.getInteractionMessages();
        }

        [HttpPost]
        public JArray getDatabaseMessages()
        {
            return _loggingBLL.getDatabaseMessages();
        }
    }
}