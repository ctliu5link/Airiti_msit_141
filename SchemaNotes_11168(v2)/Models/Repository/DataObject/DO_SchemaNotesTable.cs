using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.Models
{
    public class DO_SchemaNotesTable
    {
        #region fields of details of table for SchemaNotes
        public string TableName { get; set; }
        public string TableMSDescription { get; set; }
        public string TableType { get; set; }
        public string TableStruct { get; set; }
        public DateTime TableCreateTime { get; set; }
        public DateTime TableModifiedTime { get; set; }
        public string TableRemark { get; set; }
        public int TableRows { get; set; }
        #endregion
    }
}