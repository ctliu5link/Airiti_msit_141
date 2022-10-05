using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airiti.Common;
using SchemaNotes_11168_v1_.Models;
using System.Reflection;


namespace SchemaNotes_11168_v1_.Controllers
{
    public class SchemaNotesController : Controller
    {
        // GET: SchemaNOtes
        public static List<DO_DBconnection> DOdbcList = new List<DO_DBconnection>();
        public ActionResult DBConnection(DO_DBconnection model) {
            DA_DBConnectionStrings DAdbc = new DA_DBConnectionStrings();
            bool val;
            string connStrings;
            (val, connStrings) = DAdbc.dbconnection(model.uid, model.pwd, model.database, model.server);
            if (val) {

                return RedirectToAction("List",connStrings);
            } else { }
            return View();
        }
        public ActionResult schemaList() {
            return View();
        }
        public ActionResult defaultConnstrings(DO_DBconnection model) {
            DA_DBConnectionStrings DAdbc = new DA_DBConnectionStrings();
            bool val;
            string connStrings;
            (val, connStrings) = DAdbc.dbconnection(model.uid, model.pwd, model.database, model.server);
            if (val) {
                DOdbcList.Add(model);
                return Json(DOdbcList);
            }
            return View();
        }
      
    }
}