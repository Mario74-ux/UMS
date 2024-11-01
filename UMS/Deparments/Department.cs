using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UMS.Colleges;
using UMS.Staffs;
using UMS.Students;
using UMS.Subjects;

namespace UMS.Deparments
{
    [DataContract(IsReference = true)]
    public class Department : Base
    {
        [DataMember]
        public List<Subject> DepSubjects { get; set; } = new List<Subject>();
        
        [DataMember]
        public List<Student> Students { get; set; } = new List<Student>();
        [DataMember]
        public College DepCollege { get; set; }

        public Department(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public Department()
        {
            
        }
        //add student to department 
        public void AddStudent(Student student)
        {
            if (!Students.Any(s => s.ID == student.ID)) 
            {
                Students.Add(student);
                Console.WriteLine($"Student '{student.Name}' added to Department '{student.StudentDep.Name}'");

            }
        }

       
        //suceess rate for department
        public double GetSuccessRate()
        {
            int totalStudents = 0;
            int passedStudents = 0;


            foreach (var student in Students)
            {
                totalStudents++;
                if (student.SubjectGrades.All(sg => sg.Grade >= (sg.Subject.FullMark * 0.5)))
                {
                    passedStudents++;
                }
            }


            return totalStudents == 0 ? 0 : (double)passedStudents / totalStudents * 100;
        }
        //evaluate Department
        public void Evaluate()
        {
            int totalStudents = 0;
            int passedStudents = 0;


            foreach (var student in Students)
            {
                
                
                    totalStudents++;
                    if (student.SubjectGrades.All(sg => sg.Grade >= (sg.Subject.FullMark * 0.5) ))
                    {
                        passedStudents++;
                    }
                
            }


            if (totalStudents == 0)
            {
                Console.WriteLine($"Department: {Name}");
                Console.WriteLine("No students to evaluate.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("<<<<>>>>");
            Console.WriteLine($"Department: {Name}");
            Console.WriteLine($"Total Students: {totalStudents}");
            Console.WriteLine($"Passed Students: {passedStudents}");
            Console.WriteLine($"Success Percentage: {((double)passedStudents / totalStudents) * 100}%");
            Console.WriteLine("<<<<>>>>");
            Console.ReadLine();
        }
        
    }
}