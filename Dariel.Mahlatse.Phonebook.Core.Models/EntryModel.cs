using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dariel.Mahlatse.Phonebook.Core.Models
{
    public class EntryModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
        public int PhoneBookId { get; set; }
        public PhoneBookModel PhoneBook { get; set; }

    }
}