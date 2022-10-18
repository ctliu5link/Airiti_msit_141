using Airiti.Common;
using SchemaNotes_11168_v2_.Models.Repository.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SchemaNotes_11168_v2_.Models.Repository.DataAccess
{
    public class DA_DBConnection:DA_Base<DO_DBconnection>
    {
       
        public DA_DBConnection(DO_DBconnection model)
        {
                if (string.IsNullOrEmpty(model.uid) && string.IsNullOrEmpty(model.pwd) && string.IsNullOrEmpty(model.database) && string.IsNullOrEmpty(model.server))
                {
                    connStrings = "New";
                }
                else if (string.IsNullOrEmpty(model.uid) || string.IsNullOrEmpty(model.pwd) || string.IsNullOrEmpty(model.database) || string.IsNullOrEmpty(model.server))
                {
                    connStrings = "Error";
                }
                else
                {
                    connStrings = $"uid={model.uid} ; pwd={model.pwd};database={model.database};server={model.server};";
                }
            }

        public override string TableName => throw new NotImplementedException();

        public override ReturnObject<int> AddData(List<DO_DBconnection> pData)
        {
            throw new NotImplementedException();
        }

        public override ReturnObject<int> DeleteData(List<DO_DBconnection> pData)
        {
            throw new NotImplementedException();
        }

        public override ReturnObject<int> ModifyData(List<DO_DBconnection> pData)
        {
            throw new NotImplementedException();
        }

        public override ReturnObject<int> SaveData(List<DO_DBconnection> pData)
        {
            throw new NotImplementedException();
        }

      public class Entity<T> {
            public Entity() { DataList = new List<T>(); }
            public List<T> DataList { get; set; }
            public void addEntityRow(T objRowData)
            {
                DataList.Add(objRowData);
            }
            public bool removeData(T objRowData) {
                    bool boolRemoveResult = true;
                    if (DataList.Contains(objRowData))
                    {
                        DataList.Remove(objRowData);
                    }
                    else { boolRemoveResult = false; }
                    return boolRemoveResult;
                
                }
            }
        }
    }