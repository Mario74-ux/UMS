using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UMS.Deparments;
using UMS.Staffs;
using UMS.Universities;

namespace UMS.Colleges
{
    [DataContract(IsReference = true)]
    public class College:Base
    {
        [DataMember]
        public double Fees { get; set; }
        [DataMember]
        public List<Department> Departments { get; set; } = new List<Department>();
        [DataMember]
        public List<University>ColUni {  get; set; }= new List<University>();
        [DataMember]
        public List<Staff> ColStaff { get; set; }=new List<Staff>();
        [DataMember]
        public int noDepart {  get; set; }

        public College(int id, string name, double fees, int noDepart)
        {
            ID = id;
            Name = name;
            Fees = fees;
            this.noDepart = noDepart;
        }
        public College()
        {

        }

        
        //evaluate college
        public void Evaluate()
        {
            int totalStudents = 0;
            int passedStudents = 0;

            
                foreach (var department in Departments)
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
            

            if (totalStudents == 0)
            {
                Console.WriteLine($"College: {Name}");
                Console.WriteLine("No students to evaluate.");
                
                return;
            }

            Console.WriteLine($"College: {Name}");
            Console.WriteLine($"Total Students: {totalStudents}");
            Console.WriteLine($"Passed Students: {passedStudents}");
            Console.WriteLine($"Failed Students: {totalStudents - passedStudents}");
            Console.WriteLine($"Success Percentage: {((double)passedStudents / totalStudents) * 100}%");
            
        }
        //Classification college
        public Classification GetClassification()
        {
            double successRate = (double)Departments.Sum(d => d.GetSuccessRate()) / Departments.Count;

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
        

        //sucess rate in college
        public double GetSuccessRate()
        {
            int totalStudents = 0;
            int passedStudents = 0;

            foreach (var department in Departments)
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

            return totalStudents == 0 ? 0 : (double)passedStudents / totalStudents * 100;
        }
    }
}
