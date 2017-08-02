using MessagesCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ServerClass.Controllers
{
    public class ContactsController : ApiController
    {
        public IEnumerable<Contact> Get(string filter = null)
        {
            var s = new MessageService();
            var m = new List<Contact>();
            var c = s.LoadContacts(filter);
            return c;
        }

        // POST api/values
        public Contact Post(Contact contact)
        {
            var s = new MessageService();
            s.SaveContact(contact);
            return contact;
        }
    }
}
