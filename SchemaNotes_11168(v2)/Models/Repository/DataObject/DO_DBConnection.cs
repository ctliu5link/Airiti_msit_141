using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel;

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
        [DisplayName("使用者ID")]
        public string uid { get; set; }
        [DisplayName("密碼")]
        public string pwd { get; set; }
        [DisplayName("資料庫名稱")]
        public string database { get; set; }
        [DisplayName("資料庫伺服器")]
        public string server { get; set; }
        }
       
 
}