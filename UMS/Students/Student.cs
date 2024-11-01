using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UMS.Deparments;
using UMS.Subjects;

namespace UMS.Students
{
    [DataContract(IsReference = true)]
    public class Student:Base
    {

        [DataMember]
        public List<SubjectGrade> SubjectGrades { get; set; } = new List<SubjectGrade>();

        public Department StudentDep { get; set; }

        public Student()
        {

        }
        public Student(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public int GetPassedSubjects()
        {
            return SubjectGrades.Count(c => c.Grade >= (c.Subject.FullMark * 0.5)); 
        }
        
        //assign subject&grades &department
        public void AssignSubjectGrade(Subject subject, double grade)
        {
            if (StudentDep != null && subject.SubDep.ID != StudentDep.ID)
            {
                Console.WriteLine($"Student is already have subject from '{StudentDep.Name}' Department and can't assign another subjects from diffrent Department '{subject.SubDep.Name}'");
                Console.ReadKey();
                return;
            }
            // Validate  relationship between department and college
            if (!subject.ValidateDepartmentAndCollege(this))
            {
                return; 
            }


            if (SubjectGrades.Any(sg => sg.Subject.ID == subject.ID))
            {
                Console.WriteLine($"Subject '{subject.Name}' already has a grade assigned.");
                Console.ReadKey();
                return;
            }
            SubjectGrades.Add(new SubjectGrade(subject, grade));
            
            Console.WriteLine($"Assigned grade '{grade}' for Subject '{subject.Name}' to Student '{Name}'.");
            if (StudentDep == null) 
            { 
                StudentDep=subject.SubDep;
            }
            subject.SubDep.AddStudent(this);

            Console.ReadKey();
            
        }

         // Method to get total score
        public double GetTotalScore()
        {
            return SubjectGrades.Sum(sg => sg.Grade);
        }
    }
}
