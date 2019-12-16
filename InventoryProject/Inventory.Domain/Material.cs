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
        public string rmk { get; set; }
        public string grpCd { get; set; }
        public string subCd { get; set; }
        public string unitCd { get; set; }
    }
}
