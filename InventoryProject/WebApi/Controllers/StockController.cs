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
    /// <summary>
    /// 재고
    /// </summary>
    public class StockController : ApiController
    {
        Query query = new Query();

        /// <summary>
        /// 재고 조회
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Stock> Get(string id = "")
        {
            return query.SelectStock(id);
        }
    }
}
