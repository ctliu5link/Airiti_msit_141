using SchemaNotes_11168_v2_.Models;
using SchemaNotes_11168_v2_.Models.Repository.DataAccess;
using SchemaNotes_11168_v2_.Models.Repository.DataAccess.Base;
using SchemaNotes_11168_v2_.Models.Services;
using SchemaNotes_11168_v2_.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace SchemaNotes_11168_v2_.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(DO_DBconnection model, SchemaViewModel vModel)
        {
            #region get details of table and column from SV by SchemaViewModel
            SV_SchemaTablesColumns STC = new SV_SchemaTablesColumns();
            DA_DBConnection da = new DA_DBConnection(model);
            vModel = STC.SchemaDetails(da);
            return View(vModel);
            #endregion
        }
        public ActionResult DB_Connection(DO_DBconnection model) 
        {
            DA_DBConnection DADBC = new DA_DBConnection(model);
            bool val;
            string connstring;
            (val, connstring) = DADBC.IsConnectedSever(model);
            if (DADBC.IsConnected==false)
            {
                if (val == false && connstring == "New")
                {
                    return View();
                }
                else
                {
                    MessageBox.Show(connstring);
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", model);
            }
        }
    }
}