using SchemaNote_11170__2_.Models.DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace SchemaNote_11170__2_.Models.DataAccess
{
    public class DA_ConnectionString
    {
        public bool isConnection(DO_ConnectionString conn)
        {
            //回傳：true=連線成功，false=連線失敗
            try
            {
                if (!string.IsNullOrEmpty(conn.MixConnectionString()))
                {
                    SqlConnection connection = new SqlConnection(conn.MixConnectionString());
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
        
                return false;
            }
            return false;
        }
    }
}