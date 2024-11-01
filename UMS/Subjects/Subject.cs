using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UMS.Deparments;
using UMS.Students;

namespace UMS.Subjects
{
    [DataContract(IsReference = true)]
    public class Subject:Base
    {
        [DataMember]
        public int FullMark { get; set; }
        [DataMember]
        public Department SubDep { get; set; }

        public Subject(int id, string name, int fullMark,Department subdep)
        {
            ID = id;
            Name = name;
            FullMark = fullMark;
            SubDep = subdep;
            
        }
        public Subject(int id, string name, int fullMark)
        {
            ID = id;
            Name = name;
            FullMark = fullMark;
           

        }
        public Subject()
        {

        }
        public bool ValidateDepartmentAndCollege(Student student)
        {
            var studentDepartment = student.SubjectGrades.Select(sg => sg.Subject.SubDep).FirstOrDefault();
            if (studentDepartment != null && studentDepartment.DepCollege != SubDep.DepCollege)
            {
                Console.WriteLine($"Error: Subject {Name} belongs to department {SubDep.Name} which is assigned to a different college than student's department.");
                Console.ReadKey();
                return false;
            }

            return true;
        }

    }
}
