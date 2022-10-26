//#define Level1
#define Level2
using Airiti.Common;
using Airiti.DataAccess;
using SchemaNote_11170__2_.Models.DataAccess;
using SchemaNote_11170__2_.Models.DataObject;
using SchemaNote_11170__2_.Service;
using SchemaNote_11170__2_.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace SchemaNote_11170__2_.Controllers
{
    public class DatabaseController : Controller
    {
        // GET: Database
        /// <summary>
        /// 首頁(輸入字串)
        /// </summary>
        /// <returns></returns>
        public ActionResult Connect()
        {
            return View();
        }
        /// <summary>
        /// 確認連線狀態
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public ActionResult CheckConnect(DO_ConnectionString conn)
        {
#if Level1
            DA_ConnectionString connect = new DA_ConnectionString();
            if (!connect.isConnection(conn))
            {
                return RedirectToAction("ShowData", new { connection = conn.MixConnectionString() });
            }
#endif

#if Level2
            string connectionStrings = conn.MixConnectionString();
            ReturnObject<string> objReturn = DBService.DBConnectionTest(connectionStrings);
            if (objReturn.ReturnValue == OpReturnValue.Correct)
            {
                return RedirectToAction("ShowData", new { connection = connectionStrings });
            }
            else
            {
                MessageBox.Show(objReturn.ReturnValue+"\n"+objReturn.ReturnMessage);
                return RedirectToAction("Connect");
            }
#endif
        }
        /// <summary>
        /// 展示所有DataDetail
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public ActionResult ShowData(string connection)
        {
#if Level1
            if (!string.IsNullOrEmpty(connection))
            {
                VM_ShowData vModel = new VM_ShowData();
                SV_ShowData SV = new SV_ShowData();
                vModel.TableDetail = SV.InsertTableDetail(connection);
                vModel.ColumnDetail= SV.InsertColumnDetail(connection);
                vModel.Connection= connection;
                return View(vModel);
            }
                return RedirectToAction("Connect");
#endif

#if Level2
            ReturnObject<string> objReturn= DBService.DBConnectionTest(connection);
            if (objReturn.ReturnValue==OpReturnValue.Correct)
            {
                VM_ShowData vModel = new VM_ShowData();
                SV_ShowData SV = new SV_ShowData();
                vModel.TableDetail = SV.InsertTableDetail(connection);
                vModel.ColumnDetail = SV.InsertColumnDetail(connection);
                vModel.Connection = connection;
                return View(vModel);
            }
            else
            {
                MessageBox.Show(objReturn.ReturnValue + "\n" + objReturn.ReturnMessage);
                return RedirectToAction("Connect");
            }
#endif
        }
        /// <summary>
        /// 根據TableName，展示此表的Detail
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public ActionResult Details(string name,string connection)
        {
#if Level1
if (!string.IsNullOrEmpty(connection))
            {
                VM_ShowData vModel = new VM_ShowData();
                SV_ShowData SV = new SV_ShowData();
                vModel.TableDetail = SV.InsertTableDetail_ByTableName(connection, name);
                vModel.ColumnDetail = SV.InsertColumnDetail_ByTableName(connection, name);
                vModel.Connection = connection;
                return View(vModel);
            }
            return RedirectToAction("Connect");
#endif
#if Level2
            ReturnObject<string> objReturn = DBService.DBConnectionTest(connection);
            if (objReturn.ReturnValue == OpReturnValue.Correct)
            {
                VM_ShowData vModel = new VM_ShowData();
                SV_ShowData SV = new SV_ShowData();
                vModel.TableDetail = SV.InsertTableDetail_ByTableName(connection, name);
                vModel.ColumnDetail = SV.InsertColumnDetail_ByTableName(connection, name);
                vModel.Connection = connection;
                return View(vModel);
            }
            return RedirectToAction("Connect");
#endif
        }
        /// <summary>
        /// 按下送出，進行Data的CRUD邏輯運算
        /// </summary>
        /// <param name="vModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Details(VM_ShowDataDetail vModel)
        {
            SV_ShowData SV = new SV_ShowData();
            SV.CRUD_DataDetail(vModel);
            return RedirectToAction("ShowData", new { connection = vModel.Connection });
        }
    }
}