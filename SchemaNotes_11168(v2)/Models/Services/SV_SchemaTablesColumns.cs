using SchemaNotes_11168_v2_.Models.Repository.DataAccess;
using SchemaNotes_11168_v2_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SchemaNotes_11168_v2_.Models.Services
{
    public class SV_SchemaTablesColumns
    {
        public SchemaViewModel SchemaDetails(String ConnString)
        {
            SchemaViewModel VM = new SchemaViewModel();
            DA_SchemaNotesTable DASNT = new DA_SchemaNotesTable();
            DA_SchemaNotesColumn DASNC = new DA_SchemaNotesColumn();
            VM.DASNTList = DASNT.GetTables(ConnString);
            VM.DASNCList = DASNC.GetTables(ConnString);
            return VM;
        }
        public SchemaViewModel SchemaDetails(String ConnString, String TableName)
        {
            SchemaViewModel VM = new SchemaViewModel();
            DA_SchemaNotesTable DASNT = new DA_SchemaNotesTable();
            DA_SchemaNotesColumn DASNC = new DA_SchemaNotesColumn();
            VM.DASNTList = DASNT.GetTables(ConnString).Where(table => table.TableName == TableName).ToList();
            VM.DASNCList = DASNC.GetTables(ConnString).Where(column => column.TableName == TableName).ToList();
            return VM;
        }

        public bool SchemaEdit( SchemaViewModel vModel)
        {
            bool val = false;
            using (SqlConnection conn = new SqlConnection(vModel.ConnString))
            {
                conn.Open();
                foreach (var q in vModel.DASNTList)
                {
                    string queryUpdateRemark = SchemaUpdateTable(q.TableName,"REMARK",q.TableRemark);
                    string queryUpdateMS = SchemaUpdateTable(q.TableName,"MS_DESCRIPTION" ,q.TableMSDescription);
                   SqlCommand cmd = new SqlCommand(queryUpdateMS, conn);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand(queryUpdateRemark, conn);
                    cmd2.ExecuteNonQuery();
                    int i = cmd.ExecuteNonQuery();
                    //foreach (var column in vModel.DASNCList) {
                    //    string queryUpdateRemarkC = SchemaUpdateColumn(q.TableName, column.ColumnName,"REMARK", column.ColumnRemark);
                    //    string queryUpdateMSC = SchemaUpdateColumn(q.TableName, column.ColumnName, "MS_DESCRIPTION", column.ColumnMSDescription);
                    //    SqlCommand cmd3 = new SqlCommand(queryUpdateMSC, conn);
                    //    cmd3.ExecuteNonQuery();
                    //    SqlCommand cmd4 = new SqlCommand(queryUpdateRemarkC, conn);
                    //    cmd4.ExecuteNonQuery();
                    //}
                    conn.Close();
                    val = (i>= 1) ? true : false;
                } 
                return val;
            }
        }

        public string SchemaUpdateTable(string TableName,string prop, string value)
        {

              string queryUpate = $@"EXEC sp_updateextendedproperty     
              @name = N'{prop}'  ,@value = '{value}'  
             ,@level0type = N'Schema', @level0name = dbo  
              ,@level1type = N'Table',  @level1name = {TableName};";
            return queryUpate;
        }

        public string SchemaUpdateColumn(string TableName,String ColumnName ,string prop, string value)
        {

            string queryUpate = $@"EXEC sp_updateextendedproperty     
              @name = N'{prop}'  ,@value = '{value}'  
             ,@level0type = N'Schema', @level0name = dbo  
              ,@level1type = N'Table',  @level1name = {TableName}
              ,@level2type=N'Column'@level2name={ColumnName};";
           return queryUpate;
        }


        //public string SchemaAdd(string TableName, string value) {
        //      string queryAdd = $@"EXEC sp_addextendedproperty    
        //      @name = N'REMARK'  ,@value = '{value}'  
        //     ,@level0type = N'Schema', @level0name = dbo  
        //      ,@level1type = N'Table',  @level1name = {TableName};";
        //    return queryAdd;
        //}

    }
}