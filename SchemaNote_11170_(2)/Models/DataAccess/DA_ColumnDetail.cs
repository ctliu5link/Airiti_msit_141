using SchemaNote_11170__2_.Models.DataObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace SchemaNote_11170__2_.Models.DataAccess
{
    public class DA_ColumnDetail
    {
        public List<DO_ColumnDetail> SearchColumnDetail(string connectionString)
        {
            #region   --SQL語法(所有Column)
            string sql_ColumnDetail = " SELECT distinct" +
                  "  ISC.TABLE_NAME as [資料表]," +
                  " SC.name AS[欄位名稱]," +
                  "  isnull(SE1.value,'null') AS[欄位說明]," +
                  " CASE ISNULL(CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH), ' ') WHEN ' ' THEN ISC.DATA_TYPE " +
                  " ELSE ISC.DATA_TYPE + '(' + ISNULL(CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH), ' ') + ')' END AS[資料型態]," +
                  " CASE WHEN ISK.CONSTRAINT_NAME IS NULL THEN 0 ELSE 1 END AS[主鍵]," +
                  " ISC.IS_NULLABLE AS[不為NULL]," +
                  " ISC.COLUMN_DEFAULT AS[預設值]," +
                  " isnull(SE.value,'null') AS[備註]" +
          " FROM sys.columns AS SC" +
                  " LEFT JOIN sys.objects SO ON SO.object_id = SC.object_id" +
                  " LEFT JOIN sys.extended_properties SE ON SE.minor_id = SC.column_id AND SE.major_id = SO.object_id AND SE.name = 'REMARK'" +
                  " LEFT JOIN sys.extended_properties SE1 ON SE1.minor_id = SC.column_id AND SE1.major_id = SO.object_id AND SE1.name = 'MS_Description'" +
                  " LEFT JOIN INFORMATION_SCHEMA.COLUMNS AS ISC ON ISC.COLUMN_NAME = SC.name AND ISC.TABLE_NAME = SO.name" +
                  " LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.TABLE_NAME = SO.name AND SC.name = ISK.COLUMN_NAME" +
          " WHERE OBJECT_NAME(SO.object_id) in (select TABLE_NAME from INFORMATION_SCHEMA.TABLES) ";
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
                            table.table_Name = dataReader["資料表"].ToString();
                            table.column_Name = dataReader["欄位名稱"].ToString();
                            table.column_Explanation = dataReader["欄位說明"].ToString();
                            table.column_DataType = dataReader["資料型態"].ToString();
                            table.column_PK = dataReader["主鍵"].ToString();
                            table.column_IsNotNull = dataReader["不為NULL"].ToString();
                            table.column_Default = dataReader["預設值"].ToString();
                            table.column_Description = dataReader["備註"].ToString();
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

            return list_column;
        }
    }
}