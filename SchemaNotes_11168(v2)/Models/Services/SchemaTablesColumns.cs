using SchemaNotes_11168_v2_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.Models.Services
{
    public class SchemaTablesColumns
    {
        public SchemaViewModel SchemaDetails(DO_DBconnection model)
        {
            SchemaViewModel VM = new SchemaViewModel();
            DA_SchemaNotesTable DASNT = new DA_SchemaNotesTable();
            DA_SchemaNotesColumn DASNC = new DA_SchemaNotesColumn();
            VM.DASNTList=(DASNT.GetTables(model));
            VM.DASNCList=(DASNC.GetTables(model));
            return VM;
        }

        public List<object> SchemaTable(DO_DBconnection model)
        {
            List<object> SNT = new List<object>();
            DA_SchemaNotesTable DASNT = new DA_SchemaNotesTable();
            SNT.Add(DASNT.GetTables(model));
            return SNT;
        }
        //public List<object> SchemaColumn(DO_DBconnection model)
        //{
        //    List<object> SNC= new List<object>();
        //    DA_SchemaNotesColumn DASNC = new DA_SchemaNotesColumn();
        //    SNC.Add(DASNC.GetTables(model));
        //    return SNC;
        //}






    }
}