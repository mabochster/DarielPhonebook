using Dariel.Mahlatse.PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dariel.Mahlatse.PhoneBook.Dal.Repositories.Database
{
    public class PhoneBookDataContext : DbContext
    {
        public virtual DbSet<Models.PhoneBook> PhoneBooks { get; set; }
        public virtual DbSet<Models.Entry> Entries { get; set; }

        public PhoneBookDataContext()
            : this("name=DefaultConnectionString")
        {
        }



        public PhoneBookDataContext(string connectionName)
            : base(connectionName)
        {
            Database.CreateIfNotExists();//create the database if it does not exist

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
