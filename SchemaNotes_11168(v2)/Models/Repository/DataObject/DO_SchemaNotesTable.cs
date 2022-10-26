using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace SchemaNotes_11168_v2_.Models
{
    public class DO_SchemaNotesTable
    {
        #region fields of details of table for SchemaNotes
        [DisplayName("物件名稱")]
        public string TableName { get; set; }
        [DisplayName("物件說明")]
        public string TableMSDescription { get; set; }
        [DisplayName("物件類型")]
        public string TableType { get; set; }
        [DisplayName("結構描述")]
        public string TableStruct { get; set; }
        [DisplayName("物件創建日期")]
        public DateTime TableCreateTime { get; set; }
        [DisplayName("物件修改日期")]
        public DateTime TableModifiedTime { get; set; }
        [DisplayName("備註")]
        public string TableRemark { get; set; }
        [DisplayName("筆數")]
        public int TableRows { get; set; }
        #endregion
    }
}