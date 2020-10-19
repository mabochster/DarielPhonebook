using Dariel.Mahlatse.PhoneBook.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Dariel.Mahlatse.PhoneBook.Service.AspNet.Controllers
{
    public class DefaultApiController : ApiController
    {
        //instantiate the logger
        //private ILog log = LogManager.GetLogger(typeof(LookupController));
        public readonly PhonebookUnitOfWork PhonebookUnitOfWork;
        public DefaultApiController()
        {
            PhonebookUnitOfWork = (PhonebookUnitOfWork)DependencyResolver.Current.GetService(typeof(PhonebookUnitOfWork));
            //this._LiveKoinsUnitOfWork = LiveKoinsUnitOfWork ?? throw new ArgumentNullException($"The variable {nameof(LiveKoinsUnitOfWork)} cannot be null");
        }
    }
}