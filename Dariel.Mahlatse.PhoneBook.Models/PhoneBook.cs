using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dariel.Mahlatse.PhoneBook.Models
{
    [Serializable]
    public class PhoneBook : BasePhoneBook
    {

        ICollection<Entry> Entries { get; set; }
    }
}
