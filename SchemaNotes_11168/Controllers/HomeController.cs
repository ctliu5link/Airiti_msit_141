using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Configuration;
using SchemaNotes_11168.Models;
namespace SchemaNotes_11168.Controllers
{
    /// <summary>
    /// SchemaDetails of table and column operation
    /// include unit of  DBconnection and SV logic operation 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// declare the connetionStrings
        /// </summary>
        public static string connStrings = "";
        /// <summary>
        /// Declare two list of schemaTable and schemaColumn to recevie the return value of DA by SV Logic Operation
        /// </summary>
        public static List<DO_schemaNotesTable> SNTList = new List<DO_schemaNotesTable>();
        public static List<DO_schemaNotesColumn> SNCList = new List<DO_schemaNotesColumn>();
        /// <summary>
        /// SchmeaNotes SV logic operation
        /// </summary>
        /// <param name="vModel"></param>
        ///  <param name="TupleModel"></param>
        /// <returns>TupleModle include Lists of schemaTableDetail and schemaColumnDeatil </returns>
        public ActionResult Index(searchKeyword vModel)
        {
            #region check the keyWord of search is null then return the all tables from connection db
            if (string.IsNullOrEmpty(vModel.keyWord))
            {
                DA_schemaNotesTable DASNT = new DA_schemaNotesTable();
                DA_schemaNotesColumn DASNC = new DA_schemaNotesColumn();
                SNTList = DASNT.GetTables(connStrings);
                SNCList = DASNC.GetColumns(connStrings);
                var TupleModel = new Tuple<List<DO_schemaNotesTable>, List<DO_schemaNotesColumn>>(SNTList, SNCList);
                return View(TupleModel);
            }
            #endregion
            #region excute the search logic and return the table of search target
            else
            {
                var t = SNTList.Where(m => m.tableName.Contains(vModel.keyWord)).ToList();
                var c = SNCList.Where(m1 => m1.tableName.Contains(vModel.keyWord)).ToList();
                var TupleModel = new Tuple<List<DO_schemaNotesTable>, List<DO_schemaNotesColumn>>(t, c);
                return View(TupleModel);
            }
            #endregion
        }
        /// <summary>
        ///  Check  state of DBConnection 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="database"></param>
        /// <param name="IPAddress"></param>
        /// <returns>View , Message,ConnectionStrings</returns>
        public ActionResult Connection(DBconnection model)
        {
                #region check the input parameters of dbconnction is null and return View and message
            if (string.IsNullOrEmpty(model.uid) || string.IsNullOrEmpty(model.pwd) || string.IsNullOrEmpty(model.database) || string.IsNullOrEmpty(model.server))
            {
                ViewBag.Message = "Please fill all required data!";
                return View();
            }
            #endregion
                #region check the dbconnection state 
            else
            {
                bool val;
                (val, connStrings) = model.IsSeverConnected();
                #endregion
                #region  dbconnection ok and redirect to Index controller to start the SV
                if (val) { 
                    return RedirectToAction("Index"); 
                }
             #endregion
                #region check the connection state  is fail and return ErrrorMessage and View() 
                else
                {
                    ViewBag.Message = connStrings;
                    return View();
                }
                #endregion
            }
        }
    }
}