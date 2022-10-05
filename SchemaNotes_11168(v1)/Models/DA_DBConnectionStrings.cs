using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SchemaNotes_11168_v1_.Models;

namespace SchemaNotes_11168_v1_.Models
{
    public class DA_DBConnectionStrings
    {
        public Tuple<bool, string> dbconnection(string uid, string pwd, string database, string server) {
            DO_DBconnection DOdbc = new DO_DBconnection();
            string connStrings = DOdbc.connstrings(uid, pwd, database, server);
            if (connStrings.Contains("Error")){
                return Tuple.Create(false, "Error");
            }
            else {
                using (SqlConnection conn = new SqlConnection(connStrings)) {
                    try {
                        conn.Open();
                        return Tuple.Create(true, connStrings);
                    } catch(SqlException ex) {
                        return Tuple.Create(false, ex.Message);
                    } finally { conn.Close(); }
                }
            }
        }
    }
}