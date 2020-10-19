using System.Web;
using System.Web.Mvc;

namespace Dariel.Mahlatse.PhoneBook.Service.AspNet
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
