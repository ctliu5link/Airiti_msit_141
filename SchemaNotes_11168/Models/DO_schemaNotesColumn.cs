using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168.Models
{
    public class DO_schemaNotesColumn
    {
        [Display(Name = "物件名稱")]
        public string tableName { get; set; }
        [Display(Name = "欄位名稱")]
        public string columnName { get; set; }
        [Display(Name = "欄位說明")]
        public string columnMSDescription { get; set; }
        [Display(Name = "資料型態")]
        public string columnType { get; set; }
        [Display(Name = "主鍵")]
        public bool columnPrimaryKey { get; set; }
        [Display(Name = "不為null")]
        public string columnNull { get; set; }
        [Display(Name = "預設值")]
        public string columnDefault { get; set; }
        [Display(Name = "備註")]
        public string columnRemark { get; set; }
    }
}