using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNote_11170.Models
{
    public class DO_ColumnDetail
    {
        public string table_Name { get; set; }
        public string column_Name { get; set; }
        public string column_Explanation { get; set; }
        public string column_Type { get; set; }
        public string column_Key { get; set; }
        public string column_IsNull { get; set; }
        public string column_Default { get; set; }
        public string column_Description { get; set; }

    }
}