using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UMS.Colleges;
using UMS.Deparments;
using UMS.Subjects;

namespace UMS.Students
{
    [DataContract(IsReference = true)]
    public class MangeStudent
    {
        [DataMember]
        private int IdCounter = 1;
        [DataMember]
        public List<Student> Students { get; set; } = new List<Student>();

        //add student
        public void AddStudent()
        {
            Console.WriteLine("No of student :");
            int no = Function.PIntInput();
            for (int i = 0; i < no; i++)
            {
                Console.Write("Enter Student Name: ");
                string name = Console.ReadLine();
                if (!IsValidString(name))
                {
                    Console.WriteLine("Invalid input for Name .");
                    Console.ReadKey();
                    return;
                }
                if (Students.Any(s => s.Name == name))
                {
                    Console.WriteLine("Student with the same name already exists.");
                    Console.ReadKey();
                    return;
                }
                var student = new Student(IdCounter++, name);
                Students.Add(student);
                Console.WriteLine("<<<<<>>>>>>");
                Console.WriteLine("Student added successfully.");
                Console.WriteLine($"Name: {student.Name} ,ID: {student.ID}");
                Console.WriteLine("<<<<<>>>>>>");
            }

            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //show all students
        public void ShowStudents()
        {
            if (Students.Count()==0)
            {
                Console.WriteLine("No students exist");

                Console.ReadKey();
                return ;
            }
            foreach (var student in Students)
            {
                Console.WriteLine("<<<<<>>>>>>");
                Console.WriteLine($"ID: {student.ID}, Name: {student.Name}");
                
                Console.WriteLine("<<<<<>>>>>>");
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey(); ;

        }
                                
        //get student by id
        public Student GetStudentById(int id)
        {
            return Students.FirstOrDefault(s => s.ID == id);
        }

        //update student
        public void UpdateStudent()
        {
            Console.Write("Enter Student ID : ");
            int id = Function.PIntInput();
            
            var student = GetStudentById(id);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                Console.ReadKey();
                return;
            }
            Console.Write("Enter New Name: ");
            string newName = Console.ReadLine();
            if (!IsValidString(newName))
            {
                Console.WriteLine("Invalid input for Name .");
                Console.ReadKey();
                return;
            }
            student.Name = newName;
            Console.WriteLine("Student updated successfully.");
            Console.ReadKey ();
        }
        //delete
        public void DeleteStudent()
        {
            Console.Write("Enter Student ID : ");
            int id = Function.PIntInput();

            var student = GetStudentById(id);
            
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                Console.ReadKey();
                return;
            }
            Students.Remove(student);
           Console.WriteLine("Student deleted successfully.");
            Console.ReadKey();
        }
        public void ShowStudentDetails()
        {
            Console.Write("Enter Student ID: ");
            int id = Function.PIntInput();

            var student = GetStudentById(id);
            if (student != null)
            {
                Console.WriteLine($"Student Name: {student.Name}");
                Console.WriteLine($"Passed Subjects: {student.GetPassedSubjects()}");
                Console.WriteLine($"Total Subjects: {student.SubjectGrades.Count}");
                
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //assign grade of subject to a student 
        public void AssignSubjectGradeToStudent(Subject sub)
        {
            Console.Write("No of srudents: ");
            int no=Function.PIntInput();
            for (int i = 0; i < no; i++) 
            {
                Console.Write("Enter Student ID: ");
                int id = Function.PIntInput();

                var student = GetStudentById(id);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    Console.ReadKey();
                    return;
                }
                Console.Write("Enter Grade: ");
                double g = Function.PDoubleInput();
                student.AssignSubjectGrade(sub, g);
            }
            
                                    }
        //// Sets the list of student (used when loading data)
        public void SetStudents(List<Student> student)
        {
            Students = student;
            if (Students.Count > 0)
            {
                IdCounter = Students.Max(u => u.ID) + 1;
            }
        }
        //get all Students
        public List<Student> GetAllStudents()
        {
            return Students;
        }

        // Method to get the top student 
        public void GetTopStudentsByDepartmentAndCollege()
        {
           List<College> colleges = new List<College>();

            foreach (var student in Students)
            {
                foreach (var subjectGrade in student.SubjectGrades)
                {
                    var department = subjectGrade.Subject.SubDep;
                    var college = department.DepCollege;  

                    if (!colleges.Contains(college))
                    {
                        colleges.Add(college);
                    }
                }
            }
             foreach (var college in colleges)
            {
                Console.WriteLine("^^^^^^^^^^^^^^^^^^^");
                Console.WriteLine($"College: {college.Name}");
                Console.WriteLine("^^^^^^^^^^^^^^^^^^^");

                // List of departments within this college
                List<Department> departmentsInCollege = college.Departments;

                Student topCollegeStudent = null;
                double highestCollegeTotalScore = 0;

                // Check each department in the current college
                foreach (var department in departmentsInCollege)
                {
                    Student topDepartmentStudent = null;
                    double highestDepartmentTotalScore = 0;
                 foreach (var student in Students)
                    {
                        double studentTotalScore = student.SubjectGrades
                            .Where(sg => sg.Subject.SubDep == department)
                            .Sum(sg => sg.Grade);

                        if (studentTotalScore > highestDepartmentTotalScore)
                        {
                            highestDepartmentTotalScore = studentTotalScore;
                            topDepartmentStudent = student;
                        }

                        if (studentTotalScore > highestCollegeTotalScore)
                        {
                            highestCollegeTotalScore = studentTotalScore;
                            topCollegeStudent = student;
                        }
                    }

                    // Print top student in the current department
                    if (topDepartmentStudent != null)
                    {
                        Console.WriteLine("<<<<>>>>>>>>");
                        Console.WriteLine($"Top student in department {department.Name}: {topDepartmentStudent.Name} with total score {highestDepartmentTotalScore}");
                        Console.WriteLine("<<<<>>>>>>>>");
                    }
                }

                // Print top student in the current college
                if (topCollegeStudent != null)
                {
                    Console.WriteLine("<<<<>>>>>>>>");
                    Console.WriteLine($"Top student in college {college.Name}: {topCollegeStudent.Name} with total score {highestCollegeTotalScore}");
                    Console.WriteLine("<<<<>>>>>>>>\n");
                }

                Console.WriteLine(); 
            }
        }
        private bool IsValidString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}

