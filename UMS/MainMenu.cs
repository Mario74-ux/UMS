using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UMS.Colleges;
using UMS.Deparments;
using UMS.Staffs;
using UMS.Students;
using UMS.Subjects;
using UMS.Universities;


namespace UMS
{
    internal class MainMenu
    {
        private MangeUniversity mangeUniversity = new MangeUniversity();

        private MangeCollege mangeCollege = new MangeCollege();
        private MangeDepartment mangeDepartment = new MangeDepartment();
        private MangeStaff mangeStaff = new MangeStaff();
        private MangeStudent mangeStudent = new MangeStudent();
        private MangeSubject mangeSubject = new MangeSubject();
        private Evaluation mangeEvaluation = new Evaluation();

        public void SaveData()
        {
            string filePath = @"..\..\..\Data.txt";
            DataContractSerializer serializer = new DataContractSerializer(typeof(DataContainer));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                DataContainer data = new DataContainer
                {
                    Universities = mangeUniversity.GetAllUniversities(),
                    Colleges = mangeCollege.GetAllColleges(),
                    Departments = mangeDepartment.GetAllDepartments(),
                    Subjects = mangeSubject.GetAllSubjects(),
                    Staffs = mangeStaff.GetAllStaff(),
                    Students = mangeStudent.GetAllStudents()
                };
                serializer.WriteObject(stream, data);
            }
        }

        public void LoadData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Data file not found. Starting with empty data.");
                Console.ReadKey();
                return;
            }

            DataContractSerializer serializer = new DataContractSerializer(typeof(DataContainer));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                DataContainer data = (DataContainer)serializer.ReadObject(stream);
                mangeUniversity.SetUniversities(data.Universities);
                mangeCollege.SetCol(data.Colleges);
                mangeDepartment.SetDep(data.Departments);
                mangeSubject.Setsub(data.Subjects);
                mangeStaff.SetStaff(data.Staffs);
                mangeStudent.SetStudents(data.Students);
            }
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                
                Console.WriteLine("\n--- University Mangment system---");
                Console.WriteLine("What is your Scope?");
                Console.WriteLine("1. Universities");
                Console.WriteLine("2. Colleges");
                Console.WriteLine("3. Departments");
                Console.WriteLine("4. Subjects");
                Console.WriteLine("5. Staff");
                Console.WriteLine("6. Students");
                Console.WriteLine("7. Student Evaluation System & Classification");
                Console.WriteLine("8. Save");
                Console.WriteLine("9. Reset system");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ManageUniversities();
                        break;
                    case "2":
                        ManageColleges();
                        break;
                    case "3":
                        ManageDepartments();
                        break;
                    case "4":
                        ManageSubjects();
                        break;
                    case "5":
                        ManageStaff();
                        break;
                    case "6":
                        ManageStudents();
                        break;
                    case "7":
                        Evaluation();
                        break;
                    case "8":
                        SaveData();
                        Console.WriteLine("save suceesfully");
                        Console.ReadKey();

                        break;
                    case "9":
                        Reset();
                        
                       Console.ReadKey();
                        break;
                         case "0":
                        return;
                    default:
                        Console.WriteLine("Check Input!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ManageUniversities()
        {
            while (true)
            {

                Console.WriteLine("\n--- Manage Universities ---");
                Console.WriteLine("1. Add University");
                Console.WriteLine("2. View Universities ");
                Console.WriteLine("3. Update University");
                Console.WriteLine("4. Delete University");
                Console.WriteLine("5. Show University Details by (ID)");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        mangeUniversity.AddUniversity();
                        break;
                    case "2":
                        mangeUniversity.ViewUniversities();
                        break;
                    case "3":
                        mangeUniversity.UpdateUniversity();
                        break;
                    case "4":

                        mangeUniversity.DeleteUniversity();
                        break;
                    case "5":
                        Console.WriteLine("Enter University ID:");
                        int id = Function.PIntInput();
                        mangeUniversity.ShowUniversityById(id);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Check Input!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ManageColleges()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n--- Manage Colleges ---");
                Console.WriteLine("1. Add College");
                Console.WriteLine("2. Show All Colleges");
                Console.WriteLine("3. Update College");
                Console.WriteLine("4. Delete College");
                Console.WriteLine("5. Show All Colleges Details in specific University ");
                Console.WriteLine("6. Assign College to University ");
                Console.WriteLine("7. Unassign College to University ");

                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        mangeCollege.AddCollege();
                        break;
                    case "2":
                        mangeCollege.ShowColleges();
                        break;
                    case "3":
                        mangeCollege.UpdateCollege();
                        break;
                    case "4":

                        mangeCollege.DeleteCollege();
                        break;
                    case "5":
                        mangeUniversity.ViewUniversities();
                        Console.Write("Enter ID Of University : ");
                        int uni = Function.PIntInput();
                        var universit = mangeUniversity.GetUniversityById(uni);
                        if (universit != null)
                        {
                            mangeCollege.ShowCollegeDetails(universit);
                        }
                        else
                        {
                            Console.WriteLine("Check input");
                            Console.ReadKey();
                        }
                        break;
                    case "6":
                        Console.Write("Enter University ID: ");
                        int uniid = Function.PIntInput();
                        var university = mangeUniversity.GetUniversityById(uniid);
                        if (university != null)
                        {
                            mangeCollege.AssignCollege(university);
                        }
                        else
                        {
                            Console.WriteLine("Check input");
                            Console.ReadKey();
                        }
                        break;
                    case "7":
                        Console.Write("Enter University ID: ");
                        int id = Function.PIntInput();
                        var uniass = mangeUniversity.GetUniversityById(id);
                        if (uniass != null)
                        {
                            mangeCollege.UnassignCollege(uniass);
                        }
                        else
                        {
                            Console.WriteLine("Check input");
                        }
                        break;

                    case "0":
                        return;
                    default:
                        Console.WriteLine("Check Input!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private void ManageDepartments()
        {
            while (true)
            {
                Console.WriteLine("\n--- Manage Departments ---");
                Console.WriteLine("1. Add Department");
                Console.WriteLine("2. Show All Departments");
                Console.WriteLine("3. Update Department");
                Console.WriteLine("4. Delete Department");
                Console.WriteLine("5. Show All Department Details in college");
                Console.WriteLine("6. Assign Departmrnt to College");
                Console.WriteLine("7. Unassign Departmrnt from College");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        mangeDepartment.AddDepartment();
                        break;
                    case "2":


                        mangeDepartment.ShowDepartments();
                        break;
                    case "3":
                        mangeDepartment.UpdateDepartment();
                        break;
                    case "4":

                        mangeDepartment.DeleteDepartment();
                        break;
                    case "5":
                        Console.Write("Enter College ID: ");
                        int colid = Function.PIntInput();
                        var col = mangeCollege.GetCollegeById(colid);
                        if (col != null)
                        {
                            mangeDepartment.ShowDepartmentDetails(col);
                        }
                        else
                        {
                            Console.WriteLine("Check inputs");
                            Console.ReadKey();
                        }
                        break;
                    case "6":
                        Console.Write("Enter College ID: ");
                        int colidD = Function.PIntInput();
                        var colD = mangeCollege.GetCollegeById(colidD);
                        if (colD != null)
                        {
                            mangeDepartment.AssignDepartment(colD);
                        }
                        else
                        {
                            Console.WriteLine("Check input");
                            Console.ReadKey();
                        }
                        break;

                    case "7":
                        Console.Write("Enter College ID: ");
                        int colidDD = Function.PIntInput();
                        var colDD = mangeCollege.GetCollegeById(colidDD);
                        if (colDD != null)
                        {
                            mangeDepartment.UnassignDepartment(colDD);
                        }
                        else
                        {
                            Console.WriteLine("Check input");
                            Console.ReadKey();
                        }
                        break;



                    case "0":
                        return;
                    default:
                        Console.WriteLine("Check Input!");
                        break;
                }
            }
        }
        private void ManageSubjects()
        {
            while (true)
            {
                Console.WriteLine("\n--- Manage Subjects ---");
                Console.WriteLine("1. Add Subject");
                Console.WriteLine("2. Show All Subjects");
                Console.WriteLine("3. Update Subject");
                Console.WriteLine("4. Delete Subject");
                Console.WriteLine("5. Assign Subject to Department");
                Console.WriteLine("6. Unassign Subject from Department");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        mangeSubject.AddSubject();
                        break;
                    case "2":
                        mangeSubject.ShowSubjects();
                        break;
                    case "3":

                        mangeSubject.UpdateSubject();
                        break;
                    case "4":

                        mangeSubject.DeleteSubject();
                        break;
                    case "0":
                        return;
                    case "5":
                        Console.Write("Enter Department ID: ");
                        int id = Function.PIntInput();
                        var Dep = mangeDepartment.GetDepartmentById(id);
                        if (Dep != null)
                        {
                            mangeSubject.AssignSubject(Dep);
                        }
                        else
                        {
                            Console.WriteLine("Check input");
                            Console.ReadKey();
                        }
                        break;

                    case "6":
                        Console.Write("Enter Department ID: ");
                        int idd = Function.PIntInput();
                        var D = mangeDepartment.GetDepartmentById(idd);
                        if (D != null)
                        {
                            mangeSubject.AssignSubject(D);
                        }
                        else
                        {
                            Console.WriteLine("Check input");
                            Console.ReadKey();
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void ManageStaff()
        {
            while (true)
            {
                Console.WriteLine("\n--- Manage Staff ---");
                Console.WriteLine("1. Add Staff");
                Console.WriteLine("2. Show Staff");
                Console.WriteLine("3. Update Staff");
                Console.WriteLine("4. Delete Staff");
                Console.WriteLine("5. Assign Staff to College");
                Console.WriteLine("6. Unassign Staff from College");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        mangeStaff.AddStaff();
                        break;
                    case "2":
                        mangeStaff.ShowStaff();
                        break;
                    case "3":
                        mangeStaff.UpdateStaff();
                        break;
                    case "4":

                        mangeStaff.DeleteStaff();
                        break;
                    case "5":

                        Console.Write("Enter College ID: ");
                        int colidD = Function.PIntInput();
                        var colD = mangeCollege.GetCollegeById(colidD);
                        if (colD != null)
                        {
                            mangeStaff.AssignStaff(colD);

                        }
                        else
                        {
                            Console.WriteLine("Check input");
                            Console.ReadKey();
                        }
                        break;

                    case "6":
                        Console.Write("Enter College ID: ");
                        int colidDD = Function.PIntInput();
                        var colDD = mangeCollege.GetCollegeById(colidDD);
                        if (colDD != null)
                        {
                            mangeStaff.UnassignStaff(colDD);

                        }
                        else
                        {
                            Console.WriteLine("Check input");
                            Console.ReadKey();
                        }
                        break;

                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void ManageStudents()
        {
            while (true)
            {
                Console.WriteLine("\n--- Manage Students ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Show All Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Show Student Details By ID");
                Console.WriteLine("6. Assign Grade to Student");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        mangeStudent.AddStudent();
                        break;
                    case "2":
                        mangeStudent.ShowStudents();
                        break;
                    case "3":

                        mangeStudent.UpdateStudent();
                        break;
                    case "4":
                        mangeStudent.DeleteStudent();
                        break;
                    case "5":

                        mangeStudent.ShowStudentDetails();
                        break;
                    case "6":
                        Console.Write("Subject ID:");
                        int id = Function.PIntInput();
                        var sub = mangeSubject.GetSubjectById(id);
                        if (sub != null)
                        {
                            mangeStudent.AssignSubjectGradeToStudent(sub);
                        }
                        else
                        {
                            Console.WriteLine("Subject not found");
                            Console.ReadKey();
                        }

                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        private void Evaluation()
        {
            while (true)
            {
                Console.WriteLine("Select Scope for Evaluation:");
                Console.WriteLine("1.Evaluate University");
                Console.WriteLine("2.Evaluate College");
                Console.WriteLine("3.Evaluate All Universities");
                Console.WriteLine("4.Evaluate All Colleges");
                Console.WriteLine("5.Top student for each Department and college");
                Console.WriteLine("0.Back");

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter University ID: ");
                        int uniid = Function.PIntInput();
                        var university = mangeUniversity.GetUniversityById(uniid);
                        if (university == null)
                        {
                            Console.WriteLine("No university exist with that ID");
                            Console.ReadKey();
                        }
                        else
                        {
                            mangeEvaluation.EvaluateUniversity(university);
                        }
                        break;
                    case "2":

                        Console.Write("Enter College ID: ");
                        int colid = Function.PIntInput();
                        var col = mangeCollege.GetCollegeById(colid);
                        if (col == null)
                        {
                            Console.WriteLine("No College exist with that ID");
                            Console.ReadKey();
                        }
                        else
                        {
                            mangeEvaluation.EvaluateCollege(col);
                        }
                        break;
                    case "3":
                        var universities = mangeUniversity.GetAllUniversities();
                        if (universities == null)
                        {
                            Console.WriteLine("No universities exist.");
                        }
                        else
                        {
                            mangeEvaluation.EvaluateAllUniversities(universities);
                        }


                        break;
                    case "4":
                        var Colleges = mangeCollege.GetAllColleges();
                        if (Colleges == null)
                        {
                            Console.WriteLine("No universities exist.");
                            Console.ReadKey();
                        }
                        else
                        {
                            mangeEvaluation.EvaluateAllColleges(Colleges);
                        }

                        break;
                    case "5":
                        if (mangeStudent.Students.Count() > 0)
                        {
                            mangeStudent.GetTopStudentsByDepartmentAndCollege();
                            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();

                        }

                        else
                        {
                            Console.WriteLine("Add students first");
                            Console.ReadKey();
                        }

                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;


                }
            }
        }

        private void Reset()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Are you sure ?");
                Console.WriteLine("1.Delete");
                Console.WriteLine("2.No");
                Console.WriteLine("0.Back");


                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        mangeUniversity.Universities.Clear();
                        mangeStaff.Staffs.Clear();
                        mangeDepartment.Departments.Clear();
                        mangeStudent.Students.Clear();
                        mangeCollege.Colleges.Clear();
                        mangeSubject.Subjects.Clear();
                        Console.WriteLine("Go To main menu and save then exist to keep changes and reset ID");
                        Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
                        break;
                    case "2":

                        Console.WriteLine("Ok no problem");
                        Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey(); ;
                        break;

                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;


                }

            }
        }
    } }



