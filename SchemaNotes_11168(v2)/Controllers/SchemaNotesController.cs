using SchemaNotes_11168_v2_.Models;
using SchemaNotes_11168_v2_.Models.Repository.DataAccess;
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
    public class SchemaNotesController : Controller
    {
        public ActionResult SchemaNotesDetails(String ConnString)
        {
            if (string.IsNullOrEmpty(ConnString))
            {
                return RedirectToAction("DB_Connection");
            }
            else
            {
                #region get details of table and column from SV by SchemaViewModel
                ViewBag.ConnString = ConnString;
                SV_SchemaTablesColumns STC = new SV_SchemaTablesColumns();
                return View(STC.SchemaDetails(ConnString));
                #endregion
            }
        }
        public ActionResult Details(SchemaViewModel viewModel) {
            if (string.IsNullOrEmpty(viewModel.ConnString))
            {
                return RedirectToAction("DB_Connection");
            }
            else
            {
                #region get details of table and column from SV by SchemaViewModel
                ViewBag.ConnString = viewModel.ConnString;
                SV_SchemaTablesColumns STC = new SV_SchemaTablesColumns();
                return View(STC.SchemaDetails(viewModel.ConnString,viewModel.TableName));
                #endregion
            }
        }
        public ActionResult Edit(SchemaViewModel viewModel,String ConnString) {
            if (string.IsNullOrEmpty(viewModel.ConnString))
            {
                return RedirectToAction("DB_Connection");
            }
            else
            {
                #region get details of table and column from SV by SchemaViewModel
                SV_SchemaTablesColumns STC = new SV_SchemaTablesColumns();
                return View(STC.SchemaDetails(viewModel.ConnString, viewModel.TableName));
                #endregion
            }
        }
        [HttpPost]
        public ActionResult Edit(SchemaViewModel vModel) {
            SV_SchemaTablesColumns SV_STC = new SV_SchemaTablesColumns();
              bool value= SV_STC.SchemaEdit(vModel);
            return RedirectToAction("Index","Home");
        }
        public ActionResult DB_Connection(DO_DBconnection model)
        {
            DA_DBConnection DADBC = new DA_DBConnection(model);
            DADBC.IsConnectedSever(model);
            ViewBag.MyConnectionString = ConfigurationManager.ConnectionStrings["MySchemaNotes"].ConnectionString;
            //ViewBag.MyConnectionString = ConfigurationManager.ConnectionStrings["MyHOME"].ConnectionString;
            if (DADBC.IsConnected == false)
            {
                if (DADBC.IsConnstrings == false && DADBC.connStrings == "New")
                {
                    return View();
                }
                else
                {
                    MessageBox.Show(DADBC.connStrings);
                    return View();
                }
            }
            else
            {
              return RedirectToAction("SchemaNotesDetails", new { ConnString =DADBC.connStrings });
            }
        }
        public ActionResult test()
        {
            return View();
        }
        
    }
}