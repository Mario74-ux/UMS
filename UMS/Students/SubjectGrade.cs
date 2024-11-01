using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UMS.Subjects;

namespace UMS.Students
{
    [DataContract(IsReference = true)]
    public class SubjectGrade
    {
        [DataMember]
        public Subject Subject { get; set; }
        [DataMember]
        public double Grade { get; set; }
        public SubjectGrade()
        {

        }

        public SubjectGrade(Subject subject, double grade)
        {
            Subject = subject;
            Grade = grade;
        }
    }
}
