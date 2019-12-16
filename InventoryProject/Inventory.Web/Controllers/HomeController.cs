using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Domain;

namespace Inventory.Web.Controllers
{
    public class HomeController : Controller
    {
        Class1 class1 = new Class1();

        // GET: Home
        public JsonResult Index()
        {
            List<test> testList = class1.ConnectDB();
            

            return Json(testList, JsonRequestBehavior.AllowGet);
        }
        
    }
}