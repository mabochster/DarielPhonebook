using Dariel.Mahlatse.Phonebook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dariel.Mahlatse.PhoneBook.UI.AspNet.Models
{
    public class PhonebookEntryViewModel
    {
        public PhoneBookModel PhoneBook { get; set; }
        public EntryModel Entry { get; set; }
    }
}