using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Configuration;
using SchemaNotes_11168.Models;
using System.Dynamic;

namespace SchemaNotes_11168.Controllers
{
   
    public class HomeController : Controller
    {
       public static string connStrings="";
        List<DO_schemaNotesTable> SNTList = new List<DO_schemaNotesTable>();
        List<DO_schemaNotesColumn> SNCList = new List<DO_schemaNotesColumn>();
        public ActionResult Index(  )
        {
            DA_schemaNotesTable DASNT = new DA_schemaNotesTable();
            DA_schemaNotesColumn DASNC = new DA_schemaNotesColumn();
            var TupleMoldel = new Tuple<List<DO_schemaNotesTable>, List<DO_schemaNotesColumn>>(DASNT.GetTables(connStrings), DASNC.GetColumns(connStrings));
            return View(TupleMoldel);
        }
        public ActionResult Connection(DBconnection model)
        {
            if ( string.IsNullOrEmpty(model.uid) || string.IsNullOrEmpty(model.pwd) || string.IsNullOrEmpty(model.database) || string.IsNullOrEmpty(model.IPAddress))
            {
                ViewBag.Message = "Please fill all required data!";
                return View();
            }
            else
            {
                bool val;
                (val, connStrings) = model.IsSeverConnected(model.uid, model.pwd, model.database, model.IPAddress);
                if (val) { return RedirectToAction("Index"); }
                else
                { 
                    ViewBag.Message = connStrings;
                    return View();
                }
            }
        }
    }
}