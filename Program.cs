using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using PAMA = ProjectA.ModuleA;
using PAMB = ProjectA.ModuleB;

namespace NetCoreCsharpConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //STATIC METHOD
            Program.StaticMethod();

            //INSTANCE METHOD
            Program p = new Program();
            p.InstanceMethod();

            //METHOD PARAMETERS
            int out_int;
            int ref_int = 3;
            //Program.MethodParameters(1, out out_int, ref ref_int); //params optional
            Program.MethodParameters(1, out out_int, ref ref_int, 1, 2, 3, 4, 5, 6); //any NUMBER/STRING of arguments can be passed
            Console.WriteLine("Out value {0}", out_int);
            Console.WriteLine("Ref value {0}", ref_int);

            //NAMESPACE
            PAMA.ClassA.Print();
            PAMB.ClassA.Print();

            //CONSTRUCTORS (static (class.) /instance (obj.) call demo is shown below)
            SampleConstructorClass obj = new SampleConstructorClass();
            Console.WriteLine(SampleConstructorClass._Greeting + " " + obj._firstname + " " + obj._lastname);

            //22. INHERITANCE
            SampleMultiLevelInheritance der = new SampleMultiLevelInheritance();
            Console.WriteLine(der._extra);

            //2nd way of calling the base class Print
            ((SampleDerived)der).Print();

            //3rd way of calling the base class Print
            SampleDerived baseder = new SampleMultiLevelInheritance(); 
            baseder.Print();

            //23. POLYMORPHISM
            SampleBase1[] base1 = new SampleBase1[3];
            base1[0] = new SampleBase1();
            base1[1] = new SampleDerived1();
            base1[2] = new SampleDerived2();//prints from base if either override is not given (OR) if a Print method is not defined.
            foreach (var item in base1)
            {
                item.Print();
            }

            //24.METHOD OVERRIDING VS METHOD HIDING
            BaseClass B = new DerivedClass();
            B.Print();
        }

        public static void MethodParameters(int i, out int j, ref int k, params int[] numbers)
        {
            j = 2;
            k = 100;
            Console.WriteLine("Params array value {0}", numbers[3]);
        }

        public static void StaticMethod()
        {
            Console.WriteLine("--------------------------START-------------------------");
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
            string strintg = "100AB";
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

            //Switch
            int mynumber = 10;
            switch (mynumber)
            {
                case 10:
                case 50:
                    Console.WriteLine("Number is {0}", mynumber);
                    break;
                default:
                    break;
            }

            //WHILE
            while (mynumber < 20)
            {
                Console.Write("Number is {0} \n", mynumber);
                mynumber += 1;
            }

            //DO WHILE
            int thisnumber = 5;
            do
            {
                Console.WriteLine(thisnumber);
                thisnumber += 1;
            } while (thisnumber < 7);

            //FOR
            for (int k = 0; k < 5; k++)
            {
                Console.WriteLine(k);
            }

            //FOR EACH  
            foreach (var thisint in arrint)
            {
                Console.WriteLine("Array {0}", thisint);
            }

            //continue
            for (int k = 0; k < 5; k++)
            {
                if (k == 3)
                    continue;
                Console.Write(k + "\t");
            }
            Console.WriteLine("--------------------------END-------------------------");
        }

        public void InstanceMethod()
        {
            Console.WriteLine("Instance method call");
        }

    }

    /// <summary>
    /// This is RUBENS CONSTRUCTOR's SAMPLE
    /// </summary>
    public class SampleConstructorClass
    {
        public static string _Greeting = "Hello"; //kept as static since salutation is common & will save memory if declared as static
        public string _firstname;
        public string _lastname;
        public SampleConstructorClass() : this("No First Name", "No Last Name")
        {

        }

        public SampleConstructorClass(string FN, string LN)
        {
            this._firstname = FN;
            this._lastname = LN;
        }

        ~SampleConstructorClass()
        {
            //Clean up code
        }
    }

    #region INHERITANCE

    class SampleBase
    {
        public string _firstname = "FN";
        public string _lastname = "LN";
    }

    class SampleDerived : SampleBase
    {
        public string _extra = "Derived";

        public void Print()
        {
            Console.WriteLine(_firstname + " " + _lastname + ": Derived");
        }
    }

    class SampleMultiLevelInheritance : SampleDerived
    {
        public new string _extra = "Multi-level";

        public new void Print()
        {
            base.Print();       //1st way of calling the base class Print
            Console.WriteLine(_firstname + " " + _lastname + " " + ": Multi-level");
        }
    }

    #endregion

    #region POLYMORPHISM
    public class SampleBase1
    {
        public virtual void Print()
        {
            Console.WriteLine("Print from Base1");
        }
    }

    public class SampleDerived1 : SampleBase1
    {
        public override void Print()
        {
            Console.WriteLine("Print from Derived 1");
        }
    }

    public class SampleDerived2 : SampleBase1
    {
        //public void Print()
        //{
        //    Console.WriteLine("Print from Derived 2");
        //}
    }
    #endregion

    #region METHOD OVERRIDING VS METHOD HIDING
    public class BaseClass
    {
        public virtual void Print()
        {
            Console.WriteLine("Base Class print");
        }
    }

    public class DerivedClass : BaseClass
    {
        public new void Print()
        {
            Console.WriteLine("Derived Class print");
        }
    }
    #endregion

}
