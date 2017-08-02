using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MessagesCore
{
    public class MessageService
    {
        public Message SaveMessage(Message message)
        {
            var client = new RestClient("http://localhost:51585");
            var request = new RestRequest("api/messages");
            request.AddBody(message);
            request.Method = Method.POST;
            return client.Execute<Message>(request).Data;
        }

        public void LoadMessages(ICollection<Message> messages, string filter="")
        {
            var client = new RestClient("http://localhost:51585");
            var request = new RestRequest("api/messages");
            request.AddQueryParameter("filter", filter);
            request.Method = Method.GET;
            var result = client.Execute<List<Message>>(request).Data;
            foreach (var m in result)
            {
                messages.Add(m);
            }
        }

        public List<Contact> LoadContacts(string filter = "")
        {
            var client = new RestClient("http://localhost:51585");
            var request = new RestRequest("api/contacts");
            request.AddQueryParameter("filter", filter);
            request.Method = Method.GET;
            var response = client.Execute<List<Contact>>(request);
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            return response.Data;
            //using (var c = new HttpClient())
            //{
            //    var response = c.GetAsync("http://localhost:51585/api/contacts").Result;
            //    var s = response.Content.ReadAsStringAsync().Result;         
            //    var content = response.Content.ReadAsStreamAsync().Result;
            //    var serializer = new XmlSerializer(typeof(List<Contact>));
            //    var result = (List<Contact>)serializer.Deserialize(content);
            //    return result;
            //}
        }

        public Contact SaveContact(Contact contact)
        {
            var client = new RestClient("http://localhost:51585");
            var request = new RestRequest("api/contacts");
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.POST;
            request.AddBody(contact);
            return client.Execute<Contact>(request).Data;
        }

        public void DeleteMessage(int id)
        {
            var client = new RestClient("http://localhost:51585");
            var request = new RestRequest("api/messages");
            request.AddQueryParameter("id", id.ToString());
            request.Method = Method.DELETE;
            client.Execute(request);
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
