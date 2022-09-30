using SchemaNotes_11168_v2_.Models;
using SchemaNotes_11168_v2_.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchemaNotes_11168_v2_.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index( DO_DBconnection model)
        {
            SchemaTablesColumns STC = new SchemaTablesColumns();
            List<object> list = new List<object>();
            list.Add(STC.SchemaDetails(model));
            return View(list);
        }

    
        public ActionResult DB_Connection(DO_DBconnection model) {
           DA_SchemaNotesTable DASNT = new DA_SchemaNotesTable();
            DASNT.IsConnectedSever(model);
            bool IsConnected = DASNT.IsConnected;
            if (IsConnected)
            {
                return RedirectToAction("Index",model);
            }
            else { return View(); }
        }
    }
}