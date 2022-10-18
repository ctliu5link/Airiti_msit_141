using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.Models
{
    public class DO_SchemaNotesColumn
    {
        #region fields of details of column for SchemaNotes
        [DisplayName("物件名稱")]
        public string TableName { get; set; }
        [DisplayName("欄位名稱")]
        public string ColumnName { get; set; }
        [DisplayName("欄位說明")]
        public string ColumnMSDescription { get; set; }
        [DisplayName("資料型態")]
        public string ColumnType { get; set; }
        [DisplayName("主鍵")]
        public string ColumnPrimaryKey { get; set; }
        [DisplayName("不為NULL")]
        public string ColumnNull { get; set; }
        [DisplayName("物件名稱")]
        public string ColumnDefault { get; set; }
        [DisplayName("備註")]
        public string ColumnRemark { get; set; }
        #endregion
    }
}