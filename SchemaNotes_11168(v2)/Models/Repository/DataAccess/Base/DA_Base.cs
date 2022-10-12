﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Airiti.Common;

namespace SchemaNotes_11168_v2_.Models.Repository.DataAccess.Base
{
    public abstract class DA_Base<T>
    {
        public string connStrings { get; set; }
        public bool IsConnected { get; set; }
        public bool IsConnstrings { get; set; }
       public  Tuple<bool, string> IsConnectedSever(DO_DBconnection model)
        {
            if (connStrings=="Error"||connStrings=="New")
            {
                IsConnstrings = false;
                return Tuple.Create(IsConnstrings,connStrings);
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connStrings))
                {
                    try
                    {
                        conn.Open();
                        IsConnected = true;
                        return Tuple.Create(IsConnected, connStrings);
                    }
                    catch (SqlException ex)
                    {
                        IsConnected = false;
                        return Tuple.Create(IsConnected,ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
       public abstract  string TableName { get;  }
        public abstract ReturnObject<int> AddData(List<T> pData);
        public abstract ReturnObject<int> ModifyData(List<T> pData);
        public abstract ReturnObject<int> DeleteData(List<T> pData);
        public abstract ReturnObject<int> SaveData(List<T> pData);
    }
}