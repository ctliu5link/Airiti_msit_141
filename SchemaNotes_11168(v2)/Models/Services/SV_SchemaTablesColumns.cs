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
                    List<string> list = new List<string>();
                    list.Add("MS_DESCRIPTION");
                    list.Add("REMARK");
                    string _name = "", _value = "";
                foreach (var q in vModel.DASNTList)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        conn.Open();
                        if (list[i] == "REMARK")
                        {
                            if (string.IsNullOrWhiteSpace(q.TableRemark)) {

                                SchemaDropTable(vModel.ConnString, q.TableName, list[i]);
                            }
                            else {
                                if (!string.IsNullOrWhiteSpace(q.TableRemark))
                            _name = "REMARK"; _value = q.TableRemark;}
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(q.TableMSDescription))
                            {

                                SchemaDropTable(vModel.ConnString, q.TableName, list[i]);
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(q.TableMSDescription))
                                    _name = "REMARK"; _value = q.TableRemark;
                            }
                            _name = "MS_DESCRIPTION"; _value = q.TableMSDescription;
                            }
                        }

                        string queryUpdate = SchemaUpdateTable(q.TableName, _name, _value);
                        SqlCommand cmd = new SqlCommand(queryUpdate, conn);
                        cmd.ExecuteNonQuery();
                        int j = cmd.ExecuteNonQuery();
                        conn.Close();
                        val = (j >= 1) ? true : false;
                    }
                  
                } 
                return val;
            }
        }

        public string SchemaUpdateTable(string TableName,string prop, string value)
        {

              string queryUpate = $@"IF EXISTS(SELECT * FROM sys.extended_properties ex
              LEFT JOIN sys.all_objects  ob   on ex.major_id=ob.object_id
               WHERE ob.name='{TableName}' AND ex.name='{prop}' and minor_id=0)
              EXEC sp_updateextendedproperty     
              @name = N'{prop}'  ,@value = '{value}'  
             ,@level0type = N'Schema', @level0name = dbo  
              ,@level1type = N'Table',  @level1name = {TableName}
              EXEC sp_addextendedproperty
             @name = N'{prop}'  ,@value = '{value}'  
             ,@level0type = N'Schema', @level0name = dbo  
              ,@level1type = N'Table',  @level1name = {TableName};";
            return queryUpate;
        }

        public void SchemaDropTable(string connectionString, string TableName, string prop) {
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                string queryDrop = $@"IF EXISTS(SELECT * FROM sys.extended_properties ex
              LEFT JOIN sys.all_objects  ob   on ex.major_id=ob.object_id
               WHERE ob.name='{TableName}' AND ex.name='{prop}' and minor_id=0)
                 EXEC sp_dropextendedproperty     
              @name = N'{prop}'  
             ,@level0type = N'Schema', @level0name = dbo  
              ,@level1type = N'Table',  @level1name = {TableName};";
                SqlCommand cmd = new SqlCommand(queryDrop, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public string SchemaUpdateColumn(string TableName,String ColumnName ,string prop, string value)
        {
            string queryUpate = $@"IF EXISTS(SELECT * FROM sys.extended_properties ex
              LEFT JOIN sys.all_objects  ob   on ex.major_id=ob.object_id
               WHERE ob.name='{TableName}' AND ex.name='{prop}' and minor_id=0)
              EXEC sp_updateextendedproperty     
              @name = N'{prop}'  ,@value = '{value}'  
             ,@level0type = N'Schema', @level0name = dbo  
              ,@level1type = N'Table',  @level1name = {TableName}
              ELSE EXEC sp_addextendedproperty
             @name = N'{prop}'  ,@value = '{value}'  
             ,@level0type = N'Schema', @level0name = dbo  
              ,@level1type = N'Table',  @level1name = {TableName};";
            return queryUpate;
        }




    }
}