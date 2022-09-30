using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace SchemaNotes_11168_v2_.Models
{
   
    /// <summary>
    ///check the  required connectionStrings 
    /// </summary>
    public class DO_DBconnection
    {
        /// <summary>
     /// region fields for commponents of connectionstrings
     /// </summary>
        #region fields of components of connectionstrings
        public string uid { get; set; }
        public string pwd { get; set; }
        public string database { get; set; }
        public string server { get; set; }
        public string connStrings { get; set; }
        #endregion
        public string getConnstrings() {
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(database) || string.IsNullOrEmpty(server)) {
                return "";
            }
            connStrings = $"uid={uid} ; pwd={pwd};database={database};server={server};";
            return connStrings;
        }
    }
}