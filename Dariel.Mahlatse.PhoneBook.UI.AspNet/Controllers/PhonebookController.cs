
using Dariel.Mahlatse.Phonebook.Core.Models;
using Dariel.Mahlatse.Phonebook.Core.Models.ConversionModels;
using Dariel.Mahlatse.Phonebook.Core.Rest;
using Dariel.Mahlatse.PhoneBook.UI.AspNet.Models;
using Dariel.Mahlatse.PhoneBook.UI.AspNet.Properties;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dariel.Mahlatse.PhoneBook.UI.AspNet.Controllers
{

    public class PhonebookController : Controller
    {
        private readonly string _apiUrl;

        public PhonebookController()
        {
            _apiUrl = Settings.Default.PhoneBookApiUrl;
        }
        // GET: Phonebook
        [HttpGet]
        // GET: 
        public async Task<ActionResult> Index()
        {
            //gett s
            IRestResponse<IEnumerable<PhoneBookModel>> phonebookResponse = await RestHelper.Get<IEnumerable<PhoneBookModel>>($"{_apiUrl}/phonebook/GetPhoneBooks",
                new List<KeyValuePair<string, object>>() { });


            if (phonebookResponse.IsSuccessful)
                return View(phonebookResponse.Data);

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> EditPhonebook(int id = 0)
        {
            PhoneBookModel model = new PhoneBookModel()
            {
                Entries = new List<EntryModel>()
            };
            
            if (id == 0)
            {
                return View(model);
            }

            IRestResponse<PhoneBookModel> phonebookResponse = await RestHelper.Get<PhoneBookModel>($"{_apiUrl}/phonebook/getphonebookbyid",
                new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("id", id) });

            if (phonebookResponse.IsSuccessful)
            {
                model = phonebookResponse.Data;

                IRestResponse<IEnumerable<EntryModel>> entiresResponse = await RestHelper.Get<IEnumerable<EntryModel>>($"{_apiUrl}/phonebook/GetPhoneBookEntriesForPhoneBook",
                new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("phoneId", id) });

                if (entiresResponse.IsSuccessful)
                {
                    model.Entries = entiresResponse.Data.ToList();
                }
            }

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> EditPhonebook(PhoneBookModel viewModel)
        {
            if (ModelState.IsValid)
            {

                IRestResponse<PhoneBookModel> phonebookModelResponse = await RestHelper.Save<PhoneBookModel>($"{_apiUrl}/phonebook/savephonebook", viewModel);

                if (phonebookModelResponse.IsSuccessful)
                {
                    return RedirectToAction("index");
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> EditPhonebookEntry(int phonebookId, int entryId = 0)
        {
            PhonebookEntryViewModel model = new PhonebookEntryViewModel();
            try
            {
                IRestResponse<PhoneBookModel> phonebookResponse = await RestHelper.Get<PhoneBookModel>($"{Settings.Default.PhoneBookApiUrl}/phonebook/GetPhoneBookById",
                    new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("id", phonebookId) });


                if (phonebookResponse.IsSuccessful)
                {
                    model.PhoneBook = phonebookResponse.Data;

                    if (entryId > 0)
                    {
                        //get child value first
                        IRestResponse<EntryModel> entryResponse = await RestHelper.Get<EntryModel>($"{_apiUrl}/phonebook/GetPhoneBookEntryById",
                            new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("id", entryId) });

                        if (entryResponse.StatusCode == System.Net.HttpStatusCode.OK)//can use isSuccessful as well
                        {
                            model.Entry = entryResponse.Data;
                        }
                    }
                    else
                    {
                        model.Entry = new EntryModel()
                        {
                            PhoneBookId = phonebookId
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                //log error(no logging framework yet?????)
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPhonebookEntry(PhonebookEntryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IRestResponse<EntryModel> entitySaveResponse = await RestHelper.Save<EntryModel>($"{_apiUrl}/phonebook/SavePhoneBookEntry", viewModel.Entry);
                    //save the data to the database
                    //add message to success TempData
                    return RedirectToAction("EditPhonebook", new { id = entitySaveResponse.Data.PhoneBookId });
                }
                catch (Exception ex)
                {
                    //log the error
                    //issue saving the value
                    return View(viewModel);
                }
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Search(string searchstring)
        {
            SearchResult model = new SearchResult()
            {

            };
            if (ModelState.IsValid)
            {
                try
                {
                    IRestResponse<SearchResult> searchResultsResponse = await RestHelper.Get<SearchResult>($"{_apiUrl}/phonebook/Search",
                            new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("searchstring", searchstring) });
                    //save the data to the database
                    //add message to success TempData
                    if (searchResultsResponse.IsSuccessful)
                    {
                        model = searchResultsResponse.Data;

                    }
                }
                catch (Exception ex)
                {
                    //log the error
                    //issue saving the value
                }
            }
            else
            {

            }

            return PartialView("_SearchResults",model);
        }
    }
}