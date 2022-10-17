using SchemaNotes_11168_v2_.Models;
using SchemaNotes_11168_v2_.Models.Repository.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SchemaNotes_11168_v2_.Models.Repository.DataAccess;
using Airiti.Common;

namespace SchemaNotes_11168_v2_.Models
{
    public class DA_SchemaNotesColumn: DA_Base<DA_DBConnection>
    {
   
        List<DO_SchemaNotesColumn> SNCList = new List<DO_SchemaNotesColumn>();
        #region SqlQuery 
        string commandText =
           "SELECT SO.name AS [物件名稱]," +
           "SC.name AS[欄位名稱],  "+
           " CASE WHEN SE1.value IS NULL THEN 'Null' ELSE SE1.value END AS[欄位說明], "+
           " CASE WHEN ISC.CHARACTER_MAXIMUM_LENGTH IS NULL THEN ISC.DATA_TYPE ELSE  (ISC.DATA_TYPE + '(' + CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH) + ')') END AS[資料型態],"+
           " CASE  WHEN ISK.CONSTRAINT_NAME IS NULL THEN 'NO'   ELSE 'YES'   END AS[主鍵], " +
           " ISC.IS_NULLABLE AS[不為NULL]," +
            "ISC.COLUMN_DEFAULT AS[預設值]," +
           "CASE WHEN SE.value IS NULL THEN 'Null' ELSE SE.value  END AS[備註]"+

           " FROM sys.columns AS SC"+
           " LEFT JOIN sys.objects SO ON SO.object_id = SC.object_id"+
           " LEFT JOIN sys.extended_properties SE ON SE.minor_id = SC.column_id   AND SE.major_id = SO.object_id   AND SE.name = 'REMARK'"+
           " LEFT JOIN sys.extended_properties SE1 ON SE1.minor_id = SC.column_id AND SE1.major_id = SO.object_id    AND SE1.name = 'MS_Description'"+
           " LEFT JOIN INFORMATION_SCHEMA.COLUMNS AS ISC ON ISC.COLUMN_NAME = SC.name    AND ISC.TABLE_NAME = SO.name"+
            " LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.TABLE_NAME = SO.name  AND SC.name = ISK.COLUMN_NAME "+
            "WHERE OBJECT_NAME(SO.object_id) IN "+
            "(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES) ORDER BY  SC.column_id ;";

        public override string TableName => throw new NotImplementedException();

        public override ReturnObject<int> AddData(List<DA_DBConnection> pData)
        {
            throw new NotImplementedException();
        }

        public override ReturnObject<int> DeleteData(List<DA_DBConnection> pData)
        {
            throw new NotImplementedException();
        }
        #endregion
        public List<DO_SchemaNotesColumn> GetTables(string connString)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(commandText, conn);
                conn.Open();
                #region  add all queried result to List  and return the list
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    DO_SchemaNotesColumn DOSNC = new DO_SchemaNotesColumn
                    {
                        TableName = dataReader["物件名稱"].ToString(),
                        ColumnName = dataReader["欄位名稱"].ToString(),
                        ColumnMSDescription = dataReader["欄位說明"].ToString(),
                        ColumnType = dataReader["資料型態"].ToString(),
                        ColumnPrimaryKey =dataReader["主鍵"].ToString(),
                        ColumnNull = dataReader["不為NULL"].ToString(),
                        ColumnDefault = dataReader["預設值"].ToString(),
                        ColumnRemark = dataReader["備註"].ToString()
                    };
                    SNCList.Add(DOSNC);
                }
             return SNCList;
                #endregion
            } 
        }

 
        public override ReturnObject<int> ModifyData(List<DA_DBConnection> pData)
        {
            throw new NotImplementedException();
        }

        public override ReturnObject<int> SaveData(List<DA_DBConnection> pData)
        {
            throw new NotImplementedException();
        }
    }
}