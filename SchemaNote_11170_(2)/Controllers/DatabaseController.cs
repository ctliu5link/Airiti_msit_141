using SchemaNote_11170__2_.Models.DataAccess;
using SchemaNote_11170__2_.Models.DataObject;
using SchemaNote_11170__2_.Service;
using SchemaNote_11170__2_.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Details(string connection,string name)
        {
            VM_ShowData vModel = new VM_ShowData();
            SV_ShowData SV = new SV_ShowData();
            vModel.TableDetail = SV.InsertTableDetail_ByTableName(connection, name);
            vModel.ColumnDetail = SV.InsertColumnDetail_ByTableName(connection, name);
            return View(vModel);
        }
    }
}