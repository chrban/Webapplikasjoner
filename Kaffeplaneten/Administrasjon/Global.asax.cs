﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Administrasjon
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Kaffeplaneten.BLL.DataCreater.addSuperadmin();
            //Kaffeplaneten.BLL.DataCreater.addProducts();
            //Kaffeplaneten.BLL.DataCreater.createCustomer();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
