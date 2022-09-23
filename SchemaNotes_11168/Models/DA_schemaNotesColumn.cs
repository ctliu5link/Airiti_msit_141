﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SchemaNotes_11168.Models;

namespace SchemaNotes_11168.Models
{
    public class DA_schemaNotesColumn
    {
        public List<DO_schemaNotesColumn> GetColumns(string connStrings)
        {
            List<DO_schemaNotesColumn> SNCList = new List<DO_schemaNotesColumn>();
            using (SqlConnection conn = new SqlConnection(connStrings))
            {
                conn.Open();
                string commandText = "SELECT SO.name AS [物件名稱],SC.name AS[欄位名稱],     SE1.value AS[欄位說明],  ISC.DATA_TYPE + '(' + CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH) + ')' AS[資料型態],"
                   + "CASE  WHEN ISK.CONSTRAINT_NAME IS NULL THEN 0   ELSE 1   END AS[主鍵],  ISC.IS_NULLABLE AS[不為NULL],ISC.COLUMN_DEFAULT AS[預設值], SE.value AS[備註]"
                   + "FROM sys.columns AS SC"
                   + " LEFT JOIN sys.objects SO ON SO.object_id = SC.object_id"
                   + " LEFT JOIN sys.extended_properties SE ON SE.minor_id = SC.column_id   AND SE.major_id = SO.object_id   AND SE.name = 'REMARK'"
                   + " LEFT JOIN sys.extended_properties SE1 ON SE1.minor_id = SC.column_id AND SE1.major_id = SO.object_id    AND SE1.name = 'MS_Description'"
                   + " LEFT JOIN INFORMATION_SCHEMA.COLUMNS AS ISC ON ISC.COLUMN_NAME = SC.name    AND ISC.TABLE_NAME = SO.name"
                   + " LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.TABLE_NAME = SO.name  AND SC.name = ISK.COLUMN_NAME "
                   + $"WHERE OBJECT_NAME(SO.object_id) IN "
                      + "(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES) ORDER BY SC.name;";
            using (SqlCommand command = new SqlCommand(commandText, conn))
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        DO_schemaNotesColumn DOSNC = new DO_schemaNotesColumn
                        {
                            tableName = dataReader["物件名稱"].ToString(),
                            columnName = dataReader["欄位名稱"].ToString(),
                            columnMSDescription = dataReader["欄位說明"].ToString(),
                            columnType = dataReader["資料型態"].ToString(),
                            //columnPrimaryKey = int.Parse(dataReader["主鍵"].ToString()),
                            columnPrimaryKey =Convert.ToBoolean(dataReader["主鍵"]),
                            columnNull =(dataReader["不為NULL"].ToString()),
                            columnDefault = dataReader["預設值"].ToString(),
                            columnRemark = dataReader["備註"].ToString()
                        };
                        SNCList.Add(DOSNC);

                    }
                }
                return SNCList;
            }

        }
    }
}