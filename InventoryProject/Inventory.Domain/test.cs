using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain
{
    public class test
    {
        public test()
        {

        }
        public test(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public string Key
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }
}
