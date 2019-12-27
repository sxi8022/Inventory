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
    public class MaterialController : ApiController
    {
        Query query = new Query();

        public List<Material> Get(string id = "")
        {
            return query.SelectMaterial(id);
        }

        // POST api/<controller>
        public string Post(Material material)
        {
            if (material != null)
            {
                query.InsertMaterial(
                    material.matNm,
                    material.itemNo,
                    material.grpCd,
                    material.subGrpCd,
                    material.rmk
                );
            }
            return material.matNm;
        }

        // PUT api/<controller>/5
        // public void Put(int id, Material material)
        [HttpPut]
        public string Put(int id, Material material)
        {
            return id.ToString();
            //if (material != null)
            //{
            //    query.InsertMaterial(
            //        material.matNm,
            //        material.itemNo,
            //        material.grpCd,
            //        material.subGrpCd,
            //        material.rmk
            //    );
            //}
        }

        // DELETE api/<controller>/5
        public void Delete()  
       { 
       }
    }
}
