using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SchemaNotes_11168.Models
{

    public class DBconnection
    {
        public string uid { get; set; }
        public string pwd { get; set; }
        public string database { get; set; }
        public string IPAddress { get; set; }

        public Tuple<bool,string> IsSeverConnected(string uid, string pwd, string databse, string IPAddress)
        {
            string conStrings = $"uid={uid} ; pwd={pwd};database={database};server={IPAddress};";
            using (SqlConnection conn = new SqlConnection(conStrings))
            {
                try
                {
                    conn.Open();
                    return Tuple.Create(true, conStrings);
                }
                catch (SqlException ex)
                {
                    return Tuple.Create(false, ex.Message);
                }
                finally { conn.Close();}
            }
        }

    }
}