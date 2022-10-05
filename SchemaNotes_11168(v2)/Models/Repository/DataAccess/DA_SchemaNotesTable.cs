using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SchemaNotes_11168_v2_.Models;
using SchemaNotes_11168_v2_.Models.Repository.DataAccess.Base;
using SchemaNotes_11168_v2_.Models.Repository.DataAccess;

namespace SchemaNotes_11168_v2_.Models
{
    public class DA_SchemaNotesTable :DA_Base
    {
     List<DO_SchemaNotesTable> SNTList = new List<DO_SchemaNotesTable>();
        #region SqlQuery
        string commandText =
                 "SELECT" +
                 " SO.name AS [物件名稱]," +
                 " SE.value AS[物件說明],  " +
                 " CASE  WHEN SO.TYPE = 'U'  THEN '資料表'    ELSE '檢視表'  END AS[物件類型]," +
                 " SS.name AS[結構描述],  " +
                " CONVERT(VARCHAR(10), create_date, 120) AS[物件創建日期],   " +
                " CONVERT(VARCHAR(10), modify_date, 120) AS[物件修改日期],   " +
                " SE1.value AS[備註]," +
                " row_count AS[筆數]" +
                " FROM sys.objects AS SO  JOIN sys.schemas AS SS ON SO.schema_id = SS.schema_id  " +
                " LEFT JOIN sys.extended_properties AS SE ON SO.object_id = SE.major_id  AND SE.minor_id = 0  AND SE.name = 'MS_Description'" +
                " LEFT JOIN sys.extended_properties AS SE1 ON SO.object_id = SE1.major_id   AND SE1.minor_id = 0   AND SE1.name = 'REMARK'" +
                " LEFT JOIN sys.dm_db_partition_stats AS SD ON SO.object_id = SD.object_id  AND (index_id < 2)" +
                " WHERE OBJECT_NAME(SO.object_id)  IN (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES);";
        #endregion
        public List<DO_SchemaNotesTable> GetTables(DA_DBConnection model)      {
                using (SqlConnection conn = new SqlConnection(model.connStrings))
                {
                    SqlCommand command = new SqlCommand(commandText, conn); 
                     conn.Open();
                    #region  add all queried result to List  and return the list
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                     DO_SchemaNotesTable DOSNT = new DO_SchemaNotesTable
                {
                            tableName = dataReader["物件名稱"].ToString(),
                            tableMSDescription = dataReader["物件說明"].ToString(),
                            tableType = dataReader["物件類型"].ToString(),
                            tableStruct = dataReader["結構描述"].ToString(),
                            tableCreateTime = Convert.ToDateTime(dataReader["物件創建日期"]),
                            tableModifiedTime = Convert.ToDateTime(dataReader["物件修改日期"]),
                            tableRemark = dataReader["備註"].ToString(),
                            tableRows = int.Parse(dataReader["筆數"].ToString())
                        };
                        SNTList.Add(DOSNT);
                    }
                    return SNTList;

                    #endregion 
                }
        }
    }
}