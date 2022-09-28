using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168.Models
{
    /// <summary>
    ///   class for fields of  SchemaNotes for column
    /// </summary>
    public class DO_schemaNotesColumn
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