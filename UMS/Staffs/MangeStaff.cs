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

namespace UMS.Staffs
{
    [DataContract(IsReference = true)]
    internal class MangeStaff
    {
        [DataMember]
        private static int idCounter = 1;
        [DataMember]
        public List<Staff> Staffs { get; set; } = new List<Staff>();
        //add staff
        public void AddStaff()
        {
            Console.Write("No of staff are : ");
            int no = Function.PIntInput();
            for (int i = 0; i < no; i++) 
            {
                Console.Write("Enter Staff Name: ");
                string name = Console.ReadLine();

                if (Staffs.Any(s => s.Name == name))
                {
                    Console.WriteLine("Staff member with the same name already exists.");

                    Console.ReadKey();
                    return;
                }
                if (!IsValidString(name))
                {
                    Console.WriteLine("Invalid input for Name of college.");
                    Console.ReadKey();
                    return;
                }
                Console.Write("Enter Position: ");
                string position = Console.ReadLine();
                Console.Write("Enter Salary: ");
                decimal salary = Function.PDecimalinput();
                var staff = new Staff(idCounter++, name, position, salary);
                Staffs.Add(staff);
                Console.WriteLine($"Name '{name}' With ID '{staff.ID}'.");
                Console.WriteLine("Staff added successfully.");
            }

            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //show staff
        public void ShowStaff()
        {
            if (Staffs.Count() == 0)
            {
                Console.WriteLine("\nIt's Empty");
                Console.ReadKey();
                return;
            }
            else
            {
                foreach (var staff in Staffs)
                {
                    Console.WriteLine("<<<<>>>>");
                    Console.WriteLine($"ID: {staff.ID},Name: {staff.Name}\n,Position: {staff.Position}\n,Salary: {staff.Salary}");
                    var name=staff.StaffCol.Name;
                    if (staff.StaffCol.Name!=null)
                    {
                        Console.WriteLine($" College name is {staff.StaffCol.Name}");
                    }
                    
                    Console.WriteLine("<<<<>>>>");
                }
            }
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();

        }
        //update staff
        public void UpdateStaff()
        {
            Console.Write("Enter Staff ID : ");
            int id = Function.PIntInput();

            var staff = GetStaffById(id);
            if (staff == null)
            {
                Console.WriteLine("Staff not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Enter New Name:{leave blank to keep current}");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName)) staff.Name = newName;
            Console.Write("Enter New Position: ");
            string newPosition = Console.ReadLine();
            Console.Write("Enter New Salary: ");
            decimal newSalary = Function.PDecimalinput();
            staff.Position = newPosition;
            staff.Salary = newSalary;

            Console.WriteLine("Staff updated successfully.");
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //get staff by id 
        public Staff GetStaffById(int id)
        {
            return Staffs.FirstOrDefault(st => st.ID == id);
        }
        //delete staff
        public void DeleteStaff()
        {
            Console.Write("Enter Staff ID : ");
            int id = Function.PIntInput();

            var staff = GetStaffById(id);
            if (staff == null)
            {
                System.Console.WriteLine("Staff not found.");
                Console.ReadKey();
                return;
            }
            Staffs.Remove(staff);
            Console.WriteLine("Staff deleted successfully.");
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
        }
        //// Sets the list of staff (used when loading data)
        public void SetStaff(List<Staff> staffs)
        {
            Staffs = staffs;
            if (Staffs.Count > 0)
            {
                idCounter = Staffs.Max(u => u.ID) + 1;
            }
        }
        //get all staffs
        public List<Staff> GetAllStaff()
        {
            return Staffs;
        }
        //assign staff and unassign staff to college
        public bool AssignStaff(College col)
        {
            Console.Write("Enter Staff ID: ");
            var id = Function.PIntInput();
            var staffmember = GetStaffById(id);

            if (col.ColStaff.Any(s => s.ID == staffmember.ID))
            {
                Console.WriteLine($"Department '{staffmember.Name}' is already assigned to this College.");
                Console.ReadKey();
                return false;
            }

            col.ColStaff.Add(staffmember);
            staffmember.StaffCol = col;
            Console.WriteLine($"Staff '{staffmember.Name}' assigned to College '{col.Name}' successfully.");
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
            return true;

        }

        public bool UnassignStaff(College col)
        {

            Console.Write("Enter Staff ID: ");
            var id = Function.PIntInput();
            var staffmember = GetStaffById(id);
            if (col.ColStaff.Any(s => s.ID == staffmember.ID))
            {

                col.ColStaff.Remove(staffmember);
                staffmember.StaffCol = null;

                Console.WriteLine($"Staff ' {staffmember.Name}' unassigned from College '{col.Name}' successfully.");
                Console.ReadKey ();
                return true;
            }
            Console.WriteLine("Staff not found in this college.");
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
            return false;

        }
       
        private bool IsValidString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

    }
}

