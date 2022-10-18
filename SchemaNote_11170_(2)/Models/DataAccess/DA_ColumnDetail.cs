//#define Level1
#define Level2
using SchemaNote_11170__2_.Models.DataObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using Airiti.Common;
using System.Data;
using System.Reflection;
using Airiti.DataAccess;
using Airiti.Extensions;

namespace SchemaNote_11170__2_.Models.DataAccess
{

    public class DA_ColumnDetail
    {
        /// <summary>
        /// 尋找所有的Column的ColumnDetail(所有)
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public List<DO_ColumnDetail> SearchColumnDetail(string connectionString)
        {
#if (Level1)
            #region   --SQL語法(所有Column)
            string sql_ColumnDetail = " SELECT distinct" +
                  " SC.column_id,"+
                  "  ISC.TABLE_NAME as [資料表]," +
                  " SC.name AS[欄位名稱]," +
                  "  isnull(SE1.value,'null') AS[欄位說明]," +
                  " CASE ISNULL(CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH), ' ') WHEN ' ' THEN ISC.DATA_TYPE " +
                  " ELSE ISC.DATA_TYPE + '(' + ISNULL(CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH), ' ') + ')' END AS[資料型態]," +
                  " CASE WHEN ISK.CONSTRAINT_NAME IS NULL THEN 'NO' ELSE 'YES' END AS[主鍵]," +
                  " CASE ISC.IS_NULLABLE WHEN 'YES' THEN '為Null' ELSE '不為Null' END  AS[不為NULL]," +
                  " ISC.COLUMN_DEFAULT AS[預設值]," +
                  " isnull(SE.value,'null') AS[備註]" +
          " FROM sys.columns AS SC" +
                  " LEFT JOIN sys.objects SO ON SO.object_id = SC.object_id" +
                  " LEFT JOIN sys.extended_properties SE ON SE.minor_id = SC.column_id AND SE.major_id = SO.object_id AND SE.name = 'REMARK'" +
                  " LEFT JOIN sys.extended_properties SE1 ON SE1.minor_id = SC.column_id AND SE1.major_id = SO.object_id AND SE1.name = 'MS_Description'" +
                  " LEFT JOIN INFORMATION_SCHEMA.COLUMNS AS ISC ON ISC.COLUMN_NAME = SC.name AND ISC.TABLE_NAME = SO.name" +
                  " LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.TABLE_NAME = SO.name AND SC.name = ISK.COLUMN_NAME" +
                  " WHERE OBJECT_NAME(SO.object_id) in (select TABLE_NAME from INFORMATION_SCHEMA.TABLES) "+
                  "ORDER BY SC.column_id;";
            #endregion
            List<DO_ColumnDetail> list_column = new List<DO_ColumnDetail>();
            #region  --連線，並找出所有column，存在list中
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql_ColumnDetail, connection);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            DO_ColumnDetail table = new DO_ColumnDetail();
                            table.資料表 = dataReader["資料表"].ToString();
                            table.欄位名稱 = dataReader["欄位名稱"].ToString();
                            table.欄位說明 = dataReader["欄位說明"].ToString();
                            table.資料型態 = dataReader["資料型態"].ToString();
                            table.主鍵 = dataReader["主鍵"].ToString();
                            table.不為Null = dataReader["不為NULL"].ToString();
                            table.預設值 = dataReader["預設值"].ToString();
                            table.備註 = dataReader["備註"].ToString();
                            list_column.Add(table);
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

#if (Level2)
            #region 必要宣告
            string strClassName = "SchemaNote_11170_DA_column";
            string strMethodName = MethodBase.GetCurrentMethod().Name;
            LogFile objLogFile = new LogFile(strClassName, strMethodName);
            objLogFile.StartLog();
            #endregion
            #region 額外宣告
            var objReturn = new ReturnObject<DataTable>();
            List<DO_ColumnDetail> list_column = new List<DO_ColumnDetail>();
            #endregion
            #region   --SQL語法(所有Column)
            string sql_ColumnDetail = " SELECT distinct" +
                  " SC.column_id," +
                  "  ISC.TABLE_NAME as [資料表]," +
                  " SC.name AS[欄位名稱]," +
                  "  isnull(SE1.value,'null') AS[欄位說明]," +
                  " CASE ISNULL(CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH), ' ') WHEN ' ' THEN ISC.DATA_TYPE " +
                  " ELSE ISC.DATA_TYPE + '(' + ISNULL(CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH), ' ') + ')' END AS[資料型態]," +
                  " CASE WHEN ISK.CONSTRAINT_NAME IS NULL THEN 'NO' ELSE 'YES' END AS[主鍵]," +
                  " CASE ISC.IS_NULLABLE WHEN 'YES' THEN '為Null' ELSE '不為Null' END  AS[不為NULL]," +
                  " ISC.COLUMN_DEFAULT AS[預設值]," +
                  " isnull(SE.value,'null') AS[備註]" +
          " FROM sys.columns AS SC" +
                  " LEFT JOIN sys.objects SO ON SO.object_id = SC.object_id" +
                  " LEFT JOIN sys.extended_properties SE ON SE.minor_id = SC.column_id AND SE.major_id = SO.object_id AND SE.name = 'REMARK'" +
                  " LEFT JOIN sys.extended_properties SE1 ON SE1.minor_id = SC.column_id AND SE1.major_id = SO.object_id AND SE1.name = 'MS_Description'" +
                  " LEFT JOIN INFORMATION_SCHEMA.COLUMNS AS ISC ON ISC.COLUMN_NAME = SC.name AND ISC.TABLE_NAME = SO.name" +
                  " LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.TABLE_NAME = SO.name AND SC.name = ISK.COLUMN_NAME" +
                  " WHERE OBJECT_NAME(SO.object_id) in (select TABLE_NAME from INFORMATION_SCHEMA.TABLES) " +
                  "ORDER BY SC.column_id;";
            #endregion
            #region  --連線，並找出所有column，存在list中
            try
            {
                ReturnObject<DataTable> column = DBService.SingleQuery(connectionString, sql_ColumnDetail);
                if (column.ReturnValue == OpReturnValue.Correct)
                {
                    objReturn.ReturnValue = OpReturnValue.Correct;
                    list_column = column.ReturnData.ToList<DO_ColumnDetail>().ToList();
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
            return list_column;
        }
        /// <summary>
        /// 根據TableName找尋相對應的ColumnDetail(單張)
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public List<DO_ColumnDetail> ColumnDetail_ByTableName(string connectionString,string tablename)
        {
#if Level1
            #region   --SQL語法(所有Column)
            string sql_ColumnDetail = " SELECT distinct " +
                  "SC.column_id,"+
                  "  ISC.TABLE_NAME as [資料表]," +
                  " SC.name AS[欄位名稱]," +
                  "  isnull(SE1.value,'null') AS[欄位說明]," +
                  " CASE ISNULL(CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH), ' ') WHEN ' ' THEN ISC.DATA_TYPE " +
                  " ELSE ISC.DATA_TYPE + '(' + ISNULL(CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH), ' ') + ')' END AS[資料型態]," +
                  " CASE WHEN ISK.CONSTRAINT_NAME IS NULL THEN 'NO' ELSE 'YES' END AS[主鍵]," +
                  " CASE ISC.IS_NULLABLE WHEN 'YES' THEN '為Null' ELSE '不為Null' END  AS[不為NULL]," +
                  " ISC.COLUMN_DEFAULT AS[預設值]," +
                  " isnull(SE.value,'null') AS[備註]" +
          " FROM sys.columns AS SC" +
                  " LEFT JOIN sys.objects SO ON SO.object_id = SC.object_id" +
                  " LEFT JOIN sys.extended_properties SE ON SE.minor_id = SC.column_id AND SE.major_id = SO.object_id AND SE.name = 'REMARK'" +
                  " LEFT JOIN sys.extended_properties SE1 ON SE1.minor_id = SC.column_id AND SE1.major_id = SO.object_id AND SE1.name = 'MS_Description'" +
                  " LEFT JOIN INFORMATION_SCHEMA.COLUMNS AS ISC ON ISC.COLUMN_NAME = SC.name AND ISC.TABLE_NAME = SO.name" +
                  " LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.TABLE_NAME = SO.name AND SC.name = ISK.COLUMN_NAME" +
                $" WHERE OBJECT_NAME(SO.object_id) = '{tablename}' "+
                  "ORDER BY SC.column_id;"; ;
            #endregion
            List<DO_ColumnDetail> list_column = new List<DO_ColumnDetail>();
            #region  --連線，並找出所有column，存在list中
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql_ColumnDetail, connection);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            DO_ColumnDetail table = new DO_ColumnDetail();
                            table.資料表 = dataReader["資料表"].ToString();
                            table.欄位名稱 = dataReader["欄位名稱"].ToString();
                            table.欄位說明 = dataReader["欄位說明"].ToString();
                            table.資料型態 = dataReader["資料型態"].ToString();
                            table.主鍵 = dataReader["主鍵"].ToString();
                            table.不為Null = dataReader["不為NULL"].ToString();
                            table.預設值  = dataReader["預設值"].ToString();
                            table.備註 = dataReader["備註"].ToString();
                            list_column.Add(table);
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
            string strClassName = "SchemaNote_11170_DA_column";
            string strMethodName = MethodBase.GetCurrentMethod().Name;
            LogFile objLogFile = new LogFile(strClassName, strMethodName);
            objLogFile.StartLog();
            #endregion
            #region 額外宣告
            var objReturn = new ReturnObject<DataTable>();
            List<DO_ColumnDetail> list_column = new List<DO_ColumnDetail>();
            #endregion
            #region   --SQL語法(所有Column)
            string sql_ColumnDetail = " SELECT distinct " +
                  "SC.column_id,"+
                  "  ISC.TABLE_NAME as [資料表]," +
                  " SC.name AS[欄位名稱]," +
                  "  isnull(SE1.value,'null') AS[欄位說明]," +
                  " CASE ISNULL(CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH), ' ') WHEN ' ' THEN ISC.DATA_TYPE " +
                  " ELSE ISC.DATA_TYPE + '(' + ISNULL(CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH), ' ') + ')' END AS[資料型態]," +
                  " CASE WHEN ISK.CONSTRAINT_NAME IS NULL THEN 'NO' ELSE 'YES' END AS[主鍵]," +
                  " CASE ISC.IS_NULLABLE WHEN 'YES' THEN '為Null' ELSE '不為Null' END  AS[不為NULL]," +
                  " ISC.COLUMN_DEFAULT AS[預設值]," +
                  " isnull(SE.value,'null') AS[備註]" +
          " FROM sys.columns AS SC" +
                  " LEFT JOIN sys.objects SO ON SO.object_id = SC.object_id" +
                  " LEFT JOIN sys.extended_properties SE ON SE.minor_id = SC.column_id AND SE.major_id = SO.object_id AND SE.name = 'REMARK'" +
                  " LEFT JOIN sys.extended_properties SE1 ON SE1.minor_id = SC.column_id AND SE1.major_id = SO.object_id AND SE1.name = 'MS_Description'" +
                  " LEFT JOIN INFORMATION_SCHEMA.COLUMNS AS ISC ON ISC.COLUMN_NAME = SC.name AND ISC.TABLE_NAME = SO.name" +
                  " LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.TABLE_NAME = SO.name AND SC.name = ISK.COLUMN_NAME" +
                $" WHERE OBJECT_NAME(SO.object_id) = '{tablename}' "+
                  "ORDER BY SC.column_id;"; ;
            #endregion
            #region  --連線，並找出所有column，存在list中
            try
            {
                ReturnObject<DataTable> column = DBService.SingleQuery(connectionString, sql_ColumnDetail);
                if (column.ReturnValue == OpReturnValue.Correct)
                {
                    objReturn.ReturnValue = OpReturnValue.Correct;
                    list_column = column.ReturnData.ToList<DO_ColumnDetail>().ToList();
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
            return list_column;
        }
        /// <summary>
        /// Column_新增：MS_Description
        /// </summary>
        /// <param name="MS_Description"></param>
        /// <param name="tabletStruct"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="connection"></param>
        public void AddColumn_MS_Description(string MS_Description, string tabletStruct, string tableName, string columnName, string connection)
        {
            string sql_addColumn =
                           $"exec sp_addextendedproperty " +
                           $"@name = 'MS_Description', @value ='{MS_Description}' ," +
                           $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                           $"@level1type = 'TABLE',@level1name = '{tableName}'," +
                           $"@level2type=N'COLUMN', @level2name = N'{columnName}';";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql_addColumn, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Column_新增：REMARK
        /// </summary>
        /// <param name="REMARK"></param>
        /// <param name="tabletStruct"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="connection"></param>
        public void AddColumn_REMARK(string REMARK, string tabletStruct, string tableName, string columnName, string connection)
        {
            string sql_addColumn =
                            $"exec sp_addextendedproperty " +
                            $"@name = 'REMARK', @value ='{REMARK}' ," +
                            $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                            $"@level1type = 'TABLE',@level1name = '{tableName}'," +
                            $"@level2type=N'COLUMN', @level2name = N'{columnName}'";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql_addColumn, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Column_修改：MS_Description
        /// </summary>
        /// <param name="MS_Description"></param>
        /// <param name="tabletStruct"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="connection"></param>
        public void UpdateColumn_MS_Description(string MS_Description, string tabletStruct, string tableName, string columnName, string connection)
        {
            string sql_updateColumn=
                            $"exec sp_updateextendedproperty " +
                            $"@name = 'MS_Description', @value ='{MS_Description}' ," +
                            $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                            $"@level1type = 'TABLE',@level1name = '{tableName}'," +
                            $"@level2type=N'COLUMN', @level2name = N'{columnName}'";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql_updateColumn, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Column_修改：REMARK
        /// </summary>
        /// <param name="REMARK"></param>
        /// <param name="tabletStruct"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="connection"></param>
        public void UpdateColumn_REMARK(string REMARK, string tabletStruct, string tableName, string columnName, string connection)
        {
            string sql_updateColumn =
                            $"exec sp_updateextendedproperty " +
                            $"@name = 'REMARK', @value ='{REMARK}' ," +
                            $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                            $"@level1type = 'TABLE',@level1name = '{tableName}'," +
                            $"@level2type = N'COLUMN',@level2name = '{columnName}'";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql_updateColumn, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Column_刪除：MS_Description
        /// </summary>
        /// <param name="tabletStruct"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="connection"></param>
        public void DropColumn_MS_Description(string tabletStruct, string tableName, string columnName, string connection)
        {
            string sql_dropColumn =
                          $"exec sp_dropextendedproperty " +
                          $"@name = 'MS_Description'," +
                          $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                          $"@level1type = 'TABLE',@level1name = '{tableName}'," +
                          $"@level2type = N'COLUMN',@level2name = '{columnName}'";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql_dropColumn, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Column_刪除：REMARK
        /// </summary>
        /// <param name="tabletStruct"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="connection"></param>
        public void DropColumn_REMARK(string tabletStruct, string tableName, string columnName, string connection)
        {
            string sql_dropColumn =
                            $"exec sp_dropextendedproperty " +
                            $"@name = 'REMARK'," +
                            $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                            $"@level1type = 'TABLE',@level1name = '{tableName}'," +
                            $"@level2type = N'COLUMN',@level2name = '{columnName}'";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql_dropColumn, conn);
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