using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Domain;
using Oracle.ManagedDataAccess.Client;
//using Oracle.DataAccess.Client;

namespace Inventory.Service
{
    public class Class1
    {
        public List<test> ConnectDB()
        {
            List<test> list = new List<test>();

           OracleConnection OracleConn = new OracleConnection("Data Source=(DESCRIPTION ="
                +"(ADDRESS_LIST ="
                  +"(ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.11.39)(PORT = 1521))"
                +")"
                +"(CONNECT_DATA ="
                  +"(SERVICE_NAME = materialDB)"
                +")"
              +"); user id=STOCK;Password=1234");

            OracleConn.Open();
            OracleDataAdapter OrclAd = new OracleDataAdapter("select * from test", OracleConn);

            OracleConn.Close();

            DataSet a = new DataSet();

            OrclAd.Fill(a);
            DataTable dt = a.Tables[0];

            list.Add(new test(dt.Rows[0]["KEY"].ToString(), dt.Rows[0]["VALUE"].ToString()));

            return list;

        }

    }
}
