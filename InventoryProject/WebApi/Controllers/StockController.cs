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
    public class StockController : ApiController
    {
        Query query = new Query();

        public List<Stock> Get(string id = "")
        {
            return query.SelectStock(id);
        }
    }
}
