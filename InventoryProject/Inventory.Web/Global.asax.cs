using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Inventory.Service.FrameWork;
using System.Text;
using System.Data;
using Inventory.Service;

namespace Inventory.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        DBConnect db = new DBConnect();
        Query qry = new Query();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //qry.InsertStock("6", "2", 33, "I", "2019-12-12", "비고");
            //qry.UpdateStock("1", "2", 7, "I", "", "2019-12-13");
            //qry.UpdateStock("4", "1", 10, "O", "qq", "2019-12-14");
            //qry.DeleteStock("1", "1", "2019-12-13", "I");
            qry.SelectMatGrp();



        }
    }
}
