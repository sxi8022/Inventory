using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain
{
    public class Material
    {
        public int matNo { get; set; }
        public string matNm { get; set; }
        public string itemNo { get; set; }
        public string grpCd { get; set; }
        public string grpNm { get; set; }
        public string subGrpCd { get; set; }
        public string subGrpNm { get; set; }
        public string rmk { get; set; }
    }
}
