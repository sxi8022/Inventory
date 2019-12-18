
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Domain;
using System.Data;
using System.Collections.Generic;

namespace Inventory.Web.Controllers
{
    public class HomeController : Controller
    {
        Query query = new Query();

        // GET: Home
        public JsonResult Index()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult MaterialSearch()
        {
            List<Material> materialList = query.SelectMaterial(Request.Params["matNm"].ToString());

            return Json(materialList, JsonRequestBehavior.AllowGet);
        }


    }
}