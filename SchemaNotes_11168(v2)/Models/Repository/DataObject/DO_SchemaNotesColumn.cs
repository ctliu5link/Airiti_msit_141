using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.Models
{
    public class DO_SchemaNotesColumn
    {
        #region fields of details of column for SchemaNotes
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ColumnMSDescription { get; set; }
        public string ColumnType { get; set; }
        public string ColumnPrimaryKey { get; set; }
        public string ColumnNull { get; set; }
        public string ColumnDefault { get; set; }
        public string ColumnRemark { get; set; }
        #endregion
    }
}