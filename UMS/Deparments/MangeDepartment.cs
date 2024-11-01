using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UMS.Colleges;
using UMS.Universities;
using UMS.Deparments;
using UMS.Students;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace UMS.Deparments
{
    [DataContract(IsReference = true)]
    internal class MangeDepartment
        
    {
        [DataMember]
        public  List<Department> Departments = new List<Department>();
        [DataMember]
        private int IdCounter = 1;
        //add department
        public void AddDepartment()
        {

            Console.Write("No of Departments : ");
            int no = Function.PIntInput();
            for (int i = 0; i < no; i++) 
            {
                Console.Write("Name Department is :");
                string namDep = Console.ReadLine();
                if (!IsValidString(namDep))
                {
                    Console.WriteLine("Invalid input for Name of Department.");
                    Console.ReadKey();
                    return;
                }
                var dep = new Department(IdCounter++, namDep);
                Departments.Add(dep);
                Console.WriteLine($"Department added succeesfully  ID:{dep.ID}");
            }

            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
         //show departments
        public void ShowDepartments( )
        {
            if (Departments.Count()==0)
            {
                Console.WriteLine("\nIt's Empty");
                Console.ReadKey();
                return;
            }
            else 
            {
                foreach (var department in Departments)
                {
                    Console.WriteLine("<<<<<>>>>>>");
                    Console.WriteLine($"ID Department: {department.ID}, Name: {department.Name} ");
                    Console.WriteLine("<<<<<>>>>>>");
                    
                }
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //get department by Id
        public Department GetDepartmentById(int id)
        {
            return Departments.FirstOrDefault(d => d.ID == id);
        }
        //update department
        public void UpdateDepartment()
        {
            Console.Write("Enter ID Of Department want to update Data : ");
            int id = Function.PIntInput();

            var Dep = GetDepartmentById(id);
            if (Dep != null)

            {
                Console.WriteLine("Enter New Name:{leave blank to keep current}");
                string newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName)) Dep.Name = newName;
                Console.WriteLine($"Department Updated succeesfully");

            }
            else
            {
                Console.WriteLine("Department not found.");
               
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();

        }
        //Delete departments
        public void DeleteDepartment()
        {
            Console.Write("Enter ID Of Department : ");
            int id = Function.PIntInput();
            var dep = GetDepartmentById(id);
            if (dep != null)
            {
                Departments.Remove(dep);
                Console.WriteLine("Department Deleted.");
                Console.ReadKey ();
                return;
            }
            else
            {
                Console.WriteLine("Department not found.");
                
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }


        public void ShowDepartmentDetails(College col)
        {

            if (col.Departments.Count > 0)
            {
                foreach (var dep in col.Departments)
                {
                    dep.Evaluate();
                }
            }
            else
            {
                Console.WriteLine("No departments assigned to that College yet!");
                Console.ReadKey();
                return;
            }


        }
        //assign&unassign department
        public bool AssignDepartment(College col)
        {
            Console.Write("Enter Department ID: ");
            var id = Function.PIntInput();
            var Dep = GetDepartmentById(id);
            if (Dep==null)
            {
                Console.WriteLine($"NO DEPARTMENT WITH THAT ID");
                Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
                return false;
            }

            if (col.Departments.Any(d => d.ID == Dep.ID))
            {
                Console.WriteLine($"Department '{Dep.Name}' is already assigned to this College.");
                Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
                return false;
            }
            
            if (col.Departments.Count>=col.noDepart)
            {
                Console.WriteLine("Maximum Department Reached!");
                Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
                return false;
            }

            col.Departments.Add(Dep);
            Dep.DepCollege= col;
            
            Console.WriteLine($"Department '{Dep.Name}' assigned to College '{col.Name}' successfully.");
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
            return true;
            
        }

        public bool UnassignDepartment(College col)
        {
            Console.Write("Enter Department ID: ");
            var id = Function.PIntInput();
            var dep = GetDepartmentById(id) ;

            if (col.Departments.Any(d => d.ID == dep.ID))
            {
                col.Departments.Remove(dep);
                dep.DepCollege=null;
                

                Console.WriteLine($"Department '{dep.Name}' unassigned from College '{col.Name}' successfully.");
                Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
                return true;
            }
            Console.WriteLine("Department not found in this college.");
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
            return false;

        }
        // Sets the list of depart (used when loading data)
        public void SetDep(List<Department> depart)
        {
            Departments = depart;
            if (Departments.Count > 0)
            {
                IdCounter = Departments.Max(u => u.ID) + 1;
            }
        }
        //get All departments at college
        public static List<Department> GetDepartments(College col)
        {
            return col.Departments;
        }
        //get all deparments
        public List<Department> GetAllDepartments()
        {
            return Departments;
        }

        #region function
        private bool IsValidString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
        //private bool IsUniqueDepName(College college, string name)
        //{

        //    return !college.Departments.Any(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        //}
        #endregion

    }
}

