using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNote_11170__2_.Models.DataObject
{
    public class DO_ColumnDetail
    {
        public string table_Name { get; set; }
        public string column_Name { get; set; }
        public string column_Explanation { get; set; }
        public string column_DataType { get; set; }
        public string column_PK { get; set; }
        public string column_IsNotNull { get; set; }
        public string column_Default{ get; set; }
        public string column_Description { get; set; }
    }
}