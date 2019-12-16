using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Inventory.Web.FrameWork;
using System.Text;
using System.Data;
using System.Diagnostics;

namespace Inventory.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        DBConnect db = new DBConnect();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

//            StringBuilder sb = new StringBuilder();

//            sb.Append(@"Select *
//From material");
//            DataTable dt = db.ExecuteQuery(sb.ToString());

//            Debug.WriteLine(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString());
            
//            sb.Clear();
//            sb.Append(@"Insert Into test (key, value) Values(
//'111', '111')");

//            db.ExecuteTranaction(sb.ToString());
         

        }
    }
}
