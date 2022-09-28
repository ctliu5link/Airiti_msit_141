using SchemaNote_11170.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace SchemaNote_11170.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        #region --全域連接字串
        public static string connectionString = null;
        #endregion

        public ActionResult About()
        {
            //宣告小容器：DO_TableDetail的list、DO_ColumnDetail的list
            List<DO_TableDetail> list1 = new List<DO_TableDetail>();
            List<DO_ColumnDetail> list2 = new List<DO_ColumnDetail>();

            //宣告大容器：Tuple包2個小容器
            Tuple<List<DO_TableDetail>, List<DO_ColumnDetail>> TupleModel = new Tuple<List<DO_TableDetail>, List<DO_ColumnDetail>>(list1, list2);

            #region  --SQL語法(所有Table)
            string test1 =
            "SELECT  " +
                    " T.TABLE_NAME AS [資料表名稱], " +
                    " isnull(e.value,'null') AS [資料說明], " +
                    " Case o.type when 'U' then '資料表' when 'S' then '基礎資料表' when 'V' then '檢視表' end as [物件類型]," +
                    " T.TABLE_SCHEMA AS [結構描述名稱], " +
                    " CONVERT(CHAR(10), o.create_date, 120) AS [CreateTime]," +
                    " CONVERT(CHAR(10), o.modify_date, 120) AS [Modify_date]," +
                    " isnull(e1.value,'null') AS [備註], " +
                    " c.row_count as [筆數]" +
            " FROM INFORMATION_SCHEMA.TABLES t" +
                    " LEFT JOIN" +
                    " ( SELECT * FROM sys.objects o1 WHERE o1.type = 'U') as o ON t.TABLE_NAME = o.name" +
                    " LEFT JOIN" +
                    " (SELECT * FROM sys.extended_properties WHERE minor_id = 0 AND name = 'MS_Description')  AS e ON e.major_id = o.object_id " +
                    " LEFT JOIN" +
                    " (SELECT * FROM sys.extended_properties WHERE minor_id = 0 AND name = 'REMARK') AS e1 ON e1.major_id = o.object_id" +
                    " LEFT JOIN" +
                    "(SELECT OBJECT_NAME(object_id) as name, row_count FROM sys.dm_db_partition_stats WHERE OBJECT_NAME(object_id) in (select TABLE_NAME from INFORMATION_SCHEMA.TABLES)  AND (index_id < 2)) AS c on c.name = t.TABLE_NAME";
            #endregion
            #region   --SQL語法(所有Column)
                string test2 = " SELECT distinct" +
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

            try
            {
                //1.可分兩次連線
                //2.可用一次連線，在command的SQL語法部分用test1 + test2，用datareader.NextResult()來轉移SQL語法


                //第一段：連線搜尋all tables
                #region  --搜尋all tables
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(test1, connection);

                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {

                        while (dataReader.Read())
                        {
                            DO_TableDetail tabeldata = new DO_TableDetail();
                            tabeldata.table_Name = dataReader["資料表名稱"].ToString();
                            tabeldata.table_Explanation = dataReader["資料說明"].ToString();
                            tabeldata.table_Data = dataReader["物件類型"].ToString();
                            tabeldata.table_Type = dataReader["結構描述名稱"].ToString();
                            tabeldata.table_CreateTime = dataReader["CreateTime"].ToString();
                            tabeldata.table_ModifyTime = dataReader["Modify_date"].ToString();
                            tabeldata.table_Description = dataReader["備註"].ToString();
                            tabeldata.table_Count = dataReader["筆數"].ToString();
                            list1.Add(tabeldata);
                            
                        }
                        //dataReader.NextResult();
                        //while (dataReader.Read())
                        //{
                        //    DO_ColumnDetail columndata = new DO_ColumnDetail();
                        //    columndata.table_Name = dataReader["資料表"].ToString();
                        //    columndata.column_Name = dataReader["欄位名稱"].ToString();
                        //    columndata.column_Explanation = dataReader["欄位說明"].ToString();
                        //    columndata.column_Type = dataReader["資料型態"].ToString();
                        //    columndata.column_Key = dataReader["主鍵"].ToString();
                        //    columndata.column_IsNull = dataReader["不為NULL"].ToString();
                        //    columndata.column_Default = dataReader["預設值"].ToString();
                        //    columndata.column_Description = dataReader["備註"].ToString();
                        //    list2.Add(columndata);
                        //}
                    }
                }
                #endregion
                //第二段：連線搜尋all columns
                #region  --搜尋all columns
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command2 = new SqlCommand(test2, connection);
                    SqlDataReader dataReader2 = command2.ExecuteReader();
                    if (dataReader2.HasRows)
                    {
                        while (dataReader2.Read())
                        {
                            DO_ColumnDetail columndata = new DO_ColumnDetail();
                            columndata.table_Name = dataReader2["資料表"].ToString();
                            columndata.column_Name = dataReader2["欄位名稱"].ToString();
                            columndata.column_Explanation = dataReader2["欄位說明"].ToString();
                            columndata.column_Type = dataReader2["資料型態"].ToString();
                            columndata.column_Key = dataReader2["主鍵"].ToString();
                            columndata.column_IsNull = dataReader2["不為NULL"].ToString();
                            columndata.column_Default = dataReader2["預設值"].ToString();
                            columndata.column_Description = dataReader2["備註"].ToString();
                            list2.Add(columndata);
                        }
                    }
                }
                #endregion
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return View(TupleModel);
        }

        public ActionResult Contact()
        {
                return View();
        }
        
        public ActionResult Connection()
        {
                return View();
        }
        public ActionResult BackConnection(DO_ConnectionStrings Connection)
        {
            SqlConnection conn = null;
            try
            {
                if (!string.IsNullOrEmpty(Connection.ConnectionStrings))
                {
                    conn = new SqlConnection(Connection.ConnectionStrings);
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        connectionString = Connection.ConnectionStrings;
                        return RedirectToAction("About");
                    }
                }
            }
            catch
            {
                MessageBox.Show($"連接錯誤：connectionString 有誤");
                return RedirectToAction("Connection");
            }
            return RedirectToAction("Connection");
        }
    }
}