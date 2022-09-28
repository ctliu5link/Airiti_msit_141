using System.Web;
using System.Web.Mvc;

namespace SchemaNote_11170__2_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
