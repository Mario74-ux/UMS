using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UMS
{
    [DataContract(IsReference =true)]
    public class Base
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
