using Dariel.Mahlatse.PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dariel.Mahlatse.Phonebook.Core.Models.ConversionModels
{
    public static class EntryConversionModel
    {
        public static Entry GetEntryFrom(this EntryModel entryModel)
        {
            Entry entity = new Entry();
            entity.Id = entryModel.Id;
            entity.PhoneBookId = entryModel.PhoneBookId;
            entity.Name = entryModel.Name;
            entity.PhoneNumber = entryModel.PhoneNumber;

            return entity;
        }
        public static EntryModel GetEntryModelFrom(this Entry entry)
        {
            EntryModel entityModel = new EntryModel();
            entityModel.Id = entry.Id;
            entityModel.Name = entry.Name;
            entityModel.PhoneBookId = entry.PhoneBookId;
            entityModel.PhoneNumber = entry.PhoneNumber;

            return entityModel;
        }
    }
}