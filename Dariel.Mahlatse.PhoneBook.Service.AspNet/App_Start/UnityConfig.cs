using Dariel.Mahlatse.PhoneBook.Dal;
using Dariel.Mahlatse.PhoneBook.Dal.Repositories.Database;
using System.Diagnostics;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Dariel.Mahlatse.PhoneBook.Service.AspNet
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            var context = new PhoneBookDataContext(Constants.ConfigurationConstants.CONNECTION_STRING_NAME);

            if (Debugger.IsAttached)
                context.Database.Log = LogDatabase;

            container.RegisterFactory<PhonebookUnitOfWork>(
                c => new PhonebookUnitOfWork(context));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        //log db context chnages to the Debugger for now
        private static void LogDatabase(string value)
        {
            Debug.WriteLine(value);
        }
    }
}