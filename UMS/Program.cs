using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
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
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Data file Attached with program");
            Console.WriteLine();
            Console.WriteLine("If not exist no problem I will Create new text:) ");
            Console.WriteLine();
            Console.WriteLine("Please Enter any key to Continue..."); Console.ReadKey();
            
           
            MainMenu menu = new MainMenu();
             string filePath = @"..\..\..\Data.txt";
              menu.LoadData(filePath);
            menu.ShowMenu();


            
        }
        
        
    }
}
