using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchemaNote_11170__2_.Models.DataObject
{
    public class DO_TableDetail
    {
        public string 資料表名稱 { get; set; }
        public string 資料說明 { get; set; }
        [DisplayName("物件類型：")]
        public string 物件類型 { get; set; }
        [DisplayName("結構描述名稱：")]
        public string 結構描述名稱 { get; set; }
        [DisplayName("物件創建日期：")]
        public string Create_date { get; set; }
        [DisplayName("物件修改日期：")]
        public string Modify_date { get; set; }
        [DisplayName("備註")]
        public string 備註 { get; set; }
        [DisplayName("筆數：")]
        public int 筆數 { get; set; }
    }
}