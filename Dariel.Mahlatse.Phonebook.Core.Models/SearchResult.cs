using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dariel.Mahlatse.Phonebook.Core.Models
{
    public class SearchResult
    {
        public IEnumerable<PhoneBookModel> PhoneBooks { get; set; }
        public IEnumerable<EntryModel> Entries { get; set; }
    }
}
