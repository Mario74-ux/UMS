using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UMS.Universities;
using UMS.Colleges;
using UMS.Students;
using System.Runtime.Serialization;
using UMS.Deparments;

namespace UMS.Colleges
{
    [DataContract(IsReference = true)]
    public class MangeCollege

    {
        [DataMember]
        public  List<College> Colleges = new List<College>();
        [DataMember]
        private int idCounter = 1;
        //add college
        public void AddCollege()
        {

            Console.Write("No of Colleges : ");
            int no = Function.PIntInput();
            for (int i = 1; i <= no; i++) 
            {
                Console.Write("Name college is :");
                string namCol = Console.ReadLine();
                if (!IsValidString(namCol))
                {
                    Console.WriteLine("Invalid input for Name of college.");
                    Console.ReadKey();
                    return;
                }
                Console.Write("Fees is: ");
                double feesCol = Function.PDoubleInput();
                Console.Write("Max numbers of Department is: ");
                int maxDep = Function.PIntInput();

                var college = new College(idCounter++, namCol, feesCol, maxDep);
                Colleges.Add(college);
                Console.WriteLine($"College '{namCol}' With ID '{college.ID}'.");
                Console.WriteLine($"College added succeesfully ");
            }

            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();



        }
        //show college
        public void ShowColleges()
        {
            if (Colleges.Count() == 0)
            {
                Console.WriteLine("\nIt's Empty");
                Console.ReadKey();
                return;
            }
            else 
            {
                foreach (var college in Colleges)
                {
                    Console.WriteLine("<<<<>>>>");
                    Console.WriteLine($"College Name is : {college.Name}");
                    Console.WriteLine($"College ID is : {college.ID}");
                    Console.WriteLine($"Max numbers of Department : {college.noDepart}");

                    Console.WriteLine($"Fees: {college.Fees}");
                    Console.WriteLine($"No of assigned department: {college.Departments.Count()}");
                    if (college.Departments.Count > 0)
                    {
                        Console.WriteLine("Assigned Departments is :");
                        foreach (var dep in college.Departments)
                        {
                            Console.WriteLine($"{dep.Name}");
                        }
                    }
                    Console.WriteLine("<<<<>>>>");
                }


                
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //show each college in specific university
        public void ShowAColData()
        {
            Console.Write("Enter College ID: ");
            int collegeId = Function.PIntInput();
            var col = GetCollegeById(collegeId);
            if (col == null)
            {
                Console.WriteLine("\nIt's Empty");
                Console.ReadKey();
                return;
            }
            else
            {

                Console.WriteLine("<<<<>>>>");
                Console.WriteLine($"\n College ID: {col.ID}, Name: {col.Name}");
                Console.WriteLine($"No of Max Departments: {col.noDepart}, Fees: {col.Fees}");
                Console.WriteLine($"No of assigned department: {col.Departments.Count()}");
                if (col.Departments.Count > 0)
                {
                    Console.WriteLine("Assigned Departments is :");
                    foreach (var dep in col.Departments)
                    {
                        Console.WriteLine($"{dep.Name}");
                    }
                }
                Console.WriteLine("<<<<>>>>");

            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }

        //get college by id
        public College GetCollegeById(int id)
        {
            return Colleges.FirstOrDefault(c => c.ID == id);
        }

        //get all colleges
        public List<College> GetAllColleges()
        {
            return Colleges;
        }


        //update college
        public void UpdateCollege()
        {
            Console.Write("Enter ID Of College want to update Data : ");
            int id = Function.PIntInput();
            
            var college = GetCollegeById(id);
            if (college != null)

            {
                Console.WriteLine("Enter New Name:{leave blank to keep current}");
                string newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName)) college.Name = newName;
                Console.Write("Enter New Max Departments: ");
                int newMaxDepartments = Function.PIntInput();
                Console.Write("Enter a New Fees: ");
                double colFees = Function.PDoubleInput();
                college.Fees = colFees;

                college.noDepart = newMaxDepartments;
                Console.WriteLine("College updated sucessefully");
                                                                                                              
            }
            else
            {
                Console.WriteLine("College not found.");
                Console.ReadKey();
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //delete  college
        public void DeleteCollege()
        {
            Console.Write("Enter ID Of College : ");
            int id = Function.PIntInput();
            var college = GetCollegeById(id);
            if (college != null)
            {
                Colleges.Remove(college);
                Console.WriteLine("College Deleted");
                
            }
            else
            {
                Console.WriteLine("College not found.");
                
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //retun colleges in a university
        public static List<College> GetColleges(University uni)
        {
            return uni.UniColleges;
        }
        //show college details in a university
        public void ShowCollegeDetails(University university)
        {
            var cols=GetColleges(university);
            if (cols != null)
            {
                foreach (var col in cols)
                {
                    Console.WriteLine("\n<<<<>>>>");
                    Console.WriteLine($"College Name is : {col.Name}");
                    Console.WriteLine($"College ID is : {col.ID}");
                    Console.WriteLine($"Max numbers of Department : {col.noDepart}");

                    Console.WriteLine($"Fees: {col.Fees}");
                    Console.WriteLine($"No of assigned department: {col.Departments.Count()}");
                    if (col.Departments.Count > 0)
                    {
                        Console.WriteLine("Assigned Departments is :");
                        foreach (var dep in col.Departments)
                        {
                            Console.WriteLine($"{dep.Name}");
                        }
                    }
                    Console.WriteLine("<<<<>>>>");

                }
                
            }

           
            else
            {
                Console.WriteLine("Check Input!");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey(); ;
        }
        //assign and unassign college
        public bool AssignCollege(University uni)
        {
            Console.Write("Enter College ID: ");
            var id=Function.PIntInput();
            var col=GetCollegeById(id);

            if (uni.UniColleges.Any(c => c.ID == col.ID))
            {
                Console.WriteLine($"College '{col.Name}' is already assigned to this university.");
                Console.ReadLine();
                return false;
            }
            if (uni.UniColleges.Count >= uni.MaxColleges) 
            {
                Console.WriteLine("No of maximum college Reached!");
                Console.ReadLine();
                return false;
            }

            uni.UniColleges.Add(col);
            col.ColUni.Add(uni);
            Console.WriteLine($"College '{col.Name}' assigned to University '{uni.Name}' successfully.");
            Console.ReadLine();
            return true;
        }

        public bool UnassignCollege(University uni)
        {
            Console.Write("Enter College ID: ");
            var id = Function.PIntInput();
            var col = GetCollegeById(id);

            if (uni.UniColleges.Any(c => c.ID == col.ID))
            {
                uni.UniColleges.Remove(col);
                col.ColUni.Remove(uni);
                Console.WriteLine($"College '{col.Name}' unassigned from University '{uni.Name}' successfully.");
                Console.ReadLine();
                return true;
            }
            Console.WriteLine("College not found in this university.");
            Console.ReadLine();
            return false;

         }
        //// Sets the list of colleg (used when loading data)
        public void SetCol(List<College> coll)

        {
            this.Colleges = coll;
            if (Colleges.Count > 0)
            {
                idCounter = Colleges.Max(u => u.ID) + 1;
            }
        }
        #region function
        // Methods to check input
        private bool IsValidString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }


        
        #endregion
    }
}
