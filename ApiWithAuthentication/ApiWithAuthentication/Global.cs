using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data;

namespace ApiWithAuthentication
{
    public static class Global
    {
        public static string localConn;
        public static System.Data.DataTable callSqlCommand(string connStr,string commType,string commTextOrSp,string retType)
        {
            try
            {
                SqlDataReader sqlReader = null;
                System.Data.DataTable sqlDataTable = null;
                SqlConnection sqlConn = new SqlConnection(connStr);
                if (sqlConn.State == System.Data.ConnectionState.Closed) { sqlConn.Open(); }
                SqlCommand sqlComm = new SqlCommand();
                if (commType == "Text")
                {
                    sqlComm.CommandType = System.Data.CommandType.Text;
                }
                else
                {
                    sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                }
                sqlComm.CommandText = commTextOrSp;
                if (retType == "DataTable")
                {
                    sqlReader = sqlComm.ExecuteReader();
                    sqlDataTable.Load(sqlReader);
                    return sqlDataTable;
                }
                else
                {
                    sqlComm.ExecuteNonQuery();
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "');</script>");
                return null;
            }
        }
    }
}