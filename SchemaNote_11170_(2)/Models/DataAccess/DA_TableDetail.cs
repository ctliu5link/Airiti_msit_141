using SchemaNote_11170__2_.Models.DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace SchemaNote_11170__2_.Models.DataAccess
{
    public class DA_TableDetail
    {
        public List<DO_TableDetail> SearchTableDetail(string connectionString)
        {

            #region  --SQL語法(所有Table)
            string sql_TableDetail =
            "SELECT  " +
                    " T.TABLE_NAME AS [資料表名稱], " +
                    " isnull(e.value,'null') AS [資料說明], " +
                    " Case o.type when 'U' then '資料表' when 'S' then '基礎資料表' when 'V' then '檢視表' end as [物件類型]," +
                    " T.TABLE_SCHEMA AS [結構描述名稱], " +
                    " CONVERT(CHAR(10), o.create_date, 120) AS [Create_date]," +
                    " CONVERT(CHAR(10), o.modify_date, 120) AS [Modify_date]," +
                    " isnull(e1.value,'null') AS [備註], " +
                    " c.row_count as [筆數]" +
            " FROM INFORMATION_SCHEMA.TABLES t" +
                    " LEFT JOIN" +
                    " ( SELECT * FROM sys.objects o1 WHERE o1.type = 'U')  o ON t.TABLE_NAME = o.name" +
                    " LEFT JOIN" +
                    " (SELECT * FROM sys.extended_properties WHERE minor_id = 0 AND name = 'MS_Description')  AS e ON e.major_id = o.object_id " +
                    " LEFT JOIN" +
                    " (SELECT * FROM sys.extended_properties WHERE minor_id = 0 AND name = 'REMARK') AS e1 ON e1.major_id = o.object_id" +
                    " LEFT JOIN" +
                    "(SELECT OBJECT_NAME(object_id) as name, row_count FROM sys.dm_db_partition_stats WHERE OBJECT_NAME(object_id) in (select TABLE_NAME from INFORMATION_SCHEMA.TABLES)  AND (index_id < 2)) AS c on c.name = t.TABLE_NAME";
            #endregion

            List<DO_TableDetail> list_table = new List<DO_TableDetail>();

            #region  --連線，並找出所有table，存在list中
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql_TableDetail, connection);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            DO_TableDetail table = new DO_TableDetail();
                            table.table_Name = dataReader["資料表名稱"].ToString();
                            table.table_Explanation = dataReader["資料說明"].ToString();
                            table.table_ObjectType = dataReader["物件類型"].ToString();
                            table.table_Struct = dataReader["結構描述名稱"].ToString();
                            table.table_CreateDate = dataReader["Create_date"].ToString();
                            table.table_ModifyDate = dataReader["Modify_date"].ToString();
                            table.table_Description = dataReader["備註"].ToString();
                            table.table_Count = dataReader["筆數"].ToString();
                            list_table.Add(table);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion

            return list_table;
        }
        public List<DO_TableDetail> TableDetail_ByTableName(string connectionString,string tablename)
        {

            #region  --SQL語法(所有Table)
            string sql_TableDetail =
            "SELECT  " +
                    " T.TABLE_NAME AS [資料表名稱], " +
                    " isnull(e.value,'null') AS [資料說明], " +
                    " Case o.type when 'U' then '資料表' when 'S' then '基礎資料表' when 'V' then '檢視表' end as [物件類型]," +
                    " T.TABLE_SCHEMA AS [結構描述名稱], " +
                    " CONVERT(CHAR(10), o.create_date, 120) AS [Create_date]," +
                    " CONVERT(CHAR(10), o.modify_date, 120) AS [Modify_date]," +
                    " isnull(e1.value,'null') AS [備註], " +
                    " c.row_count as [筆數]" +
            " FROM INFORMATION_SCHEMA.TABLES t" +
                    " LEFT JOIN" +
                    " ( SELECT * FROM sys.objects o1 WHERE o1.type = 'U')  o ON t.TABLE_NAME = o.name" +
                    " LEFT JOIN" +
                    " (SELECT * FROM sys.extended_properties WHERE minor_id = 0 AND name = 'MS_Description')  AS e ON e.major_id = o.object_id " +
                    " LEFT JOIN" +
                    " (SELECT * FROM sys.extended_properties WHERE minor_id = 0 AND name = 'REMARK') AS e1 ON e1.major_id = o.object_id" +
                    " LEFT JOIN" +
                    "(SELECT OBJECT_NAME(object_id) as name, row_count FROM sys.dm_db_partition_stats WHERE OBJECT_NAME(object_id) in (select TABLE_NAME from INFORMATION_SCHEMA.TABLES)  AND (index_id < 2)) AS c on c.name = t.TABLE_NAME"+
                    $" WHERE OBJECT_NAME(o.object_id) = '{tablename}' ";
            #endregion

            List<DO_TableDetail> list_table = new List<DO_TableDetail>();

            #region  --連線，並找出所有table，存在list中
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql_TableDetail, connection);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            DO_TableDetail table = new DO_TableDetail();
                            table.table_Name = dataReader["資料表名稱"].ToString();
                            table.table_Explanation = dataReader["資料說明"].ToString();
                            table.table_ObjectType = dataReader["物件類型"].ToString();
                            table.table_Struct = dataReader["結構描述名稱"].ToString();
                            table.table_CreateDate = dataReader["Create_date"].ToString();
                            table.table_ModifyDate = dataReader["Modify_date"].ToString();
                            table.table_Description = dataReader["備註"].ToString();
                            table.table_Count = dataReader["筆數"].ToString();
                            list_table.Add(table);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion

            return list_table;
        }
    }
}