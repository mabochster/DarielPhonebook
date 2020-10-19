using Dariel.Mahlatse.PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dariel.Mahlatse.Phonebook.Core.Models.ConversionModels
{
    public static class PhoneBookConversionModel
    {
        public static PhoneBook.Models.PhoneBook GetPhoneBookFrom(this PhoneBookModel phoneBookModel)
        {
            PhoneBook.Models.PhoneBook phoneBook = new PhoneBook.Models.PhoneBook();
            phoneBook.Id = phoneBookModel.Id;
            phoneBook.Name = phoneBookModel.Name;

            return phoneBook;
        }
        public static PhoneBookModel GetPhoneBookModelFrom(this PhoneBook.Models.PhoneBook phoneBook)
        {
            PhoneBookModel phoneBookModel = new PhoneBookModel();
            phoneBookModel.Id = phoneBook.Id;
            phoneBookModel.Name = phoneBook.Name;

            return phoneBookModel;
        }
    }
}