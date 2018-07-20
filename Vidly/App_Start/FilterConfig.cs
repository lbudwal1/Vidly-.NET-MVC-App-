using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //this one i write down which allows authrization for login admin vs user
            filters.Add(new AuthorizeAttribute());
        }
    }
}
