using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Windows;
using SchemaNote_11169.Models;


namespace SchemaNote_11169.Controllers
{
    public class HomeController : Controller
    {
     .
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult connect()
        {
            List<columnschema> columnlist = new List<columnschema>();
            List<titalschema> titallist = new List<titalschema>();
            
            try
            {
                SqlConnection cn=new SqlConnection("Data Source=DT-6169;Initial Catalog=TestRepo;Integrated Security=True");
                cn.Open();
                SqlCommand command = new SqlCommand("SELECT ISC.TABLE_NAME AS [資料表], "
       +"SC.name AS[A.欄位名稱],"
       +"SE1.value AS[B.欄位說明],"
       +"ISC.DATA_TYPE + '(' + CONVERT(VARCHAR, ISC.CHARACTER_MAXIMUM_LENGTH) + ')' AS[C.資料型態],"
       +"ISC.IS_NULLABLE AS[E.不為NULL],"
       + "CASE WHEN ISK.CONSTRAINT_NAME IS NULL THEN 0 ELSE 1 END AS [D.主鍵],"
       + "ISC.COLUMN_DEFAULT AS[F.預設值],"
       +"SE.value AS[G.備註]"
+" FROM sys.columns AS SC"
     +" LEFT JOIN sys.objects SO ON SO.object_id = SC.object_id"
     +" LEFT JOIN sys.extended_properties SE ON SE.minor_id = SC.column_id"
                                             +" AND se.major_id = so.object_id"
                                             +" AND SE.name = 'REMARK'"
     +" LEFT JOIN sys.extended_properties SE1 ON SE1.minor_id = SC.column_id"
                                              +" AND se1.major_id = so.object_id"
                                              +" AND SE1.name = 'MS_Description'"
     +" LEFT JOIN INFORMATION_SCHEMA.COLUMNS AS ISC ON ISC.COLUMN_NAME = SC.name"
                                                    +" AND ISC.TABLE_NAME = so.name"
    + " LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ISK ON ISK.TABLE_NAME = SO.name"
                                              +" AND SC.name = ISK.COLUMN_NAME"
+ " WHERE OBJECT_NAME(SO.object_id) in (select TABLE_NAME from INFORMATION_SCHEMA.TABLES)"
+ " ORDER BY 資料表;", cn);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    columnschema a = new columnschema();
                    a.不為NULL = $"{dataReader["E.不為NULL"]}";
                    a.備註 = $"{dataReader["G.備註"]}";
                    a.欄位名稱 = $"{dataReader["A.欄位名稱"]}";
                    a.欄位說明 = $"{dataReader["B.欄位說明"]}";
                    a.資料型態 = $"{dataReader["C.資料型態"]}";
                    a.預設值 = $"{dataReader["F.預設值"]}";
                    a.主鍵 = $"{dataReader["D.主鍵"]}";
                    a.資料表= $"{dataReader["資料表"]}";
                    columnlist.Add(a);
                }
                cn.Close();
                cn.Open();

                SqlCommand command2 = new SqlCommand(@"SELECT
       case SO.type when 'U' then '資料表' end  as[物件類型],
       isnull(SEp1.value, 'null') as[備註],
       isnull(SEp.value, 'null') as[物件說明],
       IST.TABLE_SCHEMA as[結構描述名稱],
       IST.TABLE_NAME AS[物件名稱],
       CONVERT(varchar(10), SO.create_date, 120) AS[物件創造日期],
       CONVERT(varchar(10), SO.modify_date, 120) AS[物件修改日期],
       st.Total_Rows as [總筆數]

from INFORMATION_SCHEMA.TABLES as IST
left join(select * from sys.objects
) so on IST.TABLE_NAME = so.name
left join(select * from sys.extended_properties where name = 'MS_Description'AND minor_id = '0')
sep on sep.major_id = so.object_id


left join(select * from sys.extended_properties where name = 'REMARK'AND minor_id = '0')
sep1 on sep1.major_id = so.object_id
left join(SELECT OBJECT_NAME(object_id) tablename, Total_Rows = SUM(st.row_count)
FROM sys.dm_db_partition_stats st
WHERE OBJECT_NAME(object_id) in (select TABLE_NAME from INFORMATION_SCHEMA.TABLES)
      AND(index_id < 2)

      group by OBJECT_NAME(object_id)) st on st.tablename = so.name", cn);

                
                SqlDataReader dataReader2 = command2.ExecuteReader();
                while (dataReader2.Read())
                {
                    titalschema a = new titalschema();
                    a.物件類型=$"{dataReader2["物件類型"]}";
                    a.備註= $"{dataReader2["備註"]}";
                    a.物件說明= $"{dataReader2["物件說明"]}";
                    a.結構描述名稱= $"{dataReader2["結構描述名稱"]}";
                    a.物件名稱= $"{dataReader2["物件名稱"]}";
                    a.物件創造日期= $"{dataReader2["物件創造日期"]}";
                    a.物件修改日期= $"{dataReader2["物件修改日期"]}";
                    a.總筆數= $"{dataReader2["總筆數"]}";
                    titallist.Add(a);
                }
                
                cn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return View(new Tuple<List<columnschema>,List<titalschema>>(columnlist, titallist));
        }
    }
}