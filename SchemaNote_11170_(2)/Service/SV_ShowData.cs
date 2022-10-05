using SchemaNote_11170__2_.Models.DataAccess;
using SchemaNote_11170__2_.Models.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNote_11170__2_.Service
{
    public class SV_ShowData
    {
        public List<DO_TableDetail> InsertTableDetail(string connecStrings)
        {
            DA_TableDetail table = new DA_TableDetail();
            return table.SearchTableDetail(connecStrings);
        }
        public List<DO_ColumnDetail> InsertColumnDetail(string connecStrings)
        {
            DA_ColumnDetail column = new DA_ColumnDetail();
            return column.SearchColumnDetail(connecStrings);
        }

        public List<DO_TableDetail> InsertTableDetail_ByTableName(string connecStrings,string tablename)
        {
            DA_TableDetail table = new DA_TableDetail();
            return table.TableDetail_ByTableName(connecStrings,tablename);
        }
        public List<DO_ColumnDetail> InsertColumnDetail_ByTableName(string connecStrings, string tablename)
        {
            DA_ColumnDetail table = new DA_ColumnDetail();
            return table.ColumnDetail_ByTableName(connecStrings, tablename);
        }
    }

    
}