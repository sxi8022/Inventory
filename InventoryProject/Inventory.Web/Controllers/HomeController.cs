
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Domain;
using System.Data;
using System.Collections.Generic;

namespace Inventory.Web.Controllers
{
    /// <summary>
    /// 재고관리
    /// </summary>
    public class HomeController : Controller
    {
        Query query = new Query();

        // GET: Home
        public JsonResult Index()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// 자재 마스터 조회
        /// </summary>
        /// <returns></returns>
        public JsonResult MaterialSearch(string matNm = "")
        {
            List<Material> materialList = query.SelectMaterial(matNm);

            return Json(materialList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 입고 화면 조회
        /// </summary>
        /// <returns></returns>
        public JsonResult IpgoList(string fromDate, string toDate)
        {
            List<Stock> stockInList = query.SelectInMaterial(fromDate, toDate);

            return Json(stockInList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 입고 화면 상세 데이터 조회
        /// </summary>
        /// <returns></returns>
        public JsonResult IpgoSpeList()
        {
            List<Stock> stockInSpeList = query.SelectInMaterialSub(Request.Params["stockNo"].ToString());

            return Json(stockInSpeList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 입고입력
        /// </summary>
        public JsonResult IpgoAdd(string pMatNo, double pIpchulCnt, string pStockType, string pIpchulDate, string pRmk)
        {
            bool res = query.InsertIpgo(pMatNo, pIpchulCnt, pStockType, pIpchulDate, pRmk);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 입고수정
        /// </summary>
        public JsonResult IpgoUpdate(string pStockNo, string pMatNo, double pIpchulCnt, string pStockType, string pRmk, string pDate)
        {
            bool res = query.UpdateStock(
                pStockNo,
                pMatNo,
                pIpchulCnt,
                pStockType,
                pRmk,
                pDate
            );
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 입고삭제
        /// </summary>
        public JsonResult IpgoDelete(string pStockNo, string pMatNo, string pDate, string pType)
        {
            bool res = query.DeleteStock(pMatNo, pMatNo, pDate, pType);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 출고 화면 조회
        /// </summary>
        /// <returns></returns>
        public JsonResult chulgoList()
        {
            List<Stock> stockOutList = query.SelectOutMaterial(Request.Params["fromDate"].ToString(), Request.Params["toDate"].ToString());

            return Json(stockOutList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 출고 화면 상세 데이터 조회
        /// </summary>
        /// <returns></returns>
        public JsonResult chulgoSpeList()
        {
            List<Stock> stockOutSpeList = query.SelectOutMaterialSub(Request.Params["stockNo"].ToString());

            return Json(stockOutSpeList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 출고추가
        /// </summary>
        public JsonResult ChulgoAdd(string pStockNo, string pMatNo, double pIpchulCnt, string pStockType, string pIpchulDate, string pRmk)
        {
            bool res = query.InsertStock(pStockNo, pMatNo, pIpchulCnt, pStockType, pIpchulDate, pRmk);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 출고수정
        /// </summary>
        public JsonResult ChulgoUpdate(string pStockNo, string pMatNo, double pIpchulCnt, string pStockType, string pRmk, string pDate)
        {
            bool res = query.UpdateStock(
                pStockNo,
                pMatNo,
                pIpchulCnt,
                pStockType,
                pRmk,
                pDate
            );
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 출고삭제
        /// </summary>
        public JsonResult ChulgoDelete(string pStockNo, string pMatNo, string pDate, string pType)
        {
            bool res = query.DeleteStock(pMatNo, pMatNo, pDate, pType);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MaterialGrpSearch() {
            return Json(query.SelectMatGrp(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MaterialSubGrpSearch()
        {
            return Json(query.SelectMatGrpSub(Request.Params["grpCd"].ToString()), JsonRequestBehavior.AllowGet);
        }

        public void MaterialAdd()
        {
            //string pMatNm, string pItemNO, string pGrpCd, string pSubCd, string pRmk
            query.InsertMaterial(
                Request.Params["matNm"].ToString(),
                Request.Params["itemNo"].ToString(),
                Request.Params["grpCd"].ToString(),
                Request.Params["subCd"].ToString(),
                Request.Params["rmk"].ToString()
            );
        }

        public void MaterialUpdate()
        {
            //string pMatNm, string pItemNO, string pGrpCd, string pSubCd, string pRmk
            query.UpdateMaterial(
                Request.Params["matNo"].ToString(),
                Request.Params["matNm"].ToString(),
                Request.Params["itemNo"].ToString(),
                Request.Params["grpCd"].ToString(),
                Request.Params["subCd"].ToString(),
                Request.Params["rmk"].ToString()
            );
        }

        public JsonResult StockSearch()
        {
            return Json(query.SelectStock(Request.Params["matNm"].ToString()), JsonRequestBehavior.AllowGet);
        }
    }
}