using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UMS.Colleges;
using UMS.Deparments;
using UMS.Staffs;
using UMS.Students;
using UMS.Subjects;
using UMS.Universities;


namespace UMS
{
    [DataContract(IsReference = true)]
    public class DataContainer
    {
        [DataMember]
        public List<University> Universities { get; set; } = new List<University>();

        [DataMember]
        public List<College> Colleges { get; set; } = new List<College>();


        [DataMember]
        public List<Subject> Subjects { get; set; } = new List<Subject>();

        [DataMember]
        public List<Department> Departments { get; set; } = new List<Department>();

        [DataMember]
        public List<Staff> Staffs { get; set; } = new List<Staff>();

        [DataMember]
        public List<SubjectGrade> SubjectGrades { get; set; } = new List<SubjectGrade>();

        [DataMember]
        public List<Student> Students { get; set; } = new List<Student>();


    }
}
