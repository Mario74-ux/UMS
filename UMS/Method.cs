using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS
{
    internal class Function
    {

        #region Function
        /// <summary>
        /// Ckecks if the input is 'int' or not.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static bool IsInt(string str)
        {
            return int.TryParse(str, out _);
        }

        /// <summary>
        /// Ckecks if the input is numeric ('double') or not.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static bool IsDouble(string str)
        {
            return double.TryParse(str, out _);
        }
        /// <summary>
        /// Ckecks if the input is numeric ('decimal') or not.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static bool IsDecimal(string str) {  return decimal.TryParse(str, out _);}



        // Int Inputs:------------------------------------------------------------------------------------------

        /// <summary>
        /// Forces the user to enter an 'int' input.
        /// </summary>
        /// <remarks>
        /// (Defencive Coding)
        /// </remarks>
        /// <returns></returns>
        static int IntInput()
        {
            string input = Console.ReadLine();

            while (!IsInt(input))
            {
                Console.WriteLine("Please, Enter an Integer Number");
                input = Console.ReadLine();
            }

            return int.Parse(input);
        }

        /// <summary>
        /// Forces the user to enter a +ve/-ve 'int' input except 'zero'.
        /// </summary>
        /// <remarks>
        /// (Defencive Coding)
        /// </remarks>
        /// <returns></returns>
        public static int NonZeroIntInput()
        {
            string input = Console.ReadLine();

            while (!IsInt(input) || int.Parse(input) == 0)
            {
                Console.WriteLine("Please, Enter a Nonzero Integer Number");
                input = Console.ReadLine();
            }

            return int.Parse(input);
        }

        /// <summary>
        /// Forces the user to enter a 'positive int' input ('zero' is excluded).
        /// </summary>
        /// <remarks>
        /// (Defencive Coding)
        /// </remarks>
        /// <returns></returns>
        public static int PIntInput()
        {
            string input = Console.ReadLine();

            while (!IsInt(input) || int.Parse(input) <= 0)
            {
                Console.WriteLine("Please, Enter a Positive Integer Number");
                input = Console.ReadLine();
            }

            return int.Parse(input);
        }

        // Double Inputs:-----------------------------------------------------------------------------------------

        /// <summary>
        /// Forces the user to enter a 'number' as an input.
        /// </summary>
        /// <remarks>
        /// (Defencive Coding)
        /// </remarks>
        /// <returns></returns>
        public static double DoubleInput()
        {
            string input = Console.ReadLine();

            while (!IsDouble(input))
            {
                Console.WriteLine("Please, Enter a Valid Number");
                input = Console.ReadLine();
            }

            return double.Parse(input);
        }

        /// <summary>
        /// Forces the user to enter a +ve 'number' as an input except 'zero'.
        /// </summary>
        /// <remarks>
        /// (Defencive Coding)
        /// </remarks>
        /// <returns></returns>
        public static double PDoubleInput()
        {
            string input = Console.ReadLine();

            while (!IsDouble(input) || double.Parse(input) <= 0)
            {
                Console.WriteLine("Please, Enter a Nonzero Number");
                input = Console.ReadLine();
            }

            return double.Parse(input);
        }
        public static double DoubleInputNOTMORE100()
        {
            string input = Console.ReadLine();

            while (!IsDouble(input) || double.Parse(input) > 100 || double.Parse(input) < 0)
            {
                Console.WriteLine("Please, Enter a Grade Between  {0 : 100}");
                input = Console.ReadLine();
            }

            return double.Parse(input);
        }
        // decimal Inputs:------------------------------------------------------------------------------------------

        /// <summary>
        /// Forces the user to enter an 'decimal' input.
        /// </summary>
        /// <remarks>
        /// (Defencive Coding)
        /// </remarks>
        /// <returns></returns>
        public static decimal PDecimalinput()
        {
            string input = Console.ReadLine();

            while (!IsDecimal(input) || decimal.Parse(input) <= 0)
            {
                Console.WriteLine("Please, Enter a Decimal Number");
                input = Console.ReadLine();
            }

            return decimal.Parse(input);
        }








        #endregion

    }
}
