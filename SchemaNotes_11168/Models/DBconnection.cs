using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SchemaNotes_11168.Models
{

    /// <summary>
    /// class for DBconnection to check the  connectionStrings and state of connection
    /// </summary>
    public class DBconnection
    {/// <summary>
     /// region fields for commponents of connectionstrings
     /// </summary>
        #region fields of components of connectionstrings
        public string uid { get; set; }
        public string pwd { get; set; }
        public string database { get; set; }
        public string server { get; set; }
        #endregion
        /// <summary>
        ///Check the state of dbconnection via connectionstrings by tuple<bool, string>
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="databse"></param>
        /// <param name="IPAddress"></param>
        /// <returns>return the state of dbconnection , connectionstrings or error's message</returns>
        public Tuple<bool,string> IsSeverConnected()
        {
            string conStrings = $"uid={uid} ; pwd={pwd};database={database};server={server};";
            using (SqlConnection conn = new SqlConnection(conStrings))
            {
                #region  state of dbconnection is ok ,return true and  connectionstrings
                try
                {
                    conn.Open();
                    return Tuple.Create(true, conStrings);
                }
                #endregion
                #region dbconnection fail ,return false and error's message
                catch (SqlException ex)
                {
                    return Tuple.Create(false, ex.Message);
                }
                #endregion
                #region close dbconnection 
                finally { conn.Close();}
                #endregion
            }
        }

    }
}