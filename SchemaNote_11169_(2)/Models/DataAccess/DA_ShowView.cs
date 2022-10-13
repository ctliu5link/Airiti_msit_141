using SchemaNote_11169__2_.Models.DataObject;
using SchemaNote_11169__2_.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace SchemaNote_11169__2_.Models.DataAccess
{
    public class DA_ShowView
    {
        public ConnectionViewModel connectionViewModel(string table,string sql)
        { 
        ConnectionViewModel connectionViewModel = new ConnectionViewModel();
        List<DO_ColumnDetail> DoColumn = new List<DO_ColumnDetail>();
        List<DO_TableDetail> DoTable = new List<DO_TableDetail>();
        try
        {
       SqlConnection cn = new SqlConnection(sql);
       cn.Open();
       SqlCommand command = new SqlCommand(@"SELECT ISC.TABLE_NAME AS [資料表], 
       SC.name AS[A.欄位名稱],
       SE1.value AS[B.欄位說明],
       ISC.DATA_TYPE + '(' + CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH) + ')' AS[C.資料型態],
       CASE
           WHEN ISK.CONSTRAINT_NAME IS NULL
           THEN 0
           ELSE 1
       END AS[D.主鍵],
       ISC.IS_NULLABLE AS[E.不為NULL],
       ISC.COLUMN_DEFAULT AS[F.預設值],
       SE.value AS[G.備註]
FROM sys.columns AS SC
     LEFT JOIN sys.objects SO ON SO.object_id = SC.object_id
     LEFT JOIN sys.extended_properties SE ON SE.minor_id = SC.column_id
                                             AND SE.major_id = SO.object_id
                                             AND SE.name = 'REMARK'
     LEFT JOIN sys.extended_properties SE1 ON SE1.minor_id = SC.column_id
                                              AND SE1.major_id = SO.object_id
                                              AND SE1.name = 'MS_Description'
     LEFT JOIN INFORMATION_SCHEMA.COLUMNS AS ISC ON ISC.COLUMN_NAME = SC.name
                                                    AND ISC.TABLE_NAME = SO.name
     LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.TABLE_NAME = SO.name
                                                             AND SC.name = ISK.COLUMN_NAME
WHERE OBJECT_NAME(SO.object_id) IN
(
    SELECT TABLE_NAME
    FROM INFORMATION_SCHEMA.TABLES
)
ORDER BY SC.column_id; ", cn);
        SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    DO_ColumnDetail a = new DO_ColumnDetail();
        a.不為NULL = $"{dataReader["E.不為NULL"]}";
                    a.備註 = $"{dataReader["G.備註"]}";
                    a.欄位名稱 = $"{dataReader["A.欄位名稱"]}";
                    a.欄位說明 = $"{dataReader["B.欄位說明"]}";
                    a.資料型態 = $"{dataReader["C.資料型態"]}";
                    a.預設值 = $"{dataReader["F.預設值"]}";
                    a.主鍵 = $"{dataReader["D.主鍵"]}";
                    a.資料表 = $"{dataReader["資料表"]}";
                    DoColumn.Add(a);
                }
    cn.Close();
                cn.Open();

                SqlCommand command2 = new SqlCommand(
@"SELECT CASE SO.type
           WHEN 'U'
           THEN '資料表'
       END AS [物件類型], 
       ISNULL(SEp1.value, 'null') AS [備註], 
       ISNULL(SEp.value, 'null') AS [物件說明], 
       IST.TABLE_SCHEMA AS [結構描述名稱], 
       IST.TABLE_NAME AS [物件名稱], 
       CONVERT(VARCHAR(10), SO.create_date, 120) AS [物件創造日期], 
       CONVERT(VARCHAR(10), SO.modify_date, 120) AS [物件修改日期], 
       st.Total_Rows AS [總筆數]
FROM INFORMATION_SCHEMA.TABLES AS IST
     LEFT JOIN
(
    SELECT *
    FROM sys.objects
) so ON IST.TABLE_NAME = so.name
     LEFT JOIN
(
    SELECT *
    FROM sys.extended_properties
    WHERE name = 'MS_Description'
          AND minor_id = '0'
) sep ON sep.major_id = so.object_id
     LEFT JOIN
(
    SELECT *
    FROM sys.extended_properties
    WHERE name = 'REMARK'
          AND minor_id = '0'
) sep1 ON sep1.major_id = so.object_id
     LEFT JOIN
(
    SELECT OBJECT_NAME(object_id) tablename, 
           Total_Rows = SUM(st.row_count)
    FROM sys.dm_db_partition_stats st
    WHERE OBJECT_NAME(object_id) IN
    (
        SELECT TABLE_NAME
        FROM INFORMATION_SCHEMA.TABLES
    )
          AND (index_id < 2)
    GROUP BY OBJECT_NAME(object_id)
) st ON st.tablename = so.name;", cn);


    SqlDataReader dataReader2 = command2.ExecuteReader();
                while (dataReader2.Read())
                {
                    DO_TableDetail a = new DO_TableDetail();
    a.物件類型 = $"{dataReader2["物件類型"]}";
                    a.備註 = $"{dataReader2["備註"]}";
                    a.物件說明 = $"{dataReader2["物件說明"]}";
                    a.結構描述名稱 = $"{dataReader2["結構描述名稱"]}";
                    a.物件名稱 = $"{dataReader2["物件名稱"]}";
                    a.物件創造日期 = $"{dataReader2["物件創造日期"]}";
                    a.物件修改日期 = $"{dataReader2["物件修改日期"]}";
                    a.總筆數 = $"{dataReader2["總筆數"]}";
                    DoTable.Add(a);
                }

cn.Close();
            }
            catch (Exception ex)
{
    MessageBox.Show(ex.Message);
}

ConnectionViewModel connectionView = new ConnectionViewModel();
connectionView.ColumnDetailListViewModel = DoColumn;
connectionView.TableDetailListViewModel = DoTable;
connectionView.ConnectionString = sql;
            connectionView.table = table;


return (connectionView);
           }
        }
    }
    