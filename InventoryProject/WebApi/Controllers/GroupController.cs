using Inventory.Domain;
using Inventory.Service;
using System.Collections.Generic;
using System.Web.Http;
using System.Web;

namespace WebApi.Controllers
{
    public class GroupController : ApiController
    {
        Query qry = new Query();

        public List<MatGrp> Get()
        {
            return qry.SelectMatGrp();
        }

        //public JsonResult MaterialGrpSearch()
        //{
        //    List<MatGrp> grpList = query.SelectMatGrp();
        //    return Json(grpList, JsonRequestBehavior.AllowGet);
        //}


        public void Post(MatGrp pMat)
        {
            if (pMat == null)
                return;

            qry.InsertMatGrp(pMat.grpNm, pMat.rmk);
        }

        public void Put(MatGrp pMat)
        {
            if (pMat.grpCd == null)
                return;

            qry.UpdatetMatGrp(pMat.grpCd, pMat.grpNm, pMat.rmk);
        }

        public void Delete(MatGrp pMat)
        {
            if (pMat.grpCd == null)
                return;

            qry.DeleteMatGrp(pMat.grpCd);
        }
    }
}
