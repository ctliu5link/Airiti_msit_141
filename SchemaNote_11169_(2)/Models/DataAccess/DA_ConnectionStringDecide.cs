using SchemaNote_11169__2_.Models.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace SchemaNote_11169__2_.Models.DataAccess
{
    public class DA_ConnectionStringDecide
    {
        public string Connection(DO_ConnectionString conn)
        {
            if (string.IsNullOrEmpty(conn.uid) || string.IsNullOrEmpty(conn.pwd) || string.IsNullOrEmpty(conn.database) || string.IsNullOrEmpty(conn.server))
            {
                string error = "失敗";
                return (error); 
            }
            string conecctionString = $"uid={conn.uid};pwd={conn.pwd};database={conn.database};server={conn.server}";
            return (conecctionString);
        }
    }
}