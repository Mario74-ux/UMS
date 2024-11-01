using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UMS.Colleges;
using UMS.Universities;

namespace UMS
{
    public class Evaluation
    {
        public void EvaluateUniversity(University university ) 
        {


            Console.WriteLine("\n<<<<<<<<<<>>>>>>>>>>");
            Console.WriteLine($"University ID :{university.ID}");
            university.Evaluate();
            Console.WriteLine($" Classification is :{university.GetClassification()}");
            Console.WriteLine("\n<<<<<<<<<<>>>>>>>>>>");



            Console.ReadLine();
        }
        public void EvaluateAllUniversities(List<University> universities)
        {


            foreach (var univer in universities)

            {
                Console.WriteLine("\n<<<<<<<<<<>>>>>>>>>>");
                Console.WriteLine($"University Name :{univer.Name}");
                univer.Evaluate();
                Console.WriteLine($" Classification is :{univer.GetClassification()}");
                Console.WriteLine("\n<<<<<<<<<<>>>>>>>>>>");
            }




            Console.ReadLine();
        }
        public void EvaluateCollege(College college)
        {


            Console.WriteLine("\n<<<<<<<<<<>>>>>>>>>>");
            Console.WriteLine($"college ID :{college.ID}");
            college.Evaluate();
            Console.WriteLine($" Classification is :{college.GetClassification()}");
            Console.WriteLine("\n<<<<<<<<<<>>>>>>>>>>");



            Console.ReadLine();
        }
        public void EvaluateAllColleges(List<College> colleges)
        {


            foreach (var Col in colleges)

            {
                Console.WriteLine("\n<<<<<<<<<<>>>>>>>>>>");
                Console.WriteLine($"<<<<<<<<<College Name :{Col.Name}>>>>>>>>>>>>>");
                Col.Evaluate();
                Console.WriteLine($" Classification is :{Col.GetClassification()}");
                Console.WriteLine("\n<<<<<<<<<<>>>>>>>>>>");
            }




            Console.ReadLine();
        }



    }
}
