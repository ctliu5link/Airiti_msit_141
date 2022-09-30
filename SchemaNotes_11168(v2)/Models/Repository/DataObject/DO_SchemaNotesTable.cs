using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.Models
{
    public class DO_SchemaNotesTable
    {
        #region fields of details of table for SchemaNotes
        public string tableName { get; set; }
        public string tableMSDescription { get; set; }
        public string tableType { get; set; }
        public string tableStruct { get; set; }
        public DateTime tableCreateTime { get; set; }
        public DateTime tableModifiedTime { get; set; }
        public string tableRemark { get; set; }
        public int tableRows { get; set; }
        #endregion
    }
}