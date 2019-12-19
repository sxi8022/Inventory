using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Inventory.Service.FrameWork;
using Inventory.Domain;

namespace Inventory.Service
{
    public class Query
    {
        StringBuilder sb = new StringBuilder();
        DBConnect db = new DBConnect();

     
        public List<MatGrp> SelectMatGrp()
        {//대분류 조회
            sb.Clear();
            sb.Append(@"
Select grp_cd, grp_nm, seq, rmk
From MAT_GRP
Where sub_cd = 0
");
            DataTable dt = db.ExecuteQuery(sb.ToString());

            List<MatGrp> matGrpLi = new List<MatGrp>();

            foreach (DataRow dr in dt.Rows)
            {
                MatGrp mat = new MatGrp();

                mat.grpCd = dr["grp_cd"].ToString();
                mat.grpNm = dr["grp_nm"].ToString();
                mat.seq = Convert.ToInt32(dr["seq"]);
                mat.rmk = dr["rmk"].ToString();
                matGrpLi.Add(mat);
            }

            return matGrpLi;
        }

        public List<MatGrp> SelectMatGrpSub(string grpCd = null)
        {//중분류 조회
            sb.Clear();
            sb.Append(@"
Select M_grp1.grp_cd
    , M_grp1.grp_nm 
    , M_grp2.sub_cd 
    , M_grp2.grp_nm sub_nm
    , M_grp2.seq
    , M_grp2.rmk
From MAT_GRP M_GRP1
Join MAT_GRP M_GRP2 On M_grp1.grp_cd = M_grp2.grp_cd 
Where M_grp1.sub_cd = 0 And M_grp2.sub_cd <> 0");
            if (!string.IsNullOrEmpty(grpCd))
            {
                sb.AppendFormat(@"
                   AND M_GRP1.grp_cd = '{0}'
                ", grpCd);
            }

            DataTable dt = db.ExecuteQuery(sb.ToString());
            List<MatGrp> matGrpLi = new List<MatGrp>();

            foreach(DataRow dr in dt.Rows)
            {
                MatGrp mat = new MatGrp();

                mat.grpCd = dr["grp_cd"].ToString();
                mat.grpNm = dr["grp_nm"].ToString();
                mat.subCd = dr["sub_cd"].ToString();
                mat.subNm = dr["sub_nm"].ToString();
                mat.seq = Convert.ToInt32(dr["seq"]);
                mat.rmk = dr["rmk"].ToString();
                matGrpLi.Add(mat);
            }
            return matGrpLi;
        }

        public List<Material> SelectMaterial(string matNm = null)
        {//자재마스터 조회
            sb.Clear();
            sb.Append(@"
                Select mat_no -- 제품코드
                    , mat_nm -- 제품명
                    , item_no -- 품번
                    , Mat.grp_cd -- 대분류코드
                    , Grp2.grp_nm -- 대분류명
                    , Mat.sub_cd -- 중분류코드
                    , Grp.grp_nm sub_nm-- 중분류명
                    , Mat.rmk -- 비고
                From MATERIAL Mat
                Join MAT_GRP Grp On Grp.grp_cd = Mat.grp_cd And Grp.sub_cd = Mat.sub_cd
                Join Mat_GRP Grp2 On Grp2.grp_cd = Mat.grp_cd And Grp2.sub_cd = 0
            ");

            if(!string.IsNullOrEmpty(matNm))
            {
                sb.Append(@"
                    Where
                        Mat.mat_nm LIKE '%" + matNm + "%'"
                );
            }

            DataTable dt = db.ExecuteQuery(sb.ToString());

            List<Material> materialList = new List<Material>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Material material = new Material();

                material.grpCd = dt.Rows[i]["grp_cd"].ToString();
                material.matNo = Convert.ToInt32(dt.Rows[i]["mat_no"]);
                material.matNm = dt.Rows[i]["mat_nm"].ToString();
                material.itemNo = dt.Rows[i]["item_no"].ToString();
                material.grpNm = dt.Rows[i]["grp_nm"].ToString();
                material.subGrpCd = dt.Rows[i]["sub_cd"].ToString();
                material.subGrpNm = dt.Rows[i]["sub_nm"].ToString();
                material.rmk = dt.Rows[i]["rmk"].ToString();

                materialList.Add(material);
            }

            return materialList;
        }
        /// <summary>
        /// 자재입고 조회
        /// </summary>
        /// <param name="pFrom"></param>
        /// <param name="pTo"></param>
        /// <returns></returns>
        public List<Stock> SelectInMaterial(string pFrom, string pTo)
        {
            sb.Clear();
            sb.Append(@"
            Select stock_no 
                , ipchul_date 
            From STOCK Sto
            Where stock_type = 'I' And ipchul_date Between '" + pFrom + @"' And '" + pTo + @"'
            Group By stock_no, ipchul_date
            Order By stock_no");

            DataTable dt = db.ExecuteQuery(sb.ToString());
            List<Stock> stockLi = new List<Stock>();
            foreach (DataRow dr in dt.Rows)
            {
                Stock sto = new Stock();

                sto.stockNo = Convert.ToInt32(dr["stock_no"].ToString());
                sto.ipchulDate = dr["ipchul_date"].ToString();

                stockLi.Add(sto);
            }
            return stockLi;
        }

        /// <summary>
        /// 자재입고sub 조회
        /// </summary>
        /// <param name="pStockNo"></param>
        /// <returns></returns>
        public List<Stock> SelectInMaterialSub(string pStockNo)
        {
            sb.Clear();
            sb.Append(@"
            Select stock_no 
                , ipchul_date
                , stock_type 
                , Mat.mat_no 
                , Mat.mat_nm 
                , Mat.item_no 
                , Sto.ipchul_cnt 
                , Sto.rmk
            From STOCK Sto
            Join MATERIAL Mat On Mat.mat_no = Sto.mat_no
            Where stock_no = '" + pStockNo + @"'");

            DataTable dt = db.ExecuteQuery(sb.ToString());
            List<Stock> stockLi = new List<Stock>();
            foreach(DataRow dr in dt.Rows)
            {
                Stock sto = new Stock();

                sto.stockNo = Convert.ToInt32(dr["stock_no"].ToString());
                sto.ipchulDate = dr["ipchul_date"].ToString();
                sto.stockType = dr["stock_type"].ToString();
                sto.matNo = Convert.ToInt32(dr["mat_no"].ToString());
                sto.matNm = dr["mat_nm"].ToString();
                sto.itemNo = dr["item_no"].ToString();
                sto.ipchulCnt = Convert.ToDouble(dr["ipchul_cnt"].ToString());
                sto.rmk = dr["rmk"].ToString();

                stockLi.Add(sto);
            }
            return stockLi;
        }

        public List<Stock> SelectOutMaterial(string pFrom, string pTo)
        {//자재출고 조회
            sb.Clear();
            sb.Append(@"
Select stock_no 
    , ipchul_date 
From STOCK Sto
Where stock_type = 'O' And ipchul_date Between '" + pFrom + @"' And '" + pTo + @"'
Group By stock_no, Sto.cust_cd, Cus.cust_nm, ipchul_date
Order By stock_no");

            DataTable dt = db.ExecuteQuery(sb.ToString());
            List<Stock> stockLi = new List<Stock>();
            foreach (DataRow dr in dt.Rows)
            {
                Stock sto = new Stock();

                sto.stockNo = Convert.ToInt32(dr["stock_no"].ToString());
                sto.ipchulDate = dr["ipchul_date"].ToString();

                stockLi.Add(sto);
            }
            return stockLi;
        }

        public List<Stock> SelectOutMaterialSub(string pStockNo)
        {//자재출고sub 조회
            sb.Clear();
            sb.Append(@"
Select stock_no 
    , ipchul_date
    , Mat.mat_no 
    , Mat.mat_nm 
    , Mat.item_no 
    , Sto.ipchul_cnt 
    , Sto.rmk
From STOCK Sto
Join MATERIAL Mat On Mat.mat_no = Sto.mat_no
Where stock_no = '" + pStockNo + @"'");

            DataTable dt = db.ExecuteQuery(sb.ToString());
            List<Stock> stockLi = new List<Stock>();
            foreach (DataRow dr in dt.Rows)
            {
                Stock sto = new Stock();

                sto.stockNo = Convert.ToInt32(dr["stock_no"].ToString());
                sto.ipchulDate = dr["ipchul_date"].ToString();
                sto.matNo = Convert.ToInt32(dr["mat_no"].ToString());
                sto.matNm = dr["mat_nm"].ToString();
                sto.itemNo = dr["item_no"].ToString();
                sto.ipchulCnt = Convert.ToDouble(dr["ipchul_cnt"].ToString());
                sto.rmk = dr["rmk"].ToString();

                stockLi.Add(sto);
            }
            return stockLi;
        }

        public List<Stock> SelectStock()
        {//자재재고 조회
            sb.Clear();
            sb.Append(@"
Select Mat.mat_no 
    , Mat.mat_nm 
    , Mat.item_no 
    , Max(sto.ipchul_cnt) ipchul_cnt
From STOCK Sto
Join MATERIAL Mat On Mat.mat_no = Sto.mat_no
Group By Mat.mat_no, Mat.mat_nm, Mat.item_no
Order By Mat.mat_nm");

            DataTable dt = db.ExecuteQuery(sb.ToString());
            List<Stock> stockLi = new List<Stock>();
            foreach(DataRow dr in dt.Rows)
            {
                Stock sto = new Stock();

                sto.matNo = Convert.ToInt32(dr["mat_no"].ToString());
                sto.matNm = dr["mat_nm"].ToString();
                sto.itemNo = dr["item_no"].ToString();
                sto.ipchulCnt = Convert.ToDouble(dr["ipchul_cnt"].ToString());

                stockLi.Add(sto);
            }
            return stockLi;
        }

        public bool InsertMatGrp(string pGrpCd, string pSeq, string pGrpNm, string pRmk)
        {
            sb.Clear();
            sb.Append(@"
Insert Into mat_grp
(GRP_CD, SUB_CD, SEQ, GRP_NM, RMK) Values
('" + pGrpCd + "', '0', '" + pSeq + "', '" + pGrpNm + "', '" + pRmk + "')");

            return db.ExecuteTranaction(sb.ToString());
        }

        public bool UpdatetMatGrp(string pGrpCd, string pGrpNm, string pRmk)
        {
            sb.Clear();
            sb.Append(@"
Update MAT_GRP Set
    GRP_NM = '" + pGrpNm + @"'
    , RMK = ''
Where GRP_CD = '" + pGrpCd + @"' And SUB_CD = '0';
");
            return db.ExecuteTranaction(sb.ToString());
        }

        public bool DeleteMatGrp(string pGrpCd)
        {
            sb.Clear();
            sb.Append(@"
Delete From MAT_GRP
Where GRP_CD = '" + pGrpCd + @"' And SUB_CD = '0';
");

            return db.ExecuteTranaction(sb.ToString());
        }

        public bool InsertMatGrpSub(string pGrpCd, string pSubCd, string pSeq, string pGrpNm, string pRmk)
        {
            sb.Clear();
            sb.Append(@"
Insert Into MAT_GRP
(GRP_CD, SUB_CD, SEQ, GRP_NM, RMK) Values
('" + pGrpCd + "', '" + pSubCd + "', '" + pSeq + "', '" + pGrpNm + "', '" + pRmk + @"');
");
            return db.ExecuteTranaction(sb.ToString());
        }

        public bool UpdateMatGrpSub(string pGrpCd, string pSubCd, string pGrpNm, string pRmk)
        {
            sb.Clear();
            sb.Append(@"
Update MAT_GRP Set
    GRP_NM = '" + pGrpNm + @"'
    , RMK = '" + pRmk + @"'
Where GRP_CD = '" + pGrpCd + "' And SUB_CD = '" + pSubCd + @"';
");

            return db.ExecuteTranaction(sb.ToString());
        }

        public bool DeleteMatGupSub(string pGrpCd, string pSubCd)
        {
            sb.Clear();
            sb.Append(@"
Delete From MAT_GRP
Where GRP_CD = '" + pGrpCd + "' And SUB_CD = '" + pSubCd + @"';
");
            return db.ExecuteTranaction(sb.ToString());
        }

        public bool InsertMaterial(string pMatNm, string pItemNO, string pGrpCd, string pSubCd, string pRmk)
        {
            sb.Clear();
            sb.Append(@"
Insert Into MATERIAL
(MAT_NO, MAT_NM, ITEM_NO, GRP_CD, SUB_CD, RMK) Values
(MATERIAL_SEQ.nextval,'" + pMatNm + "', '" + pItemNO + "', '" + pGrpCd + "', '" + pSubCd + "', '" + pRmk + @"');
");
            return db.ExecuteTranaction(sb.ToString());
        }

        public bool UpdateMaterial(string pMatNo, string pMatNm, string pItemNO, string pGrpCd, string pSubCd, string pRmk)
        {
            sb.Clear();
            sb.Append(@"
Update MATERIAL Set
    MAT_NM = '" + pMatNm + @"'
    , ITEM_NO = '" + pItemNO + @"'
    , GRP_CD = '" + pGrpCd + @"'
    , SUB_CD = '" + pSubCd + @"'
    , RMK = '" + pRmk + @"'
Where MAT_NO = '" + pMatNo + @"';
");

            return db.ExecuteTranaction(sb.ToString());
        }

        public bool DeleteMaterial(string pMatNo)
        {
            sb.Clear();
            sb.Append(@"
Delete From MATERIAL
Where MAT_NO = '" + pMatNo + @"';
");
            return db.ExecuteTranaction(sb.ToString());
        }

        /// <summary>
        /// 입고 
        /// </summary>
        /// <param name="pMatNo"></param>
        /// <param name="pIpchulCnt"></param>
        /// <param name="pStockType"></param>
        /// <param name="pIpchulDate"></param>
        /// <param name="pRmk"></param>
        /// <returns></returns>
        public bool InsertIpgo(string pMatNo, double pIpchulCnt, string pStockType, string pIpchulDate, string pRmk)
        {
            sb.Clear();
            sb.Append(@"
Insert Into STOCK
(STOCK_NO, MAT_NO, IPCHUL_CNT, STOCK_TYPE, IPCHUL_DATE, STOCK_CNT, RMK) Values
(STOCK_SEQ.nextval,'" + pMatNo + "', '" + pIpchulCnt + "', '" + pStockType + "', '" + pIpchulDate + "', '" + pIpchulCnt + "', '" + pRmk + @"');
");

            return db.ExecuteTranaction(sb.ToString());
        }

        /// <summary>
        /// 재고추가(출고)
        /// </summary>
        /// <param name="pStockNo"></param>
        /// <param name="pMatNo"></param>
        /// <param name="pIpchulCnt"></param>
        /// <param name="pStockType"></param>
        /// <param name="pIpchulDate"></param>
        /// <param name="pRmk"></param>
        /// <returns></returns>
        public bool InsertStock(string pStockNo, string pMatNo, double pIpchulCnt, string pStockType, string pIpchulDate, string pRmk)
        {
            double stockCnt = 0;

            sb.Clear();
            //데이터 입력하는 일자의 최대 stock_no일때의 재고수량 확인
            sb.Append(@"
Select stock_cnt
From Stock Sto
Join (
    Select Max(stock_no) stock_no
    From Stock
    Where ipchul_date <= '" + pIpchulDate + "' And mat_no = '" + pMatNo + @"'
) Num On Num.stock_no = Sto.stock_no
Where ipchul_date = '" + pIpchulDate + "' And mat_no = '" + pMatNo + "'");
            
            DataTable dt = db.ExecuteQuery(sb.ToString());
            if (dt.Rows.Count > 0)
                double.TryParse(dt.Rows[0][0].ToString(), out stockCnt);

            if (pStockType == "O")//출고데이터인 경우 재고를 빼줘야 함
                pIpchulCnt = pIpchulCnt - pIpchulCnt - pIpchulCnt;
            sb.Clear();
            sb.Append(@"
Insert Into STOCK
(STOCK_NO, MAT_NO, IPCHUL_CNT, STOCK_TYPE, IPCHUL_DATE, STOCK_CNT, RMK) Values
('" + pStockNo + "', '" + pMatNo + "', '" + pIpchulCnt + "', '" + pStockType + "', '" + pIpchulDate + "', '" + (stockCnt + pIpchulCnt) + "', '" + pRmk + @"');
--입출고 일자 기준으로 이후 데이터가 있는 경우 이후 데이터의 재고는 +- 처리해줘야함.
Update STOCK Set
    stock_cnt = stock_cnt + " + pIpchulCnt + @"
Where mat_no = '" + pMatNo + "' And ipchul_date >= '" + pIpchulDate + "' And ipchul_date || stock_no > '" + pIpchulDate+ pStockNo + @"';
");
            return db.ExecuteTranaction(sb.ToString());
        }

        public bool UpdateStock(string pStockNo, string pMatNo, double pIpchulCnt, string pStockType, string pRmk, string pDate)
        {
            double regCnt = 0;
            sb.Clear();
            sb.Append(@"
Select ipchul_cnt
From STOCK
Where stock_no = '" + pStockNo + "' And mat_no = '" + pMatNo + "'");

            DataTable dt = db.ExecuteQuery(sb.ToString());
            double.TryParse(dt.Rows[0][0].ToString(), out regCnt);
            
            sb.Clear();
            sb.Append(@"
Update STOCK Set
    IPCHUL_CNT = '" + pIpchulCnt + @"'
    , STOCK_TYPE = '" + pStockType + @"'");
            if (pStockType == "I")
                sb.Append(@"
    , STOCK_CNT = STOCK_CNT - " + regCnt + " + " + pIpchulCnt);
            else
                sb.Append(@"
    , STOCK_CNT = STOCK_CNT + " + regCnt + " - " + pIpchulCnt);
            sb.Append(@"
    , RMK = '" + pRmk + @"'
Where STOCK_NO = '" + pStockNo + @"' And MAT_NO = '" + pMatNo + @"';


UPDATE STOCK Set");
            if (pStockType == "I")
                sb.Append(@"
    STOCK_CNT = STOCK_CNT - " + regCnt  + " + " + pIpchulCnt);
            else
                sb.Append(@"
    STOCK_CNT = STOCK_CNT + " + regCnt + " - " + pIpchulCnt);
            sb.Append(@"
Where MAT_NO = '" + pMatNo + "' And IPCHUL_DATE >= '" + pDate + "' And ipchul_date || STOCK_NO > '" + pDate + pStockNo + @"';
");

            return db.ExecuteTranaction(sb.ToString());
        }

        public bool DeleteStock(string pStockNo, string pMatNo, string pDate, string pType)
        {
            sb.Clear();
            if (pType == "I")
                sb.Append(@"
Update STOCK Set
    STOCK_CNT = STOCK_CNT - 
        (Select IPCHUL_CNT
        From STOCK
        Where MAT_NO = '" + pMatNo + "' And STOCK_NO = '" + pStockNo + @"')
Where MAT_NO = '" + pMatNo + "' And IPCHUL_DATE >= '" + pDate + "' And ipchul_date || STOCK_NO > '" + pDate + pStockNo + @"';");
            else
                sb.Append(@"
Update STOCK Set
    STOCK_CNT = STOCK_CNT +
        (Select IPCHUL_CNT
        From STOCK
        Where MAT_NO = '" + pMatNo + "' And STOCK_NO = '" + pStockNo + @"')
Where MAT_NO = '" + pMatNo + "' And IPCHUL_DATE >= '" + pDate + "' And ipchul_date || STOCK_NO > '" + pDate + pStockNo + @"';");

            sb.Append(@"
Delete From STOCK
Where MAT_NO = '" + pMatNo + "' And STOCK_NO = '" + pStockNo + @"';
");

            return db.ExecuteTranaction(sb.ToString());
        }

    }
}
