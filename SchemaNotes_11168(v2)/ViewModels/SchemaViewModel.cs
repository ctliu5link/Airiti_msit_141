using SchemaNotes_11168_v2_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.ViewModels
{
    public class SchemaViewModel
    {
        public List<DO_SchemaNotesTable> DASNTList  { get; set; }
        public List<DO_SchemaNotesColumn> DASNCList { get; set; }
    }
}