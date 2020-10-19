using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dariel.Mahlatse.PhoneBook.Models
{
    [Serializable]
    public class Entry : BasePhoneBook
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [ForeignKey("PhoneBook")]
        public int PhoneBookId { get; set; }

        public virtual PhoneBook PhoneBook { get; set; }
    }
}
