using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MessagesCore
{
    [DataContract]
    public class Contact
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [StringLength(50)]
        [Index(IsUnique = true)]
        [DataMember]
        public string Email { get; set; }
        public virtual List<Message> Messages { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Email})";
        }
    }
}
