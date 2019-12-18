﻿
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
        public JsonResult MaterialSearch()
        {
            List<Material> materialList = query.SelectMaterial(Request.Params["matNm"].ToString());

            return Json(materialList, JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        ///// 입고 화면 조회
        ///// </summary>
        ///// <returns></returns>
        //public JsonResult IpgoList()
        //{
        //    List<Stock> stockInList = query.SelectInMaterial(Request.Params["fromDate"].ToString(), Request.Params["toDate"].ToString());

        //    return Json(stockInList, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// 입고 화면 상세 데이터 조회
        ///// </summary>
        ///// <returns></returns>
        //public JsonResult IpgoSpeList()
        //{
        //    List<Stock> stockInSpeList = query.SelectInMaterialSub(Request.Params["stockNo"].ToString());

        //    return Json(stockInSpeList, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// 출고 화면 조회
        ///// </summary>
        ///// <returns></returns>
        //public JsonResult chulgoList()
        //{
        //    List<Stock> stockOutList = query.SelectOutMaterial(Request.Params["fromDate"].ToString(), Request.Params["toDate"].ToString());

        //    return Json(stockOutList, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// 출고 화면 상세 데이터 조회
        ///// </summary>
        ///// <returns></returns>
        //public JsonResult chulgoSpeList()
        //{
        //    List<Stock> stockOutSpeList = query.SelectOutMaterialSub(Request.Params["stockNo"].ToString());

        //    return Json(stockOutSpeList, JsonRequestBehavior.AllowGet);
        //}
    }
}