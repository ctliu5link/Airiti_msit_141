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
        public string 資料表 { get; set; }
        [DisplayName("欄位名稱")]
        public string 欄位名稱 { get; set; }
        [DisplayName("欄位說明")]
        public string 欄位說明 { get; set; }
        [DisplayName("資料型態")]
        public string 資料型態 { get; set; }
        [DisplayName("主鍵")]
        public string 主鍵 { get; set; }
        [DisplayName("不為Null")]
        public string 不為Null { get; set; }
        [DisplayName("預設值")]
        public string 預設值 { get; set; }
        [DisplayName("備註")]
        public string 備註 { get; set; }
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