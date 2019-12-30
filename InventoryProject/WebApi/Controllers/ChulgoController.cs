using Inventory.Domain;
using Inventory.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ChulgoController : ApiController
    {
        Query query = new Query();

        /// <summary>
        /// 출고 조회
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Stock> Get(string fromDate, string toDate)
        {
            return query.SelectOutMaterial(fromDate, toDate);
        }

        /// <summary>
        /// 출고 화면 상세 데이터 조회
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Stock> Get(int stockNo)
        {
            return query.SelectOutMaterialSub(Convert.ToString(stockNo));
        }

        /// <summary>
        /// 출고 추가
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Post(Stock stock)
        {
            bool res = false;
            if (stock != null)
            {
                res = query.InsertStock(
                    Convert.ToString(stock.stockNo),
                    Convert.ToString(stock.matNo),
                    stock.ipchulCnt,
                    stock.stockType,
                    stock.ipchulDate,
                    stock.rmk
                );
            }
            return res;
        }

        /// <summary>
        /// 출고 수정
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Put(Stock stock)
        {
            bool res = false;
            if (stock != null)
            {
                res = query.UpdateStock(
                    Convert.ToString(stock.stockNo),
                    Convert.ToString(stock.matNo),
                    stock.ipchulCnt,
                    stock.stockType,
                    stock.ipchulDate,
                    stock.rmk
                );
            }
            return res;
        }

        /// <summary>
        /// 출고 삭제
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Delete(string stockNo, string matNo, string ipchulDate, string pType)
        {
            query.DeleteStock(stockNo, matNo, ipchulDate, pType);
        }
    }
}