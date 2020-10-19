
using Dariel.Mahlatse.Phonebook.Core.Models;
using Dariel.Mahlatse.Phonebook.Core.Models.ConversionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Dariel.Mahlatse.PhoneBook.Service.AspNet.Controllers
{
    [RoutePrefix("api/Phonebook")]
    public class PhonebookController : DefaultApiController
    {
        [System.Web.Mvc.HttpGet]
        [Route("GetPhoneBooks")]
        [ResponseType(typeof(IEnumerable<PhoneBookModel>))]
        public async Task<IHttpActionResult> GetLookups()
        {
            //get the lookups in the system
            return Ok(await Task.FromResult(PhonebookUnitOfWork.PhonebookRepository.Get()
                .Select(x => x.GetPhoneBookModelFrom())));
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Http.Route("GetPhoneBookById")]
        [ResponseType(typeof(PhoneBookModel))]
        public async Task<IHttpActionResult> GetPhoneBookById(int id)
        {
            if (id <= 0)
                throw new ArgumentNullException($"{nameof(id)} cannot be empty");

            return Ok(await Task.FromResult(PhonebookUnitOfWork.PhonebookRepository.GetByID(id)
                .GetPhoneBookModelFrom()));
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Http.Route("SavePhonebook")]
        [ResponseType(typeof(PhoneBookModel))]
        public async Task<IHttpActionResult> SavePhonebook(PhoneBookModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    //save the changes to the data
                    var existingModel =
                        PhonebookUnitOfWork.PhonebookRepository.GetByID(model.Id);

                    existingModel.Name = model.Name;

                    PhonebookUnitOfWork.PhonebookRepository.Update(existingModel);
                }
                else
                {
                    PhonebookUnitOfWork.PhonebookRepository.Insert(model.GetPhoneBookFrom());
                }
                await PhonebookUnitOfWork.SaveAsync();
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [System.Web.Mvc.HttpGet]
        [Route("GetEntries")]
        [ResponseType(typeof(IEnumerable<EntryModel>))]
        public async Task<IHttpActionResult> GetEntries()
        {
            return Ok(await Task.FromResult(PhonebookUnitOfWork.EntryRepository.Get()
                .Select((x => x.GetEntryModelFrom()))));
        }

        [System.Web.Mvc.HttpGet]
        [Route("GetPhoneBookEntriesForPhoneBook")]
        [ResponseType(typeof(IEnumerable<EntryModel>))]
        public async Task<IHttpActionResult> GetPhoneBookEntriesForPhoneBook(int phoneId)
        {
            if (phoneId  <= 0)
                throw new InvalidOperationException($"{nameof(phoneId)} cannot be less than or equal to zero");

            return Ok(await Task.FromResult(PhonebookUnitOfWork.EntryRepository.Get().Where(x => x.PhoneBookId == phoneId)
                .Select(x => x.GetEntryModelFrom())));
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Http.Route("GetPhoneBookEntryById")]
        [ResponseType(typeof(EntryModel))]
        public async Task<IHttpActionResult> GetPhoneBookEntryById(int id)
        {
            if (id <= 0)
                throw new ArgumentNullException($"{nameof(id)} cannot be empty");

            return Ok(await Task.FromResult(PhonebookUnitOfWork.EntryRepository.GetByID(id)
                .GetEntryModelFrom()));
        }

        [System.Web.Mvc.HttpGet]
        [Route("GetPhoneEntriesByPhonebookName")]
        [ResponseType(typeof(IEnumerable<EntryModel>))]
        public async Task<IHttpActionResult> GetPhoneEntriesByPhonebookName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException($"{nameof(name)} cannot be empty");

            return Ok(await Task.FromResult(PhonebookUnitOfWork.EntryRepository.Get(x => x.PhoneBook.Name.ToUpper() == name.ToUpper())
                .Select(x => x.GetEntryModelFrom())));
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Http.Route("SavePhoneBookEntry")]
        [ResponseType(typeof(PhoneBookModel))]
        public async Task<IHttpActionResult> SavePhoneBookEntry(EntryModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    //save the changes to the data
                    var existingModel =
                        PhonebookUnitOfWork.EntryRepository.GetByID(model.Id);

                    existingModel.Name = model.Name;

                    PhonebookUnitOfWork.EntryRepository.Update(existingModel);
                }
                else
                {
                    PhonebookUnitOfWork.EntryRepository.Insert(model.GetEntryFrom());
                }
                await PhonebookUnitOfWork.SaveAsync();
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("Search")]
        [ResponseType(typeof(IEnumerable<EntryModel>))]
        public async Task<IHttpActionResult> Search(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                throw new InvalidOperationException($"{nameof(searchString)} cannot be empty");

            SearchResult resulsts = new SearchResult()
            {
                Entries = await Task.FromResult(PhonebookUnitOfWork.EntryRepository.Get(x =>
              x.Name.Contains(searchString.ToUpper()) || x.PhoneNumber.Contains(searchString))
                .Select(x => x.GetEntryModelFrom())),
                PhoneBooks = await Task.FromResult(PhonebookUnitOfWork.PhonebookRepository.Get(x => x.Name.ToUpper().Contains(searchString.ToUpper()))
                .Select(x=>x.GetPhoneBookModelFrom()))
            };

            return Ok(resulsts);
        }
    }
}
