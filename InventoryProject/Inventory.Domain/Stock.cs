using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain
{
    public class Stock
    {
        public int stockNo { get; set; }
        public int matNo { get; set; }
        public string matNm { get; set; }
        public double ipchulCnt { get; set; }
        public string stockType { get; set; }
        public string ipchulDate { get; set; }
        public double stockCnt { get; set; }
        public string rmk { get; set; }
        public string custCd { get; set; }
        public string itemNo { get; set; }
        //
    }
}
