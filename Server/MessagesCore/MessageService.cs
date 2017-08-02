using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MessagesCore
{
    public class MessageService
    {
        public void SaveMessage(Message message)
        {
            using (var db = new MessagesAppContext())
            {
                message.Date = DateTimeOffset.UtcNow;
                foreach (var c in message.Recipients)
                {
                    db.Contacts.Attach(c);
                }
                db.Messages.Add(message);
                db.SaveChanges();
            }
        }

        public void LoadMessages(ICollection<Message> messages, string filter="")
        {
            using (var db = new MessagesAppContext())
            {
                IQueryable<Message> q = db.Messages.Include("Recipients");
                if (!string.IsNullOrEmpty(filter))
                {
                    q = q.Where(m => m.Subject.Contains(filter));
                }
                foreach (var m in q)
                {
                    messages.Add(m);
                }
            }
        }

        public List<Contact> LoadContacts(string filter = "")
        {
            using (var db = new MessagesAppContext())
            {
                IQueryable<Contact> q = db.Contacts;
                if (!string.IsNullOrEmpty(filter))
                {
                    q = q.Where(c => c.Name.Contains(filter));
                }
                return q.ToList();
            }
        }

        public void SaveContact(Contact contact)
        {
            using (var db = new MessagesAppContext())
            {
                if (db.Contacts.Any(c=>c.Email == contact.Email))
                {
                    throw new ContactAlreadyExistsException();
                }
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
        }

        public void DeleteMessage(int id)
        {
            using (var db = new MessagesAppContext())
            {
                var message = db.Messages.Find(id);
                db.Messages.Remove(message);
                db.SaveChanges();
            }
        }

        public void DeleteMessage(Message message)
        {
            using (var db = new MessagesAppContext())
            {
                db.Messages.Attach(message);
                db.Messages.Remove(message);
                db.SaveChanges();
            }
        }
    }

    [Serializable]
    public class ContactAlreadyExistsException : Exception
    {
        public ContactAlreadyExistsException()
        {
        }

        public ContactAlreadyExistsException(string message) : base(message)
        {
        }

        public ContactAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ContactAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
