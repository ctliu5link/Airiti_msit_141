using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchemaNote_11170__2_.Models.DataObject
{
    public class DO_TableDetail
    {
        
        public string table_Name { get; set; }
        public string table_Explanation { get; set; }
        [DisplayName("物件類型：")]
        public string table_ObjectType { get; set; }
        [DisplayName("結構描述名稱：")]
        public string table_Struct { get; set; }
        [DisplayName("物件創建日期：")]
        public string table_CreateDate { get; set; }
        [DisplayName("物件修改日期：")]
        public string table_ModifyDate { get; set; }
        [DisplayName("備註")]
        public string table_Description { get; set; }
        [DisplayName("筆數：")]
        public string table_Count { get; set; }
    }
}