using Inventory.Domain;
using Inventory.Service;
using System.Collections.Generic;
using System.Web.Http;
using System.Web;

namespace WebApi.Controllers
{
    public class SubGroupController : ApiController
    {
        Query qry = new Query();

        public List<MatGrp> Get(string id)
        {
            return qry.SelectMatGrpSub(id);
        }

    }
}
