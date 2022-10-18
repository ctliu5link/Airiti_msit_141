using SchemaNote_11170__2_.Models.DataAccess;
using SchemaNote_11170__2_.Models.DataObject;
using SchemaNote_11170__2_.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SchemaNote_11170__2_.Service
{
    public class SV_ShowData
    {
        /// <summary>
        /// 將TableDetail注入List中
        /// </summary>
        /// <param name="connecStrings"></param>
        /// <returns></returns>
        public List<DO_TableDetail> InsertTableDetail(string connecStrings)
        {
            DA_TableDetail table = new DA_TableDetail();
            return table.SearchTableDetail(connecStrings);
        }
        /// <summary>
        /// 將ColumnDetail注入List中
        /// </summary>
        /// <param name="connecStrings"></param>
        /// <returns></returns>
        public List<DO_ColumnDetail> InsertColumnDetail(string connecStrings)
        {
            DA_ColumnDetail column = new DA_ColumnDetail();
            return column.SearchColumnDetail(connecStrings);
        }
        /// <summary>
        /// (Detail用)將TableDetail注入List中
        /// </summary>
        /// <param name="connecStrings"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public List<DO_TableDetail> InsertTableDetail_ByTableName(string connecStrings,string tablename)
        {
            DA_TableDetail table = new DA_TableDetail();
            return table.TableDetail_ByTableName(connecStrings,tablename);
        }
        /// <summary>
        /// (Detail用)將ColumnDetail注入List中
        /// </summary>
        /// <param name="connecStrings"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public List<DO_ColumnDetail> InsertColumnDetail_ByTableName(string connecStrings, string tablename)
        {
            DA_ColumnDetail table = new DA_ColumnDetail();
            return table.ColumnDetail_ByTableName(connecStrings, tablename);
        }
        /// <summary>
        /// DataDetail的所有CRUD
        /// </summary>
        /// <param name="vModel"></param>
        public void CRUD_DataDetail(VM_ShowDataDetail vModel)
        {
            string tableName = vModel.TableDetail[0].資料表名稱;
            string tabletStruct = vModel.TableDetail[0].結構描述名稱;
            string MS_Description = vModel.TableDetail[0].資料說明;
            string REMARK = vModel.TableDetail[0].備註;
            string columnName = "";

            //載入原本資料：用來比對後來資料
            List<DO_TableDetail> list_Tableforcheck = InsertTableDetail_ByTableName(vModel.Connection, tableName);
            List<DO_ColumnDetail> list_Columnforcheck = InsertColumnDetail_ByTableName(vModel.Connection, tableName);
            
            //執行後來資料(CRUD)
            DA_TableDetail dA_Table = new DA_TableDetail();
            DA_ColumnDetail dA_Column = new DA_ColumnDetail();

            #region table的所有Detail判斷
            for (int i = 0; i < list_Tableforcheck.Count; i++){
                //table_MS_Description的判斷
                //1. 判斷【原本】是否有資料，if(有)else(無)
                if (list_Tableforcheck[i].資料說明.ToLower() != "null"){
                    //2. 判斷【後來】是否有資料，if(有)else(無)
                    if (!string.IsNullOrEmpty(MS_Description) && MS_Description.ToLower() != "null"){
                        //update
                        dA_Table.UpdateTable_MS_Description(MS_Description, tabletStruct, tableName, vModel.Connection);
                    }
                    else{
                        //drop
                        dA_Table.DropTable_MS_Description(tabletStruct, tableName, vModel.Connection);
                    }
                }
                else{
                    if (!string.IsNullOrEmpty(MS_Description) && MS_Description.ToLower() != "null"){
                        //add
                        dA_Table.AddTable_MS_Description(MS_Description, tabletStruct, tableName, vModel.Connection);
                    }
                }
                //table_REMARK的判斷
                //1. 判斷【原本】是否有資料，if(有)else(無)
                if (list_Tableforcheck[i].備註.ToLower() != "null"){
                    //2. 判斷【後來】是否有資料，if(有)else(無)
                    if (!string.IsNullOrEmpty(REMARK) && REMARK.ToLower() != "null"){
                        //update
                        dA_Table.UpdateTable_REMARK(REMARK, tabletStruct, tableName, vModel.Connection);
                    }
                    else{
                        //drop
                        dA_Table.DropTable_REMARK(tabletStruct, tableName, vModel.Connection);
                    }
                }
                else{
                    if (!string.IsNullOrEmpty(REMARK) && REMARK.ToLower() != "null"){
                        //add
                        dA_Table.AddTable_REMARK(REMARK, tabletStruct, tableName, vModel.Connection);
                    }
                }
            }
            #endregion
            #region column的所有Detail判斷
            for (int i = 0; i < list_Columnforcheck.Count; i++){
                //換值 columnName、MS_Description、REMARK
                columnName = vModel.ColumnDetail[i].欄位名稱;
                MS_Description = vModel.ColumnDetail[i].欄位說明;
                REMARK = vModel.ColumnDetail[i].備註;

                //column_MS_Description的判斷
                //1. 判斷【原本】是否有資料，if(有)else(無)
                if (list_Columnforcheck[i].欄位說明.ToLower() != "null"){
                    //2. 判斷【後來】是否有資料，if(有)else(無)
                    if (!string.IsNullOrEmpty(MS_Description) && MS_Description.ToLower() != "null"){
                        //update
                        dA_Column.UpdateColumn_MS_Description(MS_Description, tabletStruct, tableName, columnName, vModel.Connection);
                    }
                    else{
                        //drop
                        dA_Column.DropColumn_MS_Description(tabletStruct, tableName, columnName, vModel.Connection);
                    }
                }
                else{
                    if (!string.IsNullOrEmpty(MS_Description) && MS_Description.ToLower() != "null"){
                        //add
                        dA_Column.AddColumn_MS_Description(MS_Description, tabletStruct, tableName, columnName, vModel.Connection);
                    }
                }
                //column_REMARK的判斷
                //1. 判斷【原本】是否有資料，if(有)else(無)
                if (list_Columnforcheck[i].備註.ToLower() != "null"){
                    //2. 判斷【後來】是否有資料，if(有)else(無)
                    if (!string.IsNullOrEmpty(REMARK) && REMARK.ToLower() != "null"){
                        //update
                        dA_Column.UpdateColumn_REMARK(REMARK, tabletStruct, tableName, columnName, vModel.Connection);
                    }
                    else{
                        //drop
                        dA_Column.DropColumn_REMARK(tabletStruct, tableName, columnName, vModel.Connection);
                    }
                }
                else{
                    if (!string.IsNullOrEmpty(REMARK) && REMARK.ToLower() != "null"){
                        //add
                        dA_Column.AddColumn_REMARK(REMARK, tabletStruct, tableName, columnName, vModel.Connection);
                    }
                }
            }
            #endregion

        }
    }
}