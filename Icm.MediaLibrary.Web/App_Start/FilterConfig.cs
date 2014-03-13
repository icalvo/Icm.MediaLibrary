using System.Web.Mvc;

namespace Icm.MediaLibrary.Web
{
    internal class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
