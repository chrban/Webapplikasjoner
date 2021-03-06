﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.IO;
using System.Web.SessionState;
using System.Diagnostics;
using System.Web.Hosting;

namespace Kaffeplaneten
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            //Code that runs on application startup
            //Kaffeplaneten.BLL.DataCreater.addSuperadmin();
            //Kaffeplaneten.BLL.DataCreater.addProducts();
            //Kaffeplaneten.BLL.DataCreater.createCustomer();
            //BLL.DataCreater.addProducts();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}