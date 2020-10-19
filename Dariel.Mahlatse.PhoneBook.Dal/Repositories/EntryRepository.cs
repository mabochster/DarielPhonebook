using Dariel.Mahlatse.PhoneBook.Dal.Repositories.Database;
using Dariel.Mahlatse.PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dariel.Mahlatse.PhoneBook.Dal.Repositories
{
    public class EntryRepository : BaseRepository<Entry>
    {
        public EntryRepository(PhoneBookDataContext context) : base(context)
        {
        }
    }
}
