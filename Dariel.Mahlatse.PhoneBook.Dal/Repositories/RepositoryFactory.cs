using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dariel.Mahlatse.PhoneBook.Dal.Repositories
{
    public static class RepositoryFactory
    {
        public static TRepository Create<TRepository>(ContextTypes ctype) where TRepository : class
        {
            switch (ctype)
            {
                case ContextTypes.EntityFrameworkSource:
                    if (typeof(TRepository) == typeof(IPhoneBookRepository))
                    {
                        return new PhonebookRepository() as TRepository;
                    }
                    else if (typeof(TRepository) == typeof(IEntryRepository))
                    {
                        return new EntryRepository() as TRepository;
                    }
                    return null;
                case ContextTypes.XmlSource:
                    if (typeof(TRepository) == typeof(IEntryRepository))
                    {
                        return new EntryRepository() as TRepository;
                    }
                   else if (typeof(TRepository) == typeof(IPhoneBookRepository))
                    {
                        return new PhonebookRepository() as TRepository;
                    }
                    return null;
                default:
                    return null;
            }
        }
    }

}
