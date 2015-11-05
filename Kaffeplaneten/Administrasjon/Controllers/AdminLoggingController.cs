using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaffeplaneten.BLL;
using Newtonsoft.Json.Linq;

namespace Administrasjon.Controllers
{
    public class AdminLoggingController : Controller
    {

        private LoggingBLL _loggingBLL;

        public AdminLoggingController(){
            _loggingBLL = new LoggingBLL();
        }

        // GET: AdminLogging
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public List<JObject> getInteractionMessages()
        {
            return _loggingBLL.getInteractionMessages();
        }

        [HttpGet]
        public List<JObject> getDatabaseMessages()
        {
            return _loggingBLL.getDatabaseMessages();
        }
    }
}