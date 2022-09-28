using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNote_11169.Models
{
    public class columnschema
    {
        public string 主鍵 { get; set; } 
        public string 欄位名稱 { get;set;}
        public string 欄位說明 { get; set;}
        public string 資料型態 { get; set; }
        public string 不為NULL { get; set; }
        public string 預設值 { get; set; }
        public string 備註 { get; set; }
        public string 資料表 { get; set; }

    }

    public class titalschema
    {
        public string 物件類型 { get; set; }
        public string 備註 { get; set; }
        public string 物件說明 { get; set; }
        public string 結構描述名稱 { get; set; }
        public string 物件名稱 { get; set; }
        public string 物件創造日期 { get; set; }
        public string 物件修改日期 { get; set; }
        public string 總筆數 { get; set; }
    }
}