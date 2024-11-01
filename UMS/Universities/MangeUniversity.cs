using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UMS.Universities
{
    [DataContract(IsReference =true)]
    public class MangeUniversity
    {
        [DataMember]
        public List<University> Universities { get; set; } = new List<University>();
        [DataMember]
        private int idCounter = 1;
        //add university
        public void AddUniversity()

        {
            Console.Write("No of Universities : ");
            int no = Function.PIntInput();
            for (int i = 1; i <= no; i++) 
            {
                Console.Write("Enter University Name: ");
                string name = Console.ReadLine();
                if (!IsUniqueUniversityName(name))
                {
                    Console.WriteLine(" This name already exists.");
                    Console.ReadKey();
                    return;
                }
                Console.Write("Address: ");
                string address = Console.ReadLine();
                if (!IsValidString(name) || !IsValidString(address))
                {
                    Console.WriteLine("Invalid input for university.");
                    Console.ReadKey();
                    return;
                }


                
                Console.Write("Max Colleges: ");
                int maxColleges = Function.PIntInput();
                var uni = new University(idCounter++, name, address, maxColleges);
                Universities.Add(uni);

                Console.WriteLine("\n<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                Console.WriteLine($"University '{name}' added successfully with ID : {uni.ID}");
                Console.WriteLine("<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n");
            }
            
            Console.ReadLine();
        }
        //view universities
        public void ViewUniversities()
        {
            if (Universities.Count == 0)
            {
                Console.WriteLine("\nIt's Empty");
                Console.ReadKey();
                return;
            }
            else

            {
                foreach (var uni in Universities)
                {
                    Console.WriteLine("\n<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

                    Console.WriteLine($"University Name: {uni.Name}");
                    Console.WriteLine($"University ID: {uni.ID}");
                    Console.WriteLine($"No of maximum colleges :{uni.MaxColleges}");

                    Console.WriteLine($"Numbers College Assigned: {uni.UniColleges.Count}");
                    
                    if (uni.UniColleges.Count > 0)
                    {
                        Console.WriteLine("Assigned Colleges is :");
                        foreach (var col in uni.UniColleges)
                        {
                            Console.WriteLine($"{col.Name}");
                        }
                    }
                    Console.WriteLine("<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>\n");

                }
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //get university
        public University GetUniversityById(int id)
        {
            return Universities.FirstOrDefault(u => u.ID == id);
        }
        //get all universities
        public  List<University> GetAllUniversities()
        {
            return Universities;
        }


        //getuniversity by id
        public void ShowUniversityById(int universityId)
        {
            var university = GetUniversityById(universityId);
            if (university != null)
            {

                Console.WriteLine("\n<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                Console.WriteLine($"\nUniversity ID:{university.ID}");
                Console.WriteLine($"University Name:{university.Name}");
                Console.WriteLine($"No of maximum colleges :{university.MaxColleges}");
                Console.WriteLine($"Numbers College Assigned: {university.UniColleges.Count}");
                if (university.UniColleges.Count > 0)
                {
                    Console.WriteLine("Assigned Colleges is :");
                    foreach (var col in university.UniColleges)
                    {
                        Console.WriteLine($"{col.Name}");
                    }
                }



            }
            else
            {
                Console.WriteLine("\nUniversity not found.");
                Console.ReadKey();
                
                return;
            }
            Console.WriteLine("Please enter any key to Continue...");
            Console.ReadKey();
            

        }
        //update university
        public void UpdateUniversity()
        {
            Console.WriteLine("Enter University ID:");
            int id = Function.PIntInput();
            ShowUniversityById(id);
            
            var university = GetUniversityById(id);
            if (university != null)
            {
                Console.WriteLine("Enter New Name:{leave blank to keep current}");
                string newName = Console.ReadLine();
                if (!IsUniqueUniversityName(newName))
                {
                    Console.WriteLine("University name must be unique.");
                    Console.ReadKey();
                    return;

                }
                Console.WriteLine("Enter New Address:{leave blank to keep current}");
                string newAddress = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName)) university.Name = newName;
                if (!string.IsNullOrEmpty(newAddress)) university.Address = newAddress;
                
                Console.WriteLine("Enter New Max Colleges:");
                int newMaxColleges = Function.PIntInput();
                                             
                university.MaxColleges = newMaxColleges;
                Console.WriteLine("\nUniversity updated successfully.");
            }
            else
            {
                Console.WriteLine("\nUniversity not found.");
            }
            Console.WriteLine("Please Enter any key to Continue...");Console.ReadKey();
            
        }
        //delete university
        public void DeleteUniversity()
        {
            Console.WriteLine("Enter University ID to Delete :");
            int id = Function.PIntInput();
            var university = GetUniversityById(id);
            if (university != null)
            {

                Universities.Remove(university);

                Console.WriteLine("\nUniversity Deleted successfully.");
            }
            else
            {
                Console.WriteLine("\nUniversity not found.");
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //show details of university
        public void ShowUniversityDetails()
        {
            Console.Write("Enter University ID: ");
            int id = Function.PIntInput();

            var university = GetUniversityById(id);
            if (university != null)
            {
                university.Evaluate();
                Console.WriteLine($"{university.GetClassification()}\nNumbers College Assigned: {university.UniColleges.Count}");
                
            }
            else
            {
                Console.WriteLine("University not found.");
                
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        // Sets the list of universities (used when loading data)
        public void SetUniversities(List<University> universities)
        {
            Universities = universities;
            if (Universities.Count > 0) 
            {
                idCounter = Universities.Max(u => u.ID) + 1;
            }
            
        }

        #region function
        // Methods to check input
        private bool IsValidString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        private bool IsUniqueUniversityName(string name)
        {
            return !Universities.Any(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));


        }
        #endregion
    }
}

