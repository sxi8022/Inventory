 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain
{
    //재고
    public class Stock
    {
        /// <summary>
        /// 재고번호
        /// </summary>
        public int stockNo { get; set; }
        /// <summary>
        /// 자재번호
        /// </summary>
        public int matNo { get; set; }
        /// <summary>
        /// 자재명
        /// </summary>
        public string matNm { get; set; }
        /// <summary>
        /// 입출개수
        /// </summary>
        public double ipchulCnt { get; set; }
        /// <summary>
        /// 입고출고유형
        /// </summary>
        public string stockType { get; set; }
        /// <summary>
        /// 입출고일자
        /// </summary>
        public string ipchulDate { get; set; }
        /// <summary>
        /// 재고개수
        /// </summary>
        public double stockCnt { get; set; }
        /// <summary>
        /// 비고
        /// </summary>
        public string rmk { get; set; }
        /// <summary>
        /// 품번
        /// </summary>
        public string itemNo { get; set; }
    }
}
