using Inventory.Domain;
using Inventory.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class MaterialSubGrpController : ApiController
    {
        Query query = new Query();

        // POST api/<controller>
        public void Post(MatGrp matGrp)
        {
            query.InsertMatGrpSub(matGrp.grpCd, matGrp.subNm, matGrp.rmk);
        }

        // PUT api/<controller>/5
        public void Put(MatGrp matGrp)
        {
            query.UpdateMatGrpSub(
                matGrp.grpCd,
                matGrp.subCd,
                matGrp.grpNm,
                matGrp.rmk
            );
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(string grpCd = "", string subCd = "")
        {
            query.DeleteMatGrpSub(grpCd, subCd);
        }
    }
}
