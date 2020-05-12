using Microsoft.VisualBasic.CompilerServices;
using System;

namespace NetCoreCsharpConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console Read/Write
            Console.WriteLine("Enter you name");
            string sName = Console.ReadLine();
            Console.WriteLine("Hello {0} {1}", sName, "boss");

            //In-built data types
            Console.WriteLine(int.MinValue);
            Console.WriteLine(int.MaxValue);

            //Escape characters
            Console.WriteLine("Hello \v Calvyn");
            Console.WriteLine(@"D:\Home\Ruben\Work\C#");

            //Ternary operator
            bool IsTrue = sName == "Calvyn" ? true : false;
            Console.WriteLine("Is Calvyn : {0}", IsTrue);

            //Nullable type
            int? i = null;
            i = 10;

            //Nullable type Explicit conversion
            int mycount;
            mycount = i.Value;
            mycount = (int)i;

            //Nullable type Coalescing operator
            int count;
            count = i ?? 0; //if i is null return zero
            Console.WriteLine("value of count: {0}", count);

            //Implicit Conversion
            int m = 100;
            float ft = m;
            Console.WriteLine("Float Value is {0}", ft);

            //Explicit Conversion
            float f = 12345678902123123123.12345F;
            int j = (int)f;                  //will not throw error
            //int j = Convert.ToInt32(f);    //will throw error
            Console.WriteLine("Integer Value is {0}", j);

            //Parse
            string strint = "100";
            int intstr = int.Parse(strint);
            Console.WriteLine("Integer to String using Parse {0}", intstr);

            //TryParse
            string strintg = "100";
            int oInt;
            bool result = int.TryParse(strintg, out oInt);
            Console.WriteLine("Result {0} : Integer {1}", result, oInt);

            //Arrays
            int[] arrint = new int[2];
            arrint[0] = 5;
            arrint[1] = 10;
            Console.WriteLine("Array values {0} {1}", arrint[0], arrint[1]);

            //Comments
            //SampleClass

            //Difference between && , &
            if (arrint[0] == 2 && 1 == 1)
            {
                Console.WriteLine("Hi");
            }
        }
    }

    /// <summary>
    /// This is my comment
    /// </summary>
    public class SampleClass
    { 
    }
}
