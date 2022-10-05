using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchemaNote_11170__2_.Models.DataObject
{
    public class DO_ColumnDetail
    {
        [DisplayName("資料表名稱")]
        public string table_Name { get; set; }
        [DisplayName("欄位名稱")]
        public string column_Name { get; set; }
        [DisplayName("欄位說明")]
        public string column_Explanation { get; set; }
        [DisplayName("資料型態")]
        public string column_DataType { get; set; }
        [DisplayName("主鍵")]
        public string column_PK { get; set; }
        [DisplayName("不為Null")]
        public string column_IsNotNull { get; set; }
        [DisplayName("預設值")]
        public string column_Default{ get; set; }
        [DisplayName("備註")]
        public string column_Description { get; set; }
    }

    /// <summary>
    /// 判斷PK是否為主鍵
    /// </summary>
    public enum PK主鍵
    {
        YES = 0,
        NO =1,
    }
    /// <summary>
    /// 判斷欄位是否可為Null值
    /// </summary>
    public enum Null判斷
    {
        不為Null = 0,
        為Null =1,
    }

}