using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.Models.Repository.DataAccess
{
    public class DA_DBConnection
    {
        public string connStrings { get; set; }
        public DA_DBConnection(DO_DBconnection model  )
        {
            if (string.IsNullOrEmpty(model.uid) && string.IsNullOrEmpty(model.pwd) && string.IsNullOrEmpty(model.database) && string.IsNullOrEmpty(model.server))
            {
                connStrings = "New";
            }
            else if (string.IsNullOrEmpty(model.uid) || string.IsNullOrEmpty(model.pwd) || string.IsNullOrEmpty(model.database) || string.IsNullOrEmpty(model.server))
            {
                connStrings = "Error";

            }
            else
            {
                connStrings = $"uid={model.uid} ; pwd={model.pwd};database={model.database};server={model.server};";
            }
        }
    }
}