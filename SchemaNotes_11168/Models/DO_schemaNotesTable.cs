using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchemaNotes_11168.Models
{
    public class DO_schemaNotesTable
    {
        [Display(Name = "物件名稱")]
        public string tableName { get; set; }
        [Display(Name = "物件說明")]
        public string tableMSDescription { get; set; }
        [Display(Name = "物件類型")]
        public string tableType { get; set; }
        [Display(Name = "結構描述名稱")]
        public string tableStruct { get; set; }
        [Display(Name = "物件創建日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public DateTime tableCreateTime { get; set; }
        [Display(Name = "物件修改日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public DateTime tableModifiedTime { get; set; }
        [Display(Name = "備註")]
        public string tableRemark { get; set; }
        [Display(Name = "資料筆數")]
        public int tableRows { get; set; }
    }
}