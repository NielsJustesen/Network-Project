using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProjectServer
{
    [DataContract]
    class Person
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string username { get; set; }

        [DataMember]
        public long orc { get; set; }

        [DataMember]
        public long goblin { get; set; }

        [DataMember]
        public long troll { get; set; }

    }
}
