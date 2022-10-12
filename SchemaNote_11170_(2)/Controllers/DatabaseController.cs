using SchemaNote_11170__2_.Models.DataAccess;
using SchemaNote_11170__2_.Models.DataObject;
using SchemaNote_11170__2_.Service;
using SchemaNote_11170__2_.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace SchemaNote_11170__2_.Controllers
{
    public class DatabaseController : Controller
    {
        // GET: Database
        public ActionResult Connect()
        {
            return View();
        }
        public ActionResult CheckConnect(DO_ConnectionString conn)
        {
            if ((new DA_ConnectionString()).isConnection(conn))
            {
                return RedirectToAction("ShowData", new { connection = conn.MixConnectionString() });
            }
            return RedirectToAction("Connect");
        }
        public ActionResult ShowData(string connection)
        {
            if (!string.IsNullOrEmpty(connection))
            {
                VM_ShowData vModel = new VM_ShowData();
                SV_ShowData SV = new SV_ShowData();
                vModel.TableDetail = SV.InsertTableDetail(connection);
                vModel.ColumnDetail = SV.InsertColumnDetail(connection);
                vModel.Connection= connection;
                return View(vModel);
            }
                return RedirectToAction("Connect");
        }
        public ActionResult DataAdapter()
        {
            return RedirectToAction("Connect");
        }

        public ActionResult Details(string name,string connection)
        {
            VM_ShowData vModel = new VM_ShowData();
            SV_ShowData SV = new SV_ShowData();
            vModel.TableDetail = SV.InsertTableDetail_ByTableName(connection, name);
            vModel.ColumnDetail = SV.InsertColumnDetail_ByTableName(connection, name);
            vModel.Connection = connection;
            return View(vModel);
        }
        [HttpPost]
        public ActionResult Details(VM_ShowData vModel)
        {
            string tableName = "";
            string tabletStruct = "";
            string columnName = "";
            string MS_Description = "";
            string REMARK = "";
            foreach (var table in vModel.TableDetail)
            {
                tableName = table.table_Name;
                tabletStruct = table.table_Struct;
                MS_Description = table.table_Explanation;
                REMARK = table.table_Description;
            }
            string sql_editTable1 =
                $"exec sp_updateextendedproperty " +
                $"@name = 'MS_Description', @value ='{MS_Description}' ," +
                $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                $"@level1type = 'TABLE',@level1name = '{tableName}'";
            string sql_editTable2 =
                $"exec sp_updateextendedproperty " +
                $"@name = 'MS_Description', @value ='{REMARK}' ," +
                $"@level0type = N'SCHEMA',@level0name = '{tabletStruct}'," +
                $"@level1type = 'TABLE',@level1name = '{tableName}'";
            try
            {
                SqlConnection connection = null;
                using (connection = new SqlConnection(vModel.Connection))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql_editTable1, connection);
                    command.ExecuteNonQuery();
                    SqlCommand command2 = new SqlCommand(sql_editTable2, connection);
                    command.ExecuteNonQuery();
                }
                connection.Dispose();   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return RedirectToAction("ShowData", new { connection = vModel.Connection });

            //foreach (var column in vModel.ColumnDetail)
            //{

            //}


            //string sql_addTable1= 
            //    $"exec sp_addextendedproperty " +
            //    $"@name = 'MS_Description', @value ={MS_Description} ," +
            //    $"@level0type = N'SCHEMA',@level0name = {tabletStruct}," +
            //    $"@level1type = 'TABLE',@level1name = {tableName}";
            //string sql_addTable2 =
            //    $"exec sp_addextendedproperty " +
            //    $"@name = 'MS_Description', @value ={REMARK} ," +
            //    $"@level0type = N'SCHEMA',@level0name = {tabletStruct}," +
            //    $"@level1type = 'TABLE',@level1name = {tableName}";


            //string sql_addColumn1 =
            //   $"exec sp_addextendedproperty " +
            //   $"@name = 'MS_Description', @value ={MS_Description} ," +
            //   $"@level0type = N'SCHEMA',@level0name = {tabletStruct}," +
            //   $"@level1type = 'TABLE',@level1name = {tableName}" +
            //   $"@level0type = N'SCHEMA',@level0name = {tabletType},";
            //string sql_addColumn2 =
            //    $"exec sp_addextendedproperty " +
            //    $"@name = 'MS_Description', @value ={REMARK} ," +
            //    $"@level0type = N'SCHEMA',@level0name = {tabletStruct}," +
            //    $"@level1type = 'TABLE',@level1name = {tableName}" +
            //    $"@level0type = N'SCHEMA',@level0name = {tabletType},";
            //string sql_editColumn1 =
            //   $"exec sp_updatextendedproperty " +
            //   $"@name = 'MS_Description', @value ={MS_Description} ," +
            //   $"@level0type = N'SCHEMA',@level0name = {tabletStruct}," +
            //   $"@level1type = 'TABLE',@level1name = {tableName}" +
            //   $"@level0type = N'SCHEMA',@level0name = {tabletType},";
            //string sql_editColumn2 =
            //    $"exec sp_updateextendedproperty " +
            //    $"@name = 'MS_Description', @value ={REMARK} ," +
            //    $"@level0type = N'SCHEMA',@level0name = {tabletStruct}," +
            //    $"@level1type = 'TABLE',@level1name = {tableName}" +
            //    $"@level0type = N'SCHEMA',@level0name = {tabletType},";
        }
    }
}