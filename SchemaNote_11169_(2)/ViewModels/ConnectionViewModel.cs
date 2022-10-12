using SchemaNote_11169__2_.Models.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNote_11169__2_.ViewModels
{
    public class ConnectionViewModel
    {
        public List<DO_ColumnDetail> ColumnDetailListViewModel { get; set; }
        public List<DO_TableDetail> TableDetailListViewModel { get; set; }
        public string ConnectionString { get; set; }
        public string table { get; set; }
        public List<DO_ColumnDetail> TableChangListViewModel { get; set; }
    }
}