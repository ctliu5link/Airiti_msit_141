//#define version_1
#define version_2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SchemaNotes_11168_v2_.Models;
using SchemaNotes_11168_v2_.Models.Repository.DataAccess.Base;
using SchemaNotes_11168_v2_.Models.Repository.DataAccess;
using Airiti.Common;
using System.Reflection;
using System.Data;
using Airiti.DataAccess;
using Airiti.Extensions;

namespace SchemaNotes_11168_v2_.Models
{
    public class DA_SchemaNotesTable : DA_Base<DO_SchemaNotesTable>
    {

        List<DO_SchemaNotesTable> SNTList = new List<DO_SchemaNotesTable>();
        #region SqlQuery
        string strSQLCmd =
                 "SELECT" +
                 " SO.name AS [TableName]," +
                 " CASE  WHEN SE.value IS NULL THEN 'Null' ELSE SE.value END AS[TableMSDescription],  " +
                 " CASE  WHEN SO.TYPE = 'U'  THEN '資料表'    ELSE '檢視表'  END AS[TableType]," +
                 " SS.name AS[TableStruct],  " +
                " CONVERT(VARCHAR(10), create_date, 120) AS[TableCreateTime],   " +
                " CONVERT(VARCHAR(10), modify_date, 120) AS[TableModifiedTime],   " +
                "  CASE WHEN SE1.value IS NULL THEN 'Null' ELSE SE1.value  END AS[TableRemark]," +
                " row_count AS[TableRows]" +
                " FROM sys.objects AS SO  JOIN sys.schemas AS SS ON SO.schema_id = SS.schema_id  " +
                " LEFT JOIN sys.extended_properties AS SE ON SO.object_id = SE.major_id  AND SE.minor_id = 0  AND SE.name = 'MS_Description'" +
                " LEFT JOIN sys.extended_properties AS SE1 ON SO.object_id = SE1.major_id   AND SE1.minor_id = 0   AND SE1.name = 'REMARK'" +
                " LEFT JOIN sys.dm_db_partition_stats AS SD ON SO.object_id = SD.object_id  AND (index_id < 2)" +
                " WHERE OBJECT_NAME(SO.object_id)  IN (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES);";

        #endregion
        public List<DO_SchemaNotesTable> GetTables(String connString)
        {
#if (version_1)
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(strSQLCmd, conn);
                conn.Open();
            #region  add all queried result to List  and return the list
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    DO_SchemaNotesTable DOSNT = new DO_SchemaNotesTable
                    {
                        TableName = dataReader["TableName"].ToString(),
                        TableMSDescription = dataReader["TableMSDescription"].ToString(),
                        TableType = dataReader["TableType"].ToString(),
                        TableStruct = dataReader["TableStruct"].ToString(),
                        TableCreateTime = Convert.ToDateTime(dataReader["TableCreateTime"]),
                        TableModifiedTime = Convert.ToDateTime(dataReader["TableModifiedTime"]),
                        TableRemark = dataReader["TableRemark"].ToString(),
                        TableRows = int.Parse(dataReader["TableRows"].ToString())
                    };
                    SNTList.Add(DOSNT);
                }
                return SNTList;

            #endregion
            }
        }
#endif
#if (version_2)
            #region 必要宣告
            string strClassName = "Airiti.Check.Models.Repository.DataAccess.DA_Account";
            string strMethodName = MethodBase.GetCurrentMethod().Name;
            LogFile objLogFile = new LogFile(strClassName, strMethodName);
            objLogFile.StartLog();
            List<DO_SchemaNotesTable> objReturn = new List<DO_SchemaNotesTable>();
            ReturnObject<DataTable> objDBReturn = new ReturnObject<DataTable>();
            #endregion
            try
            {
                objDBReturn = DBService.SingleQuery(connString, strSQLCmd);
                if (objDBReturn.ReturnData.Rows.Count != 0)
                {
                    objDBReturn.ReturnValue = OpReturnValue.Correct;
                    objReturn = objDBReturn.ReturnData.ToList<DO_SchemaNotesTable>().ToList();
                }
            }
            catch (Exception ex)
            {
                objDBReturn.ReturnValue = OpReturnValue.Exception;
                objDBReturn.ReturnMessage = ex.Message;
                objDBReturn.ReturnStackTrace = ex.StackTrace;
                objDBReturn.ReturnErrNum = OpErrNum.Exception;
                objLogFile.Log(string.Format("ReturnValue:{0}", OpReturnValue.Exception));
                objLogFile.Log(string.Format("ReturnMessage:{0}", ex.Message));
                objLogFile.Log(string.Format("ReturnStackTrace:{0}", ex.StackTrace));
                if (ex.InnerException != null)
                {
                    objLogFile.Log(string.Format("ReturnInnerMessage:{0}", ex.InnerException.Message));
                    objLogFile.Log(string.Format("ReturnInnerStackTrace:{0}", ex.InnerException.StackTrace));
                }
            }
            finally
            {
                objLogFile.EndLog();
            }

            return objReturn;
            #endif
        }


        public override string TableName => throw new NotImplementedException();
        public override ReturnObject<int> AddData(List<DO_SchemaNotesTable> pData)
        {
            throw new NotImplementedException();
        }
        public override ReturnObject<int> DeleteData(List<DO_SchemaNotesTable> pData)
        {
            throw new NotImplementedException();
        }
        public override ReturnObject<int> ModifyData(List<DO_SchemaNotesTable> pData)
        {
            throw new NotImplementedException();
        }
        public override ReturnObject<int> SaveData(List<DO_SchemaNotesTable> pData)
        {
            throw new NotImplementedException();
        }
    }
}