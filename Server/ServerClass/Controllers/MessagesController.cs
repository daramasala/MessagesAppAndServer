using MessagesCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerClass.Controllers
{
    public class MessagesController : ApiController
    {
        // GET api/values
        public IEnumerable<Message> Get(string filter = null)
        {
            var s = new MessageService();
            var m = new List<Message>();
            s.LoadMessages(m, filter);
            return m;
        }

        // POST api/values
        public Message Post([FromBody]Message message)
        {
            var s = new MessageService();
            s.SaveMessage(message);
            return message;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            var s = new MessageService();
            s.DeleteMessage(id);
        }
    }
}
