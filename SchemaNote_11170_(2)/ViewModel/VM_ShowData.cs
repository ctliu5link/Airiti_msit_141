using SchemaNote_11170__2_.Models.DataObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchemaNote_11170__2_.ViewModel
{
    public class VM_ShowData
    {
        public List<DO_TableDetail> TableDetail { get; set; }
        public List<DO_ColumnDetail> ColumnDetail { get; set; }
        public DO_TableDetail Table{ get; set; }
        public DO_ColumnDetail Column { get; set; }
        public string Connection { get; set; }
    }
}