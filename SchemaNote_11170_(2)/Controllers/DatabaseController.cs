using SchemaNote_11170__2_.Models.DataAccess;
using SchemaNote_11170__2_.Models.DataObject;
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
                DA_TableDetail table = new DA_TableDetail();
                DA_ColumnDetail column = new DA_ColumnDetail();

                Tuple<List<DO_TableDetail>, List<DO_ColumnDetail>> tupleMoel =
                    new Tuple<List<DO_TableDetail>, List<DO_ColumnDetail>>(table.SearchTableDetail(connection), column.SearchColumnDetail(connection));
                return View(tupleMoel);
            }
                return RedirectToAction("Connect");
        }

        public ActionResult DataAdapter()
        {
            return RedirectToAction("Connect");
        }

    }
}