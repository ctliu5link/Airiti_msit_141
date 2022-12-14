using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNote_11169__2_.Models.DataObject
{
    public class DO_TableDetail
    {
        /// <summary>
        /// U => Table
        /// </summary>
        public string 物件類型 { get; set; }
        /// <summary>
        /// TABLE NAME
        /// </summary>
        public string 物件名稱 { get; set; }
        /// <summary>
        /// MS_Description
        /// </summary>
        public string 物件說明 { get; set; }
        /// <summary>
        /// REMARK
        /// </summary>
        public string 備註 { get; set; }
        public string 結構描述名稱 { get; set; }
        public string 物件創造日期 { get; set; }
        public string 物件修改日期 { get; set; }
        public string 總筆數 { get; set; }
    }
}