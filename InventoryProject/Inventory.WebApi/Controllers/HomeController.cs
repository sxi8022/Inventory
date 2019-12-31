using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult Index()
        {
            return Json("재고관리", JsonRequestBehavior.AllowGet);
        }
    }
}
