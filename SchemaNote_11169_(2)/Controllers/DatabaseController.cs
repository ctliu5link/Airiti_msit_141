using SchemaNote_11169__2_.Models.DataAccess;
using SchemaNote_11169__2_.Models.DataObject;
using SchemaNote_11169__2_.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using System.Windows.Forms;


namespace SchemaNote_11169__2_.Controllers
{
    public class DatabaseController : Controller
    {
        // GET: Database
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Connection(DO_ConnectionString connect)
        {
            DA_ConnectionStringDecide connection = new DA_ConnectionStringDecide();

            if (connection.Connection(connect) == "失敗")
                return View();

            else
                return RedirectToAction("ConnectionString", new { sql = connection.Connection(connect) });
        }

        public ActionResult ConnectionString(string sql)
        {
            DA_ConnectionString connectionString = new DA_ConnectionString();
            return View(connectionString.ColumnDetails(sql));
        }
        /// <summary>
        /// 將點選的欄位的那張表整個呈現出來
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ActionResult ShowView(string table, string sql)
        {
            DA_ShowView showView = new DA_ShowView();
            return View(showView.connectionViewModel(table, sql));
        }

        /// <summary>
        /// 1.先取得修改後table的值
        /// 2.取得修改前table的值
        /// 3.將值跟未更改前比對，有更動再新刪修
        /// 4.先取得修改後column的值
        /// 5.取得修改前table的值
        /// 6.將值跟未更改前比對，有更動再新刪修
        /// 7.儲存，傳回View畫面
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>


        public ActionResult SaveSql(ConnectionViewModel model)
        {

            #region 取得table更改後的值
            string Input_Table_MS_Description = "";
            string Input_TableName = "";
            string Input_SCHEMA = "";
            string Input_Table_REMARK = "";
            string Sql_Table_MS_Description = "";
            string Sql_Table_REMARK = "";
            string Sql_Column_MS_Description = "";
            string Sql_Column_REMARK = "";
            

            DO_TableDetail do_table = model.TableDetailListViewModel.FirstOrDefault();
            Input_TableName = do_table.物件名稱;
            Input_Table_MS_Description = do_table.物件說明;
            Input_SCHEMA = do_table.結構描述名稱;
            Input_Table_REMARK = do_table.備註;
            #endregion

            #region 取得table更改前的值
            DA_ShowView showtable = new DA_ShowView();
            ConnectionViewModel showtotaltable = showtable.connectionViewModel(Input_TableName, model.ConnectionString);
            do_table = showtotaltable.TableDetailListViewModel.FirstOrDefault();
            #endregion

            #region 取得Column更改後的值
            List<DO_ColumnDetail> do_column = new List<DO_ColumnDetail>();
            foreach (DO_ColumnDetail column in model.ColumnDetailListViewModel)
            {
                do_column.Add(column);
            }
            #endregion

            #region 取得Column更改前的值
            DA_ShowView showcolumn = new DA_ShowView();
            ConnectionViewModel showtotalcolumn = showcolumn.connectionViewModel(Input_TableName, model.ConnectionString);
            List<DO_ColumnDetail> do_columns = showtotalcolumn.ColumnDetailListViewModel.ToList();
            #endregion

            #region 宣告column的變數  
            string Input_Column_MS_Description = "";
            string Input_Column_REMARK = "";
            string Column_Name = "";
            #endregion

            #region [column][欄位說明] 新刪修
            for (int i = 0; i < do_column.Count; i++)
            {
                Input_Column_MS_Description = do_column[i].欄位說明;
                Input_Column_REMARK = do_column[i].備註;
                Column_Name = do_column[i].欄位名稱;

                #region [Column][MS_Description]刪除
                if (string.IsNullOrEmpty(Input_Column_MS_Description))
                {
                    if ((do_columns[i].欄位說明) != "null")
                    {
                        Sql_Column_MS_Description =
                        $"EXEC sp_dropextendedproperty " +
                        $"@name=N'MS_Description'," +//todo
                        $"@level0type=N'SCHEMA', @level0name=N'{Input_SCHEMA}'," +
                        $"@level1type = N'TABLE', @level1name=N'{Input_TableName}'," +
                        $"@level2type=N'COLUMN', @level2name = N'{Column_Name}';";
                    }
                    else
                    {
                        //不做事
                    }
                }
                #endregion
                #region[Column][MS_Description]新增修改
                else
                {
                    if ((do_columns[i].欄位說明) == "null")
                    {
                        Sql_Column_MS_Description =
                        $"EXEC sp_addextendedproperty " +
                        $"@name = N'MS_Description', @value = N'{Input_Column_MS_Description}'," +
                        $"@level0type = N'SCHEMA',@level0name = N'{Input_SCHEMA}'," +
                        $"@level1type = N'TABLE', @level1name = N'{Input_TableName}'," +
                        $"@level2type=N'COLUMN', @level2name = N'{Column_Name}';";
                    }
                    else
                    {
                        Sql_Column_MS_Description =
                        $"EXEC sp_updateextendedproperty " +
                        $"@name=N'MS_Description',@value=N'{Input_Column_MS_Description}'," +
                        $"@level0type=N'SCHEMA', @level0name=N'{Input_SCHEMA}'," +
                        $"@level1type = N'TABLE', @level1name=N'{Input_TableName}',"+
                        $"@level2type=N'COLUMN', @level2name = N'{Column_Name}';";
                    }
                }
                #endregion
                #region column_MS_Description之sql更改資料庫
                try
                {
                    using (SqlConnection conn = new SqlConnection(model.ConnectionString))
                    {
                        conn.Open();
                        SqlCommand command1 = new SqlCommand(Sql_Column_MS_Description, conn);
                        command1.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }

            #endregion

            #region [table][物件說明] 新刪修
            if (string.IsNullOrEmpty(Input_Table_MS_Description))
            {
                #region 想要刪除 MS_Description
                if ((do_table.物件說明)!="null")
                {
                    Sql_Table_MS_Description =
                    $"EXEC sp_dropextendedproperty " +
                    $"@name=N'MS_Description'," +//todo
                    $"@level0type=N'SCHEMA', @level0name=N'{Input_SCHEMA}'," +
                    $"@level1type = N'TABLE', @level1name=N'{Input_TableName}'";
                }
                else 
                {
                    //不做事
                }
                #endregion
            }
            else //if (!string.IsNullOrEmpty(Input_MS_Description))
            {
                #region 想要新增或更新 MS_Description
                if ((do_table.物件說明)=="null")
                {
                    Sql_Table_MS_Description =
                    $"EXEC sp_addextendedproperty " +
                    $"@name = N'MS_Description', @value = N'{Input_Table_MS_Description}'," +
                    $"@level0type = N'SCHEMA',@level0name = N'{Input_SCHEMA}'," +
                    $"@level1type = N'TABLE', @level1name = N'{Input_TableName}'";
                }
                else
                {
                    Sql_Table_MS_Description =
                    $"EXEC sp_updateextendedproperty " +
                    $"@name=N'MS_Description',@value=N'{Input_Table_MS_Description}'," +
                    $"@level0type=N'SCHEMA', @level0name=N'{Input_SCHEMA}'," +
                    $"@level1type = N'TABLE', @level1name=N'{Input_TableName}'";
                }
                #endregion
            }
            #endregion

            #region [table][備註] 新刪修
            if (string.IsNullOrEmpty(Input_Table_REMARK))
            {
                #region 刪除備註
                if ((do_table.備註)!="null")
                {
                    Sql_Table_REMARK =
                    $"EXEC sys.sp_dropextendedproperty " +
                    $"@name=N'REMARK'," +//todo
                    $"@level0type=N'SCHEMA', @level0name=N'{Input_SCHEMA}'," +
                    $"@level1type = N'TABLE', @level1name=N'{Input_TableName}'";
                }
                else
                {
                    //不做任何事情
                }
                #endregion
            }
            else
            {
                #region 新增或更新[備註]
                if ((do_table.備註)=="null")
                {
                    Sql_Table_REMARK =
                        "EXEC sys.sp_addextendedproperty " +
                        $"@name = N'REMARK', @value = N'{Input_Table_REMARK}'," +
                        $"@level0type = N'SCHEMA',@level0name = N'{Input_SCHEMA}'," +
                        $"@level1type = N'TABLE', @level1name = N'{Input_TableName}'";
                }
                else
                {
                    Sql_Table_REMARK =
                        $"EXEC sys.sp_updateextendedproperty " +
                        $"@name=N'REMARK',@value=N'{Input_Table_REMARK}'," +
                        $"@level0type=N'SCHEMA', @level0name=N'{Input_SCHEMA}'," +
                        $"@level1type = N'TABLE', @level1name=N'{Input_TableName}'";
                }
                #endregion
            }
            #endregion



            #region table_MS_Description之sql更改資料庫
            try
            {
                using (SqlConnection conn = new SqlConnection(model.ConnectionString))
                {
                    conn.Open();
                    SqlCommand command1 = new SqlCommand(Sql_Table_MS_Description, conn);
                    command1.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion

            #region table_REMARK之sql更改資料庫
            try
            {
                using (SqlConnection conn = new SqlConnection(model.ConnectionString))
                {
                    conn.Open();
                    SqlCommand command1 = new SqlCommand(Sql_Table_REMARK, conn);
                    command1.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
            return RedirectToAction("ShowView", new { table = Input_TableName, sql = model.ConnectionString });
        }
    }
}
//if (b.備註 == REMARK)
//{
//    sql =
//    $"EXEC sp_updateextendedproperty " +
//    $"@name='MS_Description',@value=N'{REMARK}'," +
//    $"@level0type=N'SCHEMA', @level0name=N'{MS_Description}'," +
//    $"@level1type = N'TABLE', @level1name=N'{tableName}'";
//}
//if (b.備註 == null)
//{
//    sql =
//    $"EXEC sp_dropextendedproperty " +
//    $"@name='MS_Description',@value=N'{REMARK}'," +
//    $"@level0type=N'SCHEMA', @level0name=N'{MS_Description}'," +
//    $"@level1type = N'TABLE', @level1name=N'{tableName}'";
//}
//if (b.備註 == "")
//{
//    sql =
//    $"EXEC sp_dropextendedproperty " +
//    $"@name='MS_Description',@value=N'{REMARK}'," +
//    $"@level0type=N'SCHEMA', @level0name=N'{MS_Description}'," +
//    $"@level1type = N'TABLE', @level1name=N'{tableName}'";
//}
//if (b.備註 != REMARK)
//{
//    sql =
//    $"EXEC sp_updateextendedproperty " +
//    $"@name='MS_Description',@value=N'{REMARK}'," +
//    $"@level0type=N'SCHEMA', @level0name=N'{MS_Description}'," +
//    $"@level1type = N'TABLE', @level1name=N'{tableName}'";
//}