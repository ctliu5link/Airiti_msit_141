#define Level1
//#define Level2
using Airiti.Common;
using Airiti.DataAccess;
using Airiti.Extensions;
using SchemaNote_11170__2_.Models.DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Windows.Forms;

namespace SchemaNote_11170__2_.Models.DataAccess
{
    public class DA_TableDetail
    {
        /// <summary>
        /// 尋找所有的Table的TableDetail(所有)
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public List<DO_TableDetail> SearchTableDetail(string connectionString)
        {
            #if Level1
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
                            table.資料表名稱 = dataReader["資料表名稱"].ToString();
                            table.資料說明 = dataReader["資料說明"].ToString();
                            table.物件類型 = dataReader["物件類型"].ToString();
                            table.結構描述名稱 = dataReader["結構描述名稱"].ToString();
                            table.Create_date = dataReader["Create_date"].ToString();
                            table.Modify_date = dataReader["Modify_date"].ToString();
                            table.備註 = dataReader["備註"].ToString();
                            table.筆數 = int.Parse(dataReader["筆數"].ToString());
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
            #endif

            #if Level2
            #region 必要宣告
            string strClassName = "SchemaNote_11170_DA_table";
            string strMethodName = MethodBase.GetCurrentMethod().Name;
            LogFile objLogFile = new LogFile(strClassName, strMethodName);
            objLogFile.StartLog();
            #endregion
            #region 額外宣告
            var objReturn = new ReturnObject<DataTable>();
            List<DO_TableDetail> list_table = new List<DO_TableDetail>();
            #endregion
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
            #region  --連線，並找出所有table，存在list中
            try
            {
                ReturnObject<DataTable> table = DBService.SingleQuery(connectionString, sql_TableDetail);
                if (table.ReturnValue == OpReturnValue.Correct)
                {
                    objReturn.ReturnValue = OpReturnValue.Correct;
                    list_table = table.ReturnData.ToList<DO_TableDetail>().ToList();
                }
            }
            catch (Exception ex)
            {
                #region --植入錯誤訊息
                objReturn.ReturnValue = OpReturnValue.Exception;
                objReturn.ReturnMessage = ex.Message;
                objReturn.ReturnStackTrace = ex.StackTrace;
                objReturn.ReturnErrNum = OpErrNum.Exception;

                objLogFile.Log(string.Format("ReturnValue:{0}", OpReturnValue.Exception));
                objLogFile.Log(string.Format("ReturnMessage:{0}", ex.Message));
                objLogFile.Log(string.Format("ReturnStackTrace:{0}", ex.StackTrace));
                if (ex.InnerException != null)
                {
                    objLogFile.Log(string.Format("ReturnInnerMessage:{0}", ex.InnerException.Message));
                    objLogFile.Log(string.Format("ReturnInnerStackTrace:{0}", ex.InnerException.StackTrace));
                }
                #endregion
            }
            finally
            {
                objLogFile.EndLog();
            }
            #endregion
            #endif
            return list_table;
        }
        /// <summary>
        /// 根據TableName找尋相對應的TableDetail(單張)
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public List<DO_TableDetail> TableDetail_ByTableName(string connectionString,string tablename)
        {
#if Level1
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
                            table.資料表名稱 = dataReader["資料表名稱"].ToString();
                            table.資料說明 = dataReader["資料說明"].ToString();
                            table.物件類型 = dataReader["物件類型"].ToString();
                            table.結構描述名稱 = dataReader["結構描述名稱"].ToString();
                            table.Create_date = dataReader["Create_date"].ToString();
                            table.Modify_date = dataReader["Modify_date"].ToString();
                            table.備註 = dataReader["備註"].ToString();
                            table.筆數 = int.Parse(dataReader["筆數"].ToString());
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
#endif

#if Level2
            #region 必要宣告
            string strClassName = "SchemaNote_11170_DA_table";
            string strMethodName = MethodBase.GetCurrentMethod().Name;
            LogFile objLogFile = new LogFile(strClassName, strMethodName);
            objLogFile.StartLog();
            #endregion
            #region 額外宣告
            var objReturn = new ReturnObject<DataTable>();
            List<DO_TableDetail> list_table = new List<DO_TableDetail>();
            #endregion
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
            #region  --連線，並找出所有table，存在list中
            try
            {
                ReturnObject<DataTable> table = DBService.SingleQuery(connectionString, sql_TableDetail);
                if (table.ReturnValue == OpReturnValue.Correct)
                {
                    objReturn.ReturnValue = OpReturnValue.Correct;
                    list_table = table.ReturnData.ToList<DO_TableDetail>().ToList();
                }
            }
            catch (Exception ex)
            {
                #region --植入錯誤訊息
                objReturn.ReturnValue = OpReturnValue.Exception;
                objReturn.ReturnMessage = ex.Message;
                objReturn.ReturnStackTrace = ex.StackTrace;
                objReturn.ReturnErrNum = OpErrNum.Exception;

                objLogFile.Log(string.Format("ReturnValue:{0}", OpReturnValue.Exception));
                objLogFile.Log(string.Format("ReturnMessage:{0}", ex.Message));
                objLogFile.Log(string.Format("ReturnStackTrace:{0}", ex.StackTrace));
                if (ex.InnerException != null)
                {
                    objLogFile.Log(string.Format("ReturnInnerMessage:{0}", ex.InnerException.Message));
                    objLogFile.Log(string.Format("ReturnInnerStackTrace:{0}", ex.InnerException.StackTrace));
                }
                #endregion
            }
            finally
            {
                objLogFile.EndLog();
            }
            #endregion
#endif
            return list_table;
        }
        /// <summary>
        /// Table_新增：MS_Description
        /// </summary>
        /// <param name="MS_Description"></param>
        /// <param name="tabletStruct"></param>
        /// <param name="tableName"></param>
        /// <param name="connection"></param>
        public void AddTable_MS_Description(string MS_Description, string tabletStruct, string tableName,string connection)
        {
            string sql_addTable =
                $"exec sp_addextendedproperty " +
                $"@name = 'MS_Description', @value ='{MS_Description}' ," +
                $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                $"@level1type = 'TABLE',@level1name = '{tableName}';";
            try
            {
                using (SqlConnection conn=new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql_addTable, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Table_新增：REMARK
        /// </summary>
        /// <param name="REMARK"></param>
        /// <param name="tabletStruct"></param>
        /// <param name="tableName"></param>
        /// <param name="connection"></param>
        public void AddTable_REMARK(string REMARK, string tabletStruct, string tableName, string connection)
        {
            string sql_addTable =
                $"exec sp_addextendedproperty " +
                $"@name = 'REMARK', @value ='{REMARK}' ," +
                $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                $"@level1type = 'TABLE',@level1name = '{tableName}';";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql_addTable, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Table_修改：MS_Description
        /// </summary>
        /// <param name="MS_Description"></param>
        /// <param name="tabletStruct"></param>
        /// <param name="tableName"></param>
        /// <param name="connection"></param>
        public void UpdateTable_MS_Description(string MS_Description, string tabletStruct, string tableName, string connection)
        {
            string sql_updateTable =
                $"exec sp_updateextendedproperty " +
                $"@name = 'MS_Description', @value ='{MS_Description}' ," +
                $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                $"@level1type = 'TABLE',@level1name = '{tableName}';";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql_updateTable, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Table_修改：REMARK
        /// </summary>
        /// <param name="REMARK"></param>
        /// <param name="tabletStruct"></param>
        /// <param name="tableName"></param>
        /// <param name="connection"></param>
        public void UpdateTable_REMARK(string REMARK, string tabletStruct, string tableName, string connection)
        {
            string sql_updateTable =
                $"exec sp_updateextendedproperty " +
                $"@name = 'REMARK', @value ='{REMARK}' ," +
                $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                $"@level1type = 'TABLE',@level1name = '{tableName}';";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql_updateTable, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Table_刪除：MS_Description
        /// </summary>
        /// <param name="tabletStruct"></param>
        /// <param name="tableName"></param>
        /// <param name="connection"></param>
        public void DropTable_MS_Description(string tabletStruct, string tableName, string connection)
        {
            string sql_dropTable =
               $"exec sp_dropextendedproperty " +
               $"@name = 'MS_Description'," +
               $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
               $"@level1type = 'TABLE',@level1name = '{tableName}';";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql_dropTable, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Table_刪除：REMARK
        /// </summary>
        /// <param name="tabletStruct"></param>
        /// <param name="tableName"></param>
        /// <param name="connection"></param>
        public void DropTable_REMARK(string tabletStruct, string tableName, string connection)
        {
            string sql_dropTable =
                 $"exec sp_dropextendedproperty " +
                 $"@name = 'REMARK'," +
                 $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                 $"@level1type = 'TABLE',@level1name = '{tableName}';";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql_dropTable, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}