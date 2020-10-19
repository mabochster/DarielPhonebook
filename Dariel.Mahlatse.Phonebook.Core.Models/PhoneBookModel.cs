using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dariel.Mahlatse.Phonebook.Core.Models
{
    public class PhoneBookModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please provide a valid Name")]
        [StringLength(30,ErrorMessage ="Please keep the name under 30 characters")]
        public string Name { get; set; }
        public List<EntryModel> Entries { get; set; }
    }
}