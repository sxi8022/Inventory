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
    public class MaterialGrpController : ApiController
    {
        Query query = new Query();

        public List<MatGrp> Get(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                return query.SelectMatGrp();
            }
            else
            {
                return query.SelectMatGrpSub(id);
            }
        }

        // POST api/<controller>
        public void Post(MatGrp matGrp)
        {
            query.InsertMatGrp(matGrp.grpNm, matGrp.rmk);
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
        public void Delete(string id)
        {
            query.DeleteMatGrp(id);
        }
    }
}
