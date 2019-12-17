using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Inventory.Web.FrameWork;
using System.Text;
using System.Data;

namespace Inventory.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        DBConnect db = new DBConnect();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
