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
            Console.WriteLine("---------------STATIC METHOD-----------------");
            //Program.StaticMethod(); //commented to avoid junk display
            Console.WriteLine("---------------------------------------------");

            //INSTANCE METHOD
            Console.WriteLine("---------------INSTANCE METHOD-----------------");
            Program p = new Program();
            p.InstanceMethod();
            Console.WriteLine("-----------------------------------------------");

            //METHOD PARAMETERS
            Console.WriteLine("---------------METHOD PARAMETERS-----------------");
            int out_int;
            int ref_int = 3;
            //Program.MethodParameters(1, out out_int, ref ref_int); //params optional
            Program.MethodParameters(1, out out_int, ref ref_int, 1, 2, 3, 4, 5, 6); //any NUMBER/STRING of arguments can be passed
            Console.WriteLine("Out value {0}", out_int);
            Console.WriteLine("Ref value {0}", ref_int);
            Console.WriteLine("-------------------------------------------------");

            //NAMESPACE
            Console.WriteLine("---------------NAMESPACE-----------------");
            PAMA.ClassA.Print();
            PAMB.ClassA.Print();
            Console.WriteLine("--------------------------------------------");

            //CONSTRUCTORS (static (class.) /instance (obj.) call demo is shown below)
            Console.WriteLine("---------------CONSTRUCTORS-----------------");
            SampleConstructorClass obj = new SampleConstructorClass();
            Console.WriteLine(SampleConstructorClass._Greeting + " " + obj._firstname + " " + obj._lastname);
            Console.WriteLine("--------------------------------------------");

            //22. INHERITANCE
            Console.WriteLine("---------------INHERITANCE-----------------");
            SampleMultiLevelInheritance der = new SampleMultiLevelInheritance();
            Console.WriteLine(der._extra);

            //2nd way of calling the base class Print
            ((SampleDerived)der).Print();

            //3rd way of calling the base class Print
            SampleDerived baseder = new SampleMultiLevelInheritance();
            baseder.Print();
            Console.WriteLine("--------------------------------------------");

            //23. POLYMORPHISM
            Console.WriteLine("---------------POLYMORPHISM-----------------");
            SampleBase1[] base1 = new SampleBase1[3];
            base1[0] = new SampleBase1();
            base1[1] = new SampleDerived1();
            base1[2] = new SampleDerived2();//prints from base if either override is not given (OR) if a Print method is not defined.
            foreach (var item in base1)
            {
                item.Print();
            }
            Console.WriteLine("--------------------------------------------");

            //24.METHOD OVERRIDING VS METHOD HIDING
            Console.WriteLine("---------------METHOD OVERRIDING VS METHOD HIDING-----------------");
            BaseClass B = new DerivedClass();
            B.Print();
            Console.WriteLine("------------------------------------------------------------------");

            //25.METHOD OVERLOADING : Return Type & Params cannot be considered in the signature of a method during overloading

            //26,27. PROPERTIES
            Console.WriteLine("---------------PROPERTIES-----------------");
            SampleProperties oprop = new SampleProperties();
            oprop.Name = "Xavier";
            oprop.City = "Toronto";
            Console.WriteLine("Name {0} City {1}", oprop.Name, oprop.City);
            Console.WriteLine("------------------------------------------");

            //28,29. STRUCTS
            Console.WriteLine("---------------STRUCTS-----------------");
            SampleStruct ostruct = new SampleStruct
            {
                _Name = "Ruben",
                Age = 41
            };
            Console.WriteLine("Struct 1 : Name {0} Age {1}", ostruct._Name, ostruct.Age);

            //copy one struct into another
            SampleStruct ostruct2 = ostruct;
            ostruct2._Name = "Roy";
            Console.WriteLine("Struct 2 : Name {0} Age {1}", ostruct2._Name, ostruct2.Age);

            //copy one class into another
            SampleProperties oprop2 = oprop;
            oprop2.City = "New York";
            Console.WriteLine("Class 1 : Name {0} City {1}", oprop.Name, oprop.City);
            Console.WriteLine("Class 2 : Name {0} City {1}", oprop2.Name, oprop2.City);

            Console.WriteLine("---------------------------------------");

            //30. INTERFACE
            Console.WriteLine("---------------INTERFACE-----------------");
            SampleInterfaceImplement imp = new SampleInterfaceImplement();
            imp.Print1();
            imp.Print2();

            /*
            VERY VERY IMPORTANT : 
            We cannot create an instance of an interface, 
            but an interface reference variable can point to a derived class object
            */
            //ISampleInterface2 imp2 = new ISampleInterface2();
            ISampleInterface2 imp2 = new SampleInterfaceImplement();
            imp2.Print2();
            Console.WriteLine("---------------------------------------");

            //31. EXPLICIT INTERFACE
            Console.WriteLine("---------------EXPLICIT INTERFACE-----------------");
            SampleExplicitInterface expint = new SampleExplicitInterface();
            //Way 1 of calling : CALLED BY INTERFACE REFERENCE VARIABLE
            ((I1)expint).Print();
            ((I2)expint).Print();

            //Way 2 of calling : CALLED BY INTERFACE REFERENCE VARIABLE
            I1 i1 = new SampleExplicitInterface();
            i1.Print();
            I2 i2 = new SampleExplicitInterface();
            i2.Print();

            expint.Print();//default implementation (implicit implementation) : CALLED BY CLASS REFERENCE VARIABLE
            Console.WriteLine("--------------------------------------------------");

            //32. ABSTRACT
            Console.WriteLine("----------------------ABSTRACT--------------------");
            SampleImplementAbstractClass abs = new SampleImplementAbstractClass();
            abs.AbstractPrint();
            abs.Print();
            Console.WriteLine("--------------------------------------------------");

            //35. MULTIPLE CLASS INHERITANCE USING INTERFACE
            Console.WriteLine("----------------------MULTIPLE CLASS INHERITANCE USING INTERFACE--------------------");
            AB ab = new AB();
            ab.PrintA();
            ab.PrintB();
            Console.WriteLine("------------------------------------------------------------------------------------");

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

    #region PROPERTIES - ENCAPSULATION
    public class SampleProperties
    {
        private string _Name;
        public string Name
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Name cannot be null or empty");
                }
                this._Name = value;
            }
            get
            {
                return string.IsNullOrEmpty(this._Name) ? "Name cannot be null or empty" : this._Name;
            }
        }

        //Automatic Properties used when no validation required on data
        public string City { get; set; } //no private declaration required

    }
    #endregion

    #region STRUCT
    public struct SampleStruct
    {
        public string _Name { get; set; }
        public int Age { get; set; }
    }
    #endregion

    #region INTERFACE

    public interface ISampleInterface0
    {
        void Print0();
    }
    public interface ISampleInterface1 : ISampleInterface0 //interface implementing an interface
    {
        void Print1();
    }

    public interface ISampleInterface2
    {
        void Print2();
    }

    public class SampleInterfaceImplement : ISampleInterface1, ISampleInterface2 //multiple interface implementation
    {
        public void Print0()
        {
            Console.WriteLine("Interface Print 0 method called");
        }

        public void Print1()
        {
            Console.WriteLine("Interface Print 1 method called");
        }

        public void Print2()
        {
            Console.WriteLine("Interface Print 2 method called");
        }
    }
    #endregion

    #region EXPLICIT INTERFACE
    interface I1
    {
        void Print();
    }

    interface I2
    {
        void Print();
    }

    interface I3 //to exhibit default implementation (mixture of explicit & implicit implementation)
    {
        void Print();
    }

    class SampleExplicitInterface : I1, I2, I3
    {
        void I1.Print()
        {
            Console.WriteLine("Print from I1");
        }

        void I2.Print()
        {
            Console.WriteLine("Print from I2");

        }

        public void Print()
        {
            Console.WriteLine("Print from I3");
        }
    }
    #endregion

    #region ABSTRACT
    public abstract class SampleAbstractClass
    {
        public abstract void AbstractPrint();

        public void Print()
        {
            Console.WriteLine("Print from NormalPrint method");
        }
    }

    public class SampleImplementAbstractClass : SampleAbstractClass
    {
        public override void AbstractPrint()
        {
            Console.WriteLine("Print from AbstractPrint method");
        }
    }
    #endregion

    #region MULTIPLE CLASS INHERITANCE USING INTERFACE

    interface IA
    {
        void PrintA();
    }
    public class A : IA
    {
        public void PrintA()
        {
            Console.WriteLine("A");
        }
    }

    interface IB
    {
        void PrintB();
    }
    public class B : IB
    {
        public void PrintB()
        {
            Console.WriteLine("B");
        }
    }

    class AB : IA, IB
    {
        A a = new A();
        B b = new B();
       
        public void PrintA()
        {
            a.PrintA();
        }

        public void PrintB()
        {
            b.PrintB();
        }
    }

    #endregion

}
