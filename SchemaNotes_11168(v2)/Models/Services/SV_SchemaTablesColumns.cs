using SchemaNotes_11168_v2_.Models.Repository.DataAccess;
using SchemaNotes_11168_v2_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.Models.Services
{
    public class SV_SchemaTablesColumns
    {
        public SchemaViewModel SchemaDetails(DA_DBConnection model )
        {
            SchemaViewModel VM = new SchemaViewModel();
            DA_SchemaNotesTable DASNT = new DA_SchemaNotesTable();
            DA_SchemaNotesColumn DASNC = new DA_SchemaNotesColumn();
            VM.DASNTList = DASNT.GetTables(model);
            VM.DASNCList=DASNC.GetTables(model);
            return VM;
        }
    }
}