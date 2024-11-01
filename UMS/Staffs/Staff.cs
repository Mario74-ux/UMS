using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UMS.Colleges;
using UMS.Deparments;

namespace UMS.Staffs
{
    [DataContract(IsReference = true)]
    public class Staff : Base
    {

        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public decimal Salary { get; set; }
        [DataMember]
        public College StaffCol {  get; set; }  

        public Staff(int id, string name, string position, decimal salary)
        {
            ID = id;
            Name = name;
            Position = position;
            Salary = salary;
        }
        public Staff()
        {

        }
    }
}
