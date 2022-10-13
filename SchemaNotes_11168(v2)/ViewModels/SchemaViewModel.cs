using SchemaNotes_11168_v2_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.ViewModels
{
    //public class ColumnExtend {
    //    public string TableName { get; set; }
    //    public string ColumnName { get; set; }
    //    public string ColumnMSDescription { get; set; }
    //    public string ColumnRemark { get; set; }
    //}
    public class SchemaViewModel
    {   public string ConnString { get; set; }
        public string TableName { get; set; }
        //public string TableMSDescription { get; set; }
        //public string TableRemark { get; set; }
        public List<DO_SchemaNotesTable> DASNTList  { get; set; }
        public List<DO_SchemaNotesColumn> DASNCList { get; set; }
        //public List<ColumnExtend> Extends { get; set; }
        //public string ColumnName { get; set; }
        //public string ColumnMSDescription { get; set; }
        //public string ColumnRemark { get; set; }
    }
}