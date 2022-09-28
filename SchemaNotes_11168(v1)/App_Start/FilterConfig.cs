using System.Web;
using System.Web.Mvc;

namespace SchemaNotes_11168_v1_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
