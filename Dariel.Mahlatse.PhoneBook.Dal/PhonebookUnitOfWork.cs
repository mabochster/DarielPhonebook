using Dariel.Mahlatse.PhoneBook.Dal.Repositories;
using Dariel.Mahlatse.PhoneBook.Dal.Repositories.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dariel.Mahlatse.PhoneBook.Dal
{
    public class PhonebookUnitOfWork : IDisposable
    {
        private readonly PhoneBookDataContext _phoneBookDataContext;
        #region repositories

        private PhonebookRepository _phoneBookReposotory;
        private EntryRepository _entryRepository;

        #endregion

        public PhonebookUnitOfWork(PhoneBookDataContext phoneBookDataContext)
        {
            if (phoneBookDataContext == null)
                throw new ArgumentNullException($"{nameof(phoneBookDataContext)} cannot be null");

            this._phoneBookDataContext = phoneBookDataContext;
            this._phoneBookDataContext.Configuration.ProxyCreationEnabled = false;

        }

        public PhonebookRepository PhonebookRepository
        {
            get
            {
                if (this._phoneBookReposotory == null)
                {
                    this._phoneBookReposotory = new PhonebookRepository(this._phoneBookDataContext);
                }

                return this._phoneBookReposotory;
            }
        }

        public EntryRepository EntryRepository
        {
            get
            {
                if (this._entryRepository == null)
                {
                    this._entryRepository = new EntryRepository(this._phoneBookDataContext);
                }

                return this._entryRepository;
            }
        }

        public void Save()
        {
            try
            {
                _phoneBookDataContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                IEnumerable<string> errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                string fullErrorMessage = string.Join(" ", errorMessages);

                // Combine the original exception message with the new one.
                string exceptionMessage = string.Concat(ex.Message, "The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _phoneBookDataContext.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                IEnumerable<string> errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                string fullErrorMessage = string.Join(" ", errorMessages);

                // Combine the original exception message with the new one.
                string exceptionMessage = string.Concat(ex.Message, "The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }


        #region disposed

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _phoneBookDataContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
