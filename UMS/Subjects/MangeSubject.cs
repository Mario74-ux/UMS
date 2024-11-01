using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UMS.Colleges;
using UMS.Deparments;
using UMS.Students;
using UMS.Universities;

namespace UMS.Subjects
{
    [DataContract(IsReference = true)]
    public class MangeSubject
    {
        [DataMember]
        private static int idCounter = 1;
        [DataMember]
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        //add subject
        public void AddSubject()
        {
            Console.Write("No of Subjects : ");
            int no = Function.PIntInput();
            for (int i = 0; i < no; i++) 
            {
                Console.Write("Enter Subject Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Full Mark: ");
                int fullMark = Function.PIntInput();
                if (Subjects.Any(s => s.Name == name))
                {
                    Console.WriteLine("Subject with the same name already exists.");
                    return;
                }
                if (!IsValidString(name))
                {
                    Console.WriteLine("Invalid input for Name of Subject.");
                    Console.ReadKey();
                    return;
                }
                var sub = new Subject(idCounter++, name, fullMark);
                Subjects.Add(sub);

                Console.WriteLine($"Subject added successfully. With ID : {sub.ID}");
                Console.WriteLine("<<<<<>>>>>>");
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();

           
        }
        //show subjects
        public void ShowSubjects()
        {
            if (Subjects.Count > 0)
            {
                foreach (var subject in Subjects)
                {
                    Console.WriteLine("<<<<<>>>>>>");
                    Console.WriteLine($"ID subject: {subject.ID}, Name: {subject.Name}, Full Mark: {subject.FullMark}");
                    Console.WriteLine("<<<<<>>>>>>");
                }
            }
            else 
            {
                Console.WriteLine("no Subjects found");
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //update subject
        public void UpdateSubject()
        {
            Console.Write("Enter Subject ID : ");
            int id = Function.PIntInput();
            var subject = Subjects.FirstOrDefault(s => s.ID == id);
            if (subject == null)
            {
                Console.WriteLine("Subject not found.");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("Enter New Name:{leave blank to keep current}");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName)) subject.Name = newName;


            Console.Write("New full mark is :");
            int newFullmark = Function.PIntInput();
           subject.FullMark = newFullmark;
            Console.WriteLine("Subject updated successfully.");
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //delete subject
        public void DeleteSubject()
        {
            Console.Write("Enter Subject ID to Delete: ");
            int id = Function.PIntInput();
            var subject = Subjects.FirstOrDefault(s => s.ID == id);
            if (subject == null)
            {
                Console.WriteLine("Subject not found.");
                Console.ReadKey ();
                return;
            }
            Subjects.Remove(subject);
            Console.WriteLine("Subject deleted successfully.");
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //// Sets the list of subjects (used when loading data)
        public void Setsub(List<Subject> subjects)
        {
            Subjects = subjects;

            if (Subjects.Count > 0)
            {
                idCounter = Subjects.Max(u => u.ID) + 1;
            }
        }
        //get all subjects
        public List<Subject> GetAllSubjects()
        {
            return Subjects;
        }

        //get subject by id 
        public Subject GetSubjectById(int id)
        {
            return Subjects.FirstOrDefault(d => d.ID == id);
        }
        //assign and unassign
        public bool AssignSubject(Department department)
        {
            Console.Write("No of subject to assign:");
            int no=Function.PIntInput();
           
            Console.Write("Enter Subject ID: ");
            var id = Function.PIntInput();
            var sub = GetSubjectById(id);

            if (department.DepSubjects.Any(s => s.ID == sub.ID))
            {
                Console.WriteLine($"Subject '{sub.Name}' is already assigned to this Department.");
                return false;
            }

           
            department.DepSubjects.Add(sub);
            sub.SubDep=department;
            

            Console.WriteLine($"Department '{sub.Name}' assigned to Department '{department.Name}' successfully.");
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
            return true;
            

        }

        public bool UnassignSubject(Department dep)
        {
            Console.Write("Enter Subject ID: ");
            var id = Function.PIntInput();
            var sub = GetSubjectById(id);

            if (dep.DepSubjects.Any(s => s.ID == sub.ID))
            {

                dep.DepSubjects.Remove(sub);
                sub.SubDep = null;


                Console.WriteLine($"Subject '{sub.Name}' unassigned from Department '{dep.Name}' successfully.");
                return true;
            }
            Console.WriteLine("Department not found in this college.");
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
            return false;

        }
        #region fun
        private bool IsValidString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
        

        #endregion


    }
}
