using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagesCore
{
    public class MessagesAppContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public MessagesAppContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MessagesAppContext>());
        }
    }
}
