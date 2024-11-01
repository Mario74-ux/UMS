using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UMS
{
    [DataContract(IsReference =true)]
    public enum Classification
    {
        A,
        B,
        C,
        D,
        E
    }
}
