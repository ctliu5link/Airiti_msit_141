﻿using SchemaNotes_11168_v2_.Models;
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
           "SELECT SO.name AS [TableName]," +
           "SC.name AS[ColumnName],  " +
           " CASE WHEN SE1.value IS NULL THEN 'Null' ELSE SE1.value END AS[ColumnMSDescription], " +
           " CASE WHEN ISC.CHARACTER_MAXIMUM_LENGTH IS NULL THEN ISC.DATA_TYPE ELSE  (ISC.DATA_TYPE + '(' + CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH) + ')') END AS[ColumnType]," +
           " CASE  WHEN ISK.CONSTRAINT_NAME IS NULL THEN 'NO'   ELSE 'YES'   END AS[ColumnPrimaryKey], " +
           " ISC.IS_NULLABLE AS[ColumnNull]," +
            "ISC.COLUMN_DEFAULT AS[ColumnDefault]," +
           "CASE WHEN SE.value IS NULL THEN 'Null' ELSE SE.value  END AS[ColumnRemark]" +

           " FROM sys.columns AS SC"+
           " LEFT JOIN sys.objects SO ON SO.object_id = SC.object_id"+
           " LEFT JOIN sys.extended_properties SE ON SE.minor_id = SC.column_id   AND SE.major_id = SO.object_id   AND SE.name = 'REMARK'"+
           " LEFT JOIN sys.extended_properties SE1 ON SE1.minor_id = SC.column_id AND SE1.major_id = SO.object_id    AND SE1.name = 'MS_Description'"+
           " LEFT JOIN INFORMATION_SCHEMA.COLUMNS AS ISC ON ISC.COLUMN_NAME = SC.name    AND ISC.TABLE_NAME = SO.name"+
            " LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.TABLE_NAME = SO.name  AND SC.name = ISK.COLUMN_NAME "+
            "WHERE OBJECT_NAME(SO.object_id) IN "+
            "(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES) ORDER BY  SC.column_id ;";


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
                        TableName = dataReader["TableName"].ToString(),
                        ColumnName = dataReader["ColumnName"].ToString(),
                        ColumnMSDescription = dataReader["ColumnMSDescription"].ToString(),
                        ColumnType = dataReader["ColumnType"].ToString(),
                        ColumnPrimaryKey =dataReader["ColumnPrimaryKey"].ToString(),
                        ColumnNull = dataReader["ColumnNull"].ToString(),
                        ColumnDefault = dataReader["ColumnDefault"].ToString(),
                        ColumnRemark = dataReader["ColumnRemark"].ToString()
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
        public override string TableName => throw new NotImplementedException();

        public override ReturnObject<int> AddData(List<DA_DBConnection> pData)
        {
            throw new NotImplementedException();
        }

        public override ReturnObject<int> DeleteData(List<DA_DBConnection> pData)
        {
            throw new NotImplementedException();
        }
    }
}