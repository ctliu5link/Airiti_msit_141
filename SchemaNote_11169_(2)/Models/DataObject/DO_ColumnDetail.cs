using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNote_11169__2_.Models.DataObject
{
    public class DO_ColumnDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public string 主鍵 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string 欄位名稱 { get; set; }
        public string 欄位說明 { get; set; }
        public string 資料型態 { get; set; }
        public string 不為NULL { get; set; }
        public string 預設值 { get; set; }
        public string 備註 { get; set; }
        public string 資料表 { get; set; }
    }
}