
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Domain;
using System.Data;
using System.Collections.Generic;
using System;

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
        public JsonResult IpgoAdd(string stockNo, string matNo, string ipchulCnt, string stockType, string ipchulDate, string rmk)
        {
            bool res = query.InsertStock(stockNo, matNo, string.IsNullOrEmpty(ipchulCnt) ? 0.0 : Convert.ToDouble(ipchulCnt), stockType, ipchulDate, rmk);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 입고수정
        /// </summary>
        public JsonResult IpgoUpDate(string stockNo, string matNo, string ipchulCnt, string stockType, string rmk, string ipchulDate)
        {
            bool res = query.UpdateStock(
                stockNo,
                matNo,
                string.IsNullOrEmpty(ipchulCnt) ? 0.0 : Convert.ToDouble(ipchulCnt),
                stockType,
                rmk,
                ipchulDate
            );
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 입고삭제
        /// </summary>
        public JsonResult IpgoDelete(string stockNo, string matNo, string ipchulDate, string pType)
        {
            bool res = query.DeleteStock(stockNo, matNo, ipchulDate, pType);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 출고 화면 조회
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public JsonResult ChulgoList(string fromDate, string toDate)
        {
            List<Stock> stockOutList = query.SelectOutMaterial(fromDate, toDate);
            return Json(stockOutList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 출고 화면 상세 데이터 조회
        /// </summary>
        /// <returns></returns>
        public JsonResult ChulgoSpeList()
        {
            List<Stock> stockOutSpeList = query.SelectOutMaterialSub(Request.Params["stockNo"].ToString());

            return Json(stockOutSpeList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 출고추가
        /// </summary>
        public JsonResult ChulgoAdd(string stockNo, string matNo, string ipchulCnt, string stockType, string ipchulDate, string rmk)
        {
            bool res = query.InsertStock(stockNo, matNo, string.IsNullOrEmpty(ipchulCnt) ? 0.0 : Convert.ToDouble(ipchulCnt), stockType, ipchulDate, rmk);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 출고수정
        /// </summary>
        public JsonResult ChulgoUpDate(string stockNo, string matNo, string ipchulCnt, string stockType, string rmk, string ipchulDate)
        {
            bool res = query.UpdateStock(
                stockNo,
                matNo,
                string.IsNullOrEmpty(ipchulCnt) ? 0.0 : Convert.ToDouble(ipchulCnt),
                stockType,
                rmk,
                ipchulDate
            );
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 출고삭제
        /// </summary>
        public JsonResult ChulgoDelete(string stockNo, string matNo, string ipchulDate, string pType)
        {
            bool res = query.DeleteStock(stockNo, matNo, ipchulDate, pType);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult MaterialGrpSearch()
        //{
        //    return Json(query.SelectMatGrp(), JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// 그룹조회
        /// </summary>
        /// <returns></returns>
        public JsonResult MaterialGrpSearch()
        {
            List<MatGrp> grpList = query.SelectMatGrp();
            return Json(grpList, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// 그룹소분류조회
        /// </summary>
        /// <returns></returns>
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

        public void MaterialDelete(string matNo)
        {
            query.DeleteMaterial(matNo);
        }

        /// <summary>
        /// 대분류 수정
        /// </summary>
        public void MatGrpUpdate()
        {
            query.UpdatetMatGrp(
                Request.Params["grpCd"].ToString(),
                Request.Params["grpNm"].ToString(),
                Request.Params["rmk"].ToString()
                );
        }
        /// <summary>
        /// 소분류 수정
        /// </summary>
        public void MatGrpSubUpdate()
        {
            query.UpdateMatGrpSub(
                Request.Params["grpCd"].ToString(),
                Request.Params["subCd"].ToString(),
                Request.Params["grpNm"].ToString(),
                Request.Params["rmk"].ToString()
                );
        }

        /// <summary>
        /// 추가
        /// </summary>
        public void MatGrpAdd()
        {
            query.InsertMatGrp(Request.Params["grpNm"].ToString(), Request.Params["rmk"].ToString());
        }
    }
}