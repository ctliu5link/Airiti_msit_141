using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.ViewModels
{
    public class SearchViewModel
    {
        public string ConnString { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string KeyWord { get; set; }
    }
}