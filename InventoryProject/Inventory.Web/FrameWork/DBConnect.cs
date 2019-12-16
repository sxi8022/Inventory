using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Data;



namespace Inventory.Web.FrameWork
{
    public class DBConnect
    {
        StringBuilder connStr = new StringBuilder();
        private OracleConnection gConn = null;
        OracleTransaction gTrans = null;

        public  DBConnect()
        {
            connStr.Append(@"Data Source=(DESCRIPTION ="
                + "(ADDRESS_LIST ="
                  + "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.10.11.39)(PORT = 1521))"
                + ")"
                + "(CONNECT_DATA ="
                  + "(SERVICE_NAME = materialDB)"
                + ")"
              + "); user id=STOCK;Password=1234");
        }

        private OracleConnection Open()
        {
            OracleConnection conn = null;

            try
            {
                conn = new OracleConnection(connStr.ToString());
                conn.Open();
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                conn = null;
            }

            return conn;
        }

        private bool OpenConn()
        {
            try
            {
                gConn = new OracleConnection(connStr.ToString());
                gConn.Open();
                return true;
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return false;
        }

        public void CloseConn()
        {
            if (gConn != null)
                gConn.Close();
        }

        public DataTable ExecuteQuery(string pQuery)
        {
            DataTable dt = new DataTable();
            dt.TableName = "vns";
            OracleConnection conn = Open();
            if (conn == null)
                return dt;

            try
            {
                OracleDataAdapter dataAdapter = new OracleDataAdapter(pQuery, conn);

                if (gTrans != null)
                    dataAdapter.SelectCommand.Transaction = gTrans;
                dataAdapter.Fill(dt);
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            return dt;
        }

        public DataSet ExecuteQuery(string[] pQuery)
        {
            DataSet ds = new DataSet();
            ds.Namespace = "ds-vns";
            for (int i = 0; i < pQuery.Length; i++)
            {
                if (pQuery[i] == null || "".Equals(pQuery[i]))
                    continue;

                if (!pQuery[i].Equals(""))
                    ds.Tables.Add("vns" + i);
            }

            OracleConnection conn = Open();

            if (conn == null)
                return ds;

            string s = "";

            try
            {
                for (int i = 0, j = 0; i < pQuery.Length; i++)
                {
                    if (null == pQuery[i] || "".Equals(pQuery[i]) || string.IsNullOrWhiteSpace(pQuery[i]))
                        continue;

                    s = pQuery[i];
                    OracleDataAdapter dataAdapter = new OracleDataAdapter(pQuery[i], conn);

                    dataAdapter.Fill(ds.Tables[j]);
                    j++;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(s);
                System.Console.WriteLine(ex.Message + " == " + pQuery);
            }
            finally
            {
                conn.Close();
            }

            return ds;
        }

        public bool ExecuteTranaction(string pQuery)
        {
            bool bResult = false;
            OracleConnection conn = null;
            OracleTransaction trans = null;

            pQuery = pQuery.Replace("[<brbr>]", "\r\n");
            conn = Open();
            if (conn == null)
                return false;

            string[] ar = pQuery.Split(';');

            for (int i = 0; i < ar.Length - 1; i++)
                ar[i] = ar[i].Trim();

            trans = conn.BeginTransaction();
            OracleCommand Command = new OracleCommand(pQuery, conn);

            for (int i = 0; i < ar.Length; i++)
            {
                if (ar[i].Trim() == "")
                    break;

                Command.CommandText = ar[i];
                Command.ExecuteNonQuery();
            }
            try
            {
                trans.Commit();
                bResult = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                System.Console.WriteLine(ex.Message + " == " + pQuery);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

            return bResult;
        }






    }
}