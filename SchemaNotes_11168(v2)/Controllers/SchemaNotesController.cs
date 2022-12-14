
using SchemaNotes_11168_v2_.Models;
using SchemaNotes_11168_v2_.Models.Commons;
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
        public ActionResult SchemaNotes_OverView(String ConnString)
        {
            if (string.IsNullOrEmpty(ConnString))
            {
                return RedirectToAction("DB_Connection");
            }
            else
            {
                #region get details of table and column from SV by SchemaViewModel
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
                SV_SchemaTablesColumns STC = new SV_SchemaTablesColumns();
                SchemaViewModel vModel = STC.SchemaDetails(viewModel.ConnString, viewModel.TableName);
                return View(vModel);
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
             SV_STC.SchemaEdit(vModel);
            return RedirectToAction("Details", vModel);
        }
        public ActionResult Search(SearchViewModel vModel) {
            if (string.IsNullOrEmpty(vModel.ConnString)) {
                return RedirectToAction("DB_Connection"); }
            else if (!string.IsNullOrEmpty(vModel.TableName)) {
                #region get details of table and column from SV by SchemaViewModel
                SV_SchemaTablesColumns STC = new SV_SchemaTablesColumns();
                return View(STC.SchemaDetails(vModel.ConnString,vModel.TableName));
                #endregion
                }
            else {

                SchemaViewModel VM = new SchemaViewModel();
                SV_SchemaTablesColumns STC = new SV_SchemaTablesColumns();
                VM = STC.SchemaDetails(vModel);
                return View(VM);
            }

        }
        public ActionResult DeepSearch(SearchViewModel vModel) {
            if (string.IsNullOrEmpty(vModel.ConnString))
            {
                return RedirectToAction("DB_Connection");
            }
            else if (!string.IsNullOrEmpty(vModel.TableName))
            {
                #region get details of table and column from SV by SchemaViewModel
                SV_SchemaTablesColumns STC = new SV_SchemaTablesColumns();
                return View(STC.SchemaDetails(vModel.ConnString, vModel.TableName));
                #endregion
            }
            else
            {
                SchemaViewModel VM = new SchemaViewModel();
                SV_SchemaTablesColumns STC = new SV_SchemaTablesColumns();
                VM = STC.SchemaDeepDetails(vModel);
                return View(VM);
            }


        }
        public ActionResult DB_Connection(DO_DBconnection model)
        {
            
            #region get the default settings of connectionString from webconfiguration
            getWebConnectionString GWCS = new getWebConnectionString();
            List<WebConfigConnectionString> WCCS = new List<WebConfigConnectionString>();
            WCCS = GWCS.getDefaultConnStrings();
            ViewBag.model = WCCS;
            #endregion
            AddOrUpdateConnectionString AOCS = new AddOrUpdateConnectionString();
            AOCS.UpdateSetting("TEST","TEST");
          DA_DBConnection DADBC = new DA_DBConnection(model);
            DADBC.IsConnectedSever(model);
            if (DADBC.IsConnected == false)
            {
                if (DADBC.IsConnstrings == false && DADBC.connStrings == "New")
                {
                    return View();
                }
                else
                {
                    MessageBox.Show(DADBC.connStrings+"連接失敗");
                    return View();
                }
            }
            else
            {
              return RedirectToAction("SchemaNotes_OverView", new { ConnString =DADBC.connStrings });
            }
        }
        public ActionResult test()
        {
            return View();
        }
        
    }
}