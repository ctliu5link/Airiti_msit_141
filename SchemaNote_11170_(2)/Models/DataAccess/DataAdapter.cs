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
    public class DataAdapter
    {
        public List<DO_TableDetail> SearchAll(string connectionString)
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

            List<DO_TableDetail> list_table = new List<DO_TableDetail>();
            List<DO_ColumnDetail> list_column = new List<DO_ColumnDetail>();
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter adapter = new SqlDataAdapter(sql_TableDetail, connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter(sql_ColumnDetail, connection);
                DataSet dataset = new DataSet();
                DataSet dataset2 = new DataSet();
                adapter.Fill(dataset);
                adapter2.Fill(dataset2);
                foreach (DataRow datarow in dataset.Tables[0].Rows)
                {
                    DO_TableDetail table = new DO_TableDetail();
                    table.table_Name = datarow.Field<string>("資料表名稱");
                    table.table_Explanation = datarow.Field<string>("資料說明");
                    table.table_ObjectType = datarow.Field<string>("物件類型");
                    table.table_Struct = datarow.Field<string>("結構描述名稱");
                    table.table_CreateDate = datarow.Field<string>("Create_date");
                    table.table_ModifyDate = datarow.Field<string>("Modify_date");
                    table.table_Description = datarow.Field<string>("備註");
                    table.table_Count = datarow.Field<Int64>("筆數").ToString();
                    list_table.Add(table);
                }
                foreach (DataRow datarow in dataset2.Tables[0].Rows)
                {
                    DO_ColumnDetail table = new DO_ColumnDetail();
                    table.table_Name = datarow.Field<string>("資料表");
                    table.column_Name = datarow.Field<string>("欄位名稱");
                    table.column_Explanation = datarow.Field<string>("欄位說明");
                    table.column_DataType = datarow.Field<string>("資料型態");
                    table.column_PK = datarow.Field<string>("主鍵");
                    table.column_IsNotNull = datarow.Field<string>("不為NULL");
                    table.column_Default = datarow.Field<string>("預設值");
                    table.column_Description = datarow.Field<Int64>("備註").ToString();
                    list_column.Add(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return list_table;
        }
    }
}