using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UMS.Colleges;

namespace UMS.Universities
{
    [DataContract(IsReference = true)]
    public class University:Base
    {
        [DataMember]
        public List<College> UniColleges { get; set; } = new List<College>();
        [DataMember]
        public int MaxColleges { get; set; }
        [DataMember]
        public string Address {  get; set; }
        public University()
        {

        }

        public University(int id, string name, string address,int max)
        {
            ID = id;
            Name = name;
            Address = address;
            MaxColleges=max;
        }
        

        public void Evaluate()
        {
            int totalStudents = 0;
            int passedStudents = 0;

            foreach (var college in UniColleges)
            {
                foreach (var department in college.Departments)
                {
                    foreach (var student in department.Students)
                    {
                        totalStudents++;
                        if (student.SubjectGrades.All(sg => sg.Grade >= (sg.Subject.FullMark * 0.5))) 
                        {
                            passedStudents++;
                        }
                    }
                }
            }

            if (totalStudents == 0)
            {
                Console.WriteLine($" University:{Name}");
                Console.WriteLine("No students to evaluate.");
                return;
            }

            Console.WriteLine($"University: {Name}");
            Console.WriteLine($"Total Students: {totalStudents}");
            Console.WriteLine($"Passed Students: {passedStudents}");
            Console.WriteLine($"Failed Students: {totalStudents-passedStudents}");
            Console.WriteLine($"Success Percentage: {((double)passedStudents / totalStudents) * 100}%");
            
        }

        public Classification GetClassification()
        {
            double successRate = (double)UniColleges.Sum(c => c.GetSuccessRate()) / UniColleges.Count;

            if (successRate >= 80)
                return Classification.A;
            if (successRate >= 60)
                return Classification.B;
            if (successRate >= 40)
                return Classification.C;
            if (successRate >= 20)
                return Classification.D;
            return Classification.E;
        }



    }
}
