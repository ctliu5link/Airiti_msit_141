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
            VM.ConnString = ConnString;
            VM.DASNTList = DASNT.GetTables(ConnString);
            VM.DASNCList = DASNC.GetTables(ConnString);
            return VM;
        }
        public SchemaViewModel SchemaDetails(String ConnString, String TableName)
        {
            SchemaViewModel VM = new SchemaViewModel();
            DA_SchemaNotesTable DASNT = new DA_SchemaNotesTable();
            DA_SchemaNotesColumn DASNC = new DA_SchemaNotesColumn();
            VM.ConnString = ConnString;
            VM.DASNTList = DASNT.GetTables(ConnString).Where(table => table.TableName == TableName).ToList();
            VM.DASNCList = DASNC.GetTables(ConnString).Where(column => column.TableName == TableName).ToList();
            return VM;
        }
        public SchemaViewModel SchemaDetails(SearchViewModel searchModel) {
            SchemaViewModel VM = new SchemaViewModel();
            List<string> listTable = new List<string>();
            List<DO_SchemaNotesTable> DOSNT = new List<DO_SchemaNotesTable>();
            List<DO_SchemaNotesColumn> DOSNC = new List<DO_SchemaNotesColumn>();
            var q = SchemaDetails(searchModel.ConnString).DASNCList.Where(x => x.ColumnName.ToUpper() == searchModel.ColumnName).ToList();
            foreach (var item in q)
            {
                listTable.Add(item.TableName);
                DOSNT.Add(SchemaDetails(searchModel.ConnString).DASNTList.Where(x => x.TableName == item.TableName).First());
                DOSNC.AddRange(SchemaDetails(searchModel.ConnString).DASNCList.Where(x => x.TableName == item.TableName).ToList().Distinct()); ;
            }
            VM.ConnString = searchModel.ConnString;
            VM.DASNTList = DOSNT;
            VM.DASNCList = DOSNC;
            return VM;
        }

        public void SchemaEdit(SchemaViewModel vModel)
        {
            using (SqlConnection conn = new SqlConnection(vModel.ConnString))
            {
                List<string> list = new List<string>();
                list.Add("MS_DESCRIPTION");
                list.Add("REMARK");
                string _prop = "", _value = "";
                foreach (var q in vModel.DASNTList)
                {
                    #region add,upate and drop extendedProperty of Table
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] == "REMARK")
                        {
                            if (string.IsNullOrWhiteSpace(q.TableRemark) || q.TableRemark == "Null")
                            {

                                SchemaDropTable(vModel.ConnString, q.TableName, list[i]);
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(q.TableRemark))
                                    _prop = "REMARK"; _value = q.TableRemark;
                                SchemaUpdateOrAddTable(vModel.ConnString, q.TableName, _prop, _value);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(q.TableMSDescription) || q.TableMSDescription == "Null")
                            {

                                SchemaDropTable(vModel.ConnString, q.TableName, list[i]);
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(q.TableMSDescription))
                                    _prop = "MS_DESCRIPTION"; _value = q.TableMSDescription;
                                SchemaUpdateOrAddTable(vModel.ConnString, q.TableName, _prop, _value);
                            }
                        }
                    }
                    conn.Close();
                    #endregion
                    #region add,upate and drop extendedProperty of Column
                     int count = 1;
                    foreach (var q1 in vModel.DASNCList) {
                      
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] == "REMARK")
                        {
                            if (string.IsNullOrWhiteSpace(q1.ColumnRemark) || q1.ColumnRemark == "Null")
                            {
                                    SchemaDropColumn(vModel.ConnString, q.TableName, q1.ColumnName, list[i],count);
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(q1.ColumnRemark))
                                    _prop = "REMARK"; _value = q1.ColumnRemark;
                                SchemaUpdateOrAddColumn(vModel.ConnString, q.TableName,q1.ColumnName, _prop, _value,count);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(q1.ColumnMSDescription) || q1.ColumnMSDescription == "Null")
                            {
                                    SchemaDropColumn(vModel.ConnString, q.TableName, q1.ColumnName, list[i],count);
                                }
                                else
                            {
                                if (!string.IsNullOrWhiteSpace(q1.ColumnMSDescription))
                                    _prop = "MS_DESCRIPTION"; _value = q1.ColumnMSDescription;
                                    SchemaUpdateOrAddColumn(vModel.ConnString, q.TableName, q1.ColumnName, _prop, _value,count);
                                }
                            }
                    }
                        count++;
                        conn.Close();
                    }
                    #endregion
                }
            }
        }
        public void SchemaUpdateOrAddTable(string connectionString, string TableName, string prop, string value)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryString = $@"IF EXISTS(SELECT * FROM sys.extended_properties ex
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
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public void SchemaDropTable(string connectionString, string TableName, string prop)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryString = $@"IF EXISTS(SELECT * FROM sys.extended_properties ex
              LEFT JOIN sys.all_objects  ob   on ex.major_id=ob.object_id
               WHERE ob.name='{TableName}' AND ex.name='{prop}' and minor_id=0)
                 EXEC sp_dropextendedproperty     
              @name = N'{prop}'  
             ,@level0type = N'Schema', @level0name = dbo  
              ,@level1type = N'Table',  @level1name = {TableName};";
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public void SchemaUpdateOrAddColumn(string connectionString, string TableName, String ColumnName, string prop, string value, int minorId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryString = $@"IF EXISTS(SELECT * FROM sys.extended_properties ex
              LEFT JOIN sys.all_objects  ob   on ex.major_id=ob.object_id
               WHERE ob.name='{TableName}' AND ex.name='{prop}' and minor_id={minorId})
              EXEC sp_updateextendedproperty     
              @name = N'{prop}'  ,@value = '{value}'  
             ,@level0type = N'Schema', @level0name = dbo  
              ,@level1type = N'Table',  @level1name = {TableName}
              ,@level2type = N'Column',  @level2name = {ColumnName}

                ELSE EXEC sp_addextendedproperty
             @name = N'{prop}'  ,@value = '{value}'  
             ,@level0type = N'Schema', @level0name = dbo  
              ,@level1type = N'Table',  @level1name = {TableName}
              ,@level2type = N'Column',  @level2name = {ColumnName};";

                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.ExecuteNonQuery();
            }


        }
        public void SchemaDropColumn(string connectionString, string TableName,String ColumnName, string prop,int minorId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryString = $@"IF EXISTS(SELECT * FROM sys.extended_properties ex
              LEFT JOIN sys.all_objects  ob   on ex.major_id=ob.object_id
               WHERE ob.name='{TableName}' AND ex.name='{prop}' and minor_id={minorId})
                 EXEC sp_dropextendedproperty     
              @name = N'{prop}'  
             ,@level0type = N'Schema', @level0name = dbo  
              ,@level1type = N'Table',  @level1name = {TableName}
              ,@level2type = N'Column',  @level2name = {ColumnName};";
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}