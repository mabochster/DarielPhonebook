using System.Web;
using System.Web.Mvc;

namespace Dariel.Mahlatse.PhoneBook.UI.AspNet
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
