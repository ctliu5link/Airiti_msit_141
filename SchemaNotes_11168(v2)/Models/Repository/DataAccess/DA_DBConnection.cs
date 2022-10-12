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
        #region dictionary for defaultConnectedString
//Dictionary<string, string> dictDefaultString = new Dictionary<string, string>();
        //public Dictionary<string, string> dictDefaultConnectedString()
        //{
        //    DO_DBconnection DODB = new DO_DBconnection();
        //    List<DO_DBconnection> DODBList = new List<DO_DBconnection>();
        //    string dConnectedStrings = ConfigurationManager.ConnectionStrings["MySchemaNotes"].ConnectionString;
        //    string[] dConn = dConnectedStrings.Split(';').Where(item => item != "").ToArray();
        //    foreach (string item in dConn)
        //    {
        //        int IndexKey = item.IndexOf("=");
        //        int IndexValue = item.IndexOf(";");
        //        string key = item.Substring(0, IndexKey);
        //        string value = item.Substring((IndexKey + 1));
        //        dictDefaultString.Add(key, value);
        //    }
        //    return dictDefaultString;
        //}
        #endregion
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