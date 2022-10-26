using SchemaNote_11170__2_.Models.DataObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchemaNote_11170__2_.ViewModel
{
    public class VM_ShowData:IVM_ShowDataDetail
    {
        public string Connection { get; set; }
        public DO_TableDetail Table { get; set; }
        public DO_ColumnDetail Column { get; set; }
        public List<DO_TableDetail> TableDetail { get; set; }
        public List<DO_ColumnDetail> ColumnDetail { get; set; }
    }
    public class VM_ShowDataDetail: IVM_ShowDataDetail
    {
        public string Connection { get; set; }
        public List<DO_TableDetail> TableDetail { get; set; }
        public List<DO_ColumnDetail> ColumnDetail { get; set; }
    }

    public interface IVM_ShowDataDetail
    {
        string Connection { get; set; }
        List<DO_TableDetail> TableDetail { get; set; }
        List<DO_ColumnDetail> ColumnDetail { get; set; }
    }
}