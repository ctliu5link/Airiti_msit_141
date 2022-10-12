using SchemaNote_11169__2_.Models.DataAccess;
using SchemaNote_11169__2_.Models.DataObject;
using System;
using System.Collections.Generic;
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

        public ActionResult ShowView(string table,string sql)
        {
            DA_ShowView showView = new DA_ShowView();
            return View(showView.connectionViewModel(table, sql));
        }
    }
}