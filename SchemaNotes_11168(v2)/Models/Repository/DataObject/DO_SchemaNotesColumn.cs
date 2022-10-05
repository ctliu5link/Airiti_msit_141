using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.Models
{
    public class DO_SchemaNotesColumn
    {
        #region fields of details of column for SchemaNotes
        public string tableName { get; set; }
        public string columnName { get; set; }
        public string columnMSDescription { get; set; }
        public string columnType { get; set; }
        public bool columnPrimaryKey { get; set; }
        public string columnNull { get; set; }
        public string columnDefault { get; set; }
        public string columnRemark { get; set; }
        #endregion
    }
}