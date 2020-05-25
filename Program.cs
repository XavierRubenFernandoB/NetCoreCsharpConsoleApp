using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using PAMA = ProjectA.ModuleA;
using PAMB = ProjectA.ModuleB;
using System.IO;
using System.Runtime.Serialization;
using System.Net.Http;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace NetCoreCsharpConsoleApp
{
    class Program

    {
        static void Main(string[] args)
        {
            //STATIC METHOD
            Console.WriteLine("---------------STATIC METHOD-----------------");
            //Program.StaticMethod(); //commented to avoid junk display

            //INSTANCE METHOD
            Console.WriteLine("---------------INSTANCE METHOD-----------------");
            Program p = new Program();
            p.InstanceMethod();

            //METHOD PARAMETERS
            Console.WriteLine("---------------METHOD PARAMETERS-----------------");
            int out_int;
            int ref_int = 3;
            //Program.MethodParameters(1, out out_int, ref ref_int); //params optional
            Program.MethodParameters(1, out out_int, ref ref_int, 1, 2, 3, 4, 5, 6); //any NUMBER/STRING of arguments can be passed
            Console.WriteLine("Out value {0}", out_int);
            Console.WriteLine("Ref value {0}", ref_int);

            //NAMESPACE
            Console.WriteLine("---------------NAMESPACE-----------------");
            PAMA.ClassA.Print();
            PAMB.ClassA.Print();

            //CONSTRUCTORS (static (class.) /instance (obj.) call demo is shown below)
            Console.WriteLine("---------------CONSTRUCTORS-----------------");
            SampleConstructorClass obj = new SampleConstructorClass();
            Console.WriteLine(SampleConstructorClass._Greeting + " " + obj._firstname + " " + obj._lastname);

            //22. INHERITANCE
            Console.WriteLine("---------------INHERITANCE-----------------");
            SampleMultiLevelInheritance der = new SampleMultiLevelInheritance();
            Console.WriteLine(der._extra);

            //2nd way of calling the base class Print
            ((SampleDerived)der).Print();

            //3rd way of calling the base class Print
            SampleDerived baseder = new SampleMultiLevelInheritance();
            baseder.Print();

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

            //24.METHOD OVERRIDING VS METHOD HIDING
            Console.WriteLine("---------------METHOD OVERRIDING VS METHOD HIDING-----------------");
            BaseClass B = new DerivedClass();
            B.Print();

            //25.METHOD OVERLOADING : Return Type & Params cannot be considered in the signature of a method during overloading

            //26,27. PROPERTIES
            Console.WriteLine("---------------PROPERTIES-----------------");
            SampleProperties oprop = new SampleProperties();
            oprop.Name = "Xavier";
            oprop.City = "Toronto";
            Console.WriteLine("Name {0} City {1}", oprop.Name, oprop.City);

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

            //32. ABSTRACT
            Console.WriteLine("----------------------ABSTRACT--------------------");
            SampleImplementAbstractClass abs = new SampleImplementAbstractClass();
            abs.AbstractPrint();
            abs.Print();

            //35. MULTIPLE CLASS INHERITANCE USING INTERFACE
            Console.WriteLine("----------------------MULTIPLE CLASS INHERITANCE USING INTERFACE--------------------");
            AB ab = new AB();
            ab.PrintA();
            ab.PrintB();

            //36. DELEGATE
            Console.WriteLine("----------------------DELEGATE--------------------");
            MyDelegate mydelegate = new MyDelegate(DelegateMethod);
            DelegateMethod("Hello Delegate");

            //37,38 WHY DELEGATES
            Console.WriteLine("---------------------- WHY DELEGATES---------------------");
            //WITHOUT DELEGATES
            List<Employee> myEmpList = new List<Employee>();
            myEmpList.Add(new Employee { Name = "Xavier", Experience = 15 });
            myEmpList.Add(new Employee { Name = "Calvyn", Experience = 1 });

            Employee emp = new Employee();
            emp.PromoteEmployee(myEmpList);

            //WITH DELEGATES
            List<Employee1> myEmpList1 = new List<Employee1>();
            myEmpList1.Add(new Employee1 { Name = "Xavier", Experience = 15 });
            myEmpList1.Add(new Employee1 { Name = "Calvyn", Experience = 1 });

            IsPromotable isPromotable = new IsPromotable(Promote);
            Employee1.PromoteEmployee(myEmpList1, isPromotable);


            //39. MULTICAST DELEGATES
            Console.WriteLine("----------------------MULTICAST DELEGATES---------------------");
            dMultiCastDelegate dmulti1 = new dMultiCastDelegate(Method1);
            dMultiCastDelegate dmulti2 = new dMultiCastDelegate(Method2);
            dMultiCastDelegate dmulti3 = dmulti1 + dmulti2 - dmulti1;
            dmulti3();

            dMultiCastDelegate dmulti11 = new dMultiCastDelegate(Method1);
            dmulti11 += Method2;
            dmulti11 += Method3;
            dmulti11 -= Method2;
            dmulti11();

            Console.WriteLine("--------------------------------------------------------------");

            //40,41. EXCEPTION HANDLING
            Console.WriteLine("--------------------EXCEPTION HANDLING-----------------------");
            ExceptionHandling();

            Console.WriteLine("-------------CUSTOM EXCEPTION HANDLING-----------------------");
            //42. CUSTOM EXCEPTION HANDLING
            CustomeExceptionHandling();

            Console.WriteLine("-------------ENUMS-----------------------");
            //45,46,47. enums,Enums
            EnumSample();

            Console.WriteLine("-------------ACCESS MODIFIERS - Type members-----------------------");
            //48. ACCESS MODIFIERS - Type members
            TypeMembers();

            Console.WriteLine("-------------ACCESS MODIFIERS - Types------------------------------");
            //51. ACCESS MODIFIERS - Types
            Types();

            Console.WriteLine("----------------------ATTRIBUTES------------------------------");
            //52. 
            oldPrint();
            newPrint();

            Console.WriteLine("----------------------REFLECTIONS------------------------------");
            //53. 
            Reflections();

            Console.WriteLine("----------------------EARLY BINDING Vs LATE BINDING------------------------------");
            //55. 
            EarlyVsLateBinding();

            Console.WriteLine("----------------------GENERICS------------------------------");
            //56. 
            Generics();

            Console.WriteLine("----------------------Provide Implemention for ToString()------------------------------");
            //57,58. 
            ProvideImplementionForToString();

            Console.WriteLine("--------------------DifferenceBetweenConvertToStringAndToString-------------------");
            //59. 
            DifferenceBetweenConvertToStringAndToString();

            //CHECK FOR PARTIAL CLASSES AT NetCoreCsharpWebApp  (61,62,63)

            Console.WriteLine("----------------------INDEXERS & OVERLOADED INDEXERS------------------------------");
            //65,66. 
            SampleIndexeres();

            Console.WriteLine("----------------------MAKING METHOD PARAMETERS OPTIONAL------------------------------");
            //67-70 
            SampleMethodParametersOptional();

            Console.WriteLine("--------------DICTIONARY-----------------");
            //72,73
            SampleDictionary();

            Console.WriteLine("--------------LIST COLLECTION-----------------");
            //74
            SampleList();
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

        //36. DELEGATE
        public static void DelegateMethod(string input)
        {
            Console.WriteLine(input);
        }

        //37,38 WHY DELEGATES
        public static bool Promote(Employee1 employee1)
        {
            return employee1.Experience > 5 ? true : false;
        }

        //39. MULTICAST DELEGATES
        public static void Method1()
        {
            Console.WriteLine("Method 1");
        }

        public static void Method2()
        {
            Console.WriteLine("Method 2");
        }

        public static void Method3()
        {
            Console.WriteLine("Method 3");
        }

        //40,41 EXCEPTION HANDLING & INNER EXCEPTION
        static void ExceptionHandling()
        {
            try
            {
                StreamReader sr = null;
                StreamWriter sw = null;
                try
                {
                    int i = 10;
                    int j = 0;
                    int k = i / j;
                }
                catch (Exception ex)//Divide by Zero exception caught
                {
                    string sPath = @"D:\Home\Ruben\Work\C#\MyText1.txt";
                    if (File.Exists(sPath))//Make File Exception to be caught
                    {
                        sr = new StreamReader(@"D:\Home\Ruben\Work\C#\MyText1.txt");
                        Console.WriteLine(sr.ReadToEnd());
                    }
                    else
                    {
                        throw new FileNotFoundException("File Not Found", ex); // STATEMENT1 : PASS AS INNER EXCEPTION PARAMETER TO THE CONSTRUCTOR
                    }
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Close();
                    }
                    if (sw != null)
                    {
                        sw.Close();
                    }
                    Console.WriteLine("finally executed");
                }
            }
            catch (Exception final_ex)
            {
                //FILE NOT FOUND EXCEPTION
                Console.WriteLine(final_ex.GetType().Name);

                //DIVIDE BY ZERO EXCEPTION : SET AT "STATEMENT1" : ex is the divide by zero exception
                if (final_ex.InnerException != null)
                    Console.WriteLine(final_ex.InnerException.GetType().Name);
            }
        }

        //42. CUSTOM EXCEPTION HANDLING
        static void CustomeExceptionHandling()
        {
            try
            {
                try
                {
                    int i = 10;
                    int j = 0;
                    int k = i / j;
                }
                catch (Exception ex)
                {
                    //code something that throws your custom exception
                    throw new MyCustomExcetpion("This is my own custom exception", ex);
                }
            }
            catch (Exception finalex)
            {
                Console.WriteLine(finalex.GetType());
                Console.WriteLine(finalex.InnerException.GetType());
            }
        }

        //45,46,47. enums,Enums
        static void EnumSample()
        {
            //Read enum Names using Enum Class
            string[] GenderNames = (string[])Enum.GetNames(typeof(Gender));
            Console.WriteLine(GenderNames[0]);

            //Read enum Values using Enum Class
            int[] GenderValues = (int[])Enum.GetValues(typeof(Gender));
            Console.WriteLine(GenderValues[0]);
        }

        //48. ACCESS MODIFIERS - Type members
        static void TypeMembers()
        {
            SampleProtected sampleProtected = new SampleProtected();
            sampleProtected.GetSetProtected();
            Console.WriteLine("Internal value is {0}", sampleProtected.d);

            SampleProtectedInternal sampleProtectedInternal = new SampleProtectedInternal();
            sampleProtectedInternal.GetSetProtectedInternal();
        }

        //51. ACCESS MODIFIERS - Types
        static void Types()
        {
            SampleAMTypes obj = new SampleAMTypes();
            obj.Print();
        }

        #region ATTRIBUTES
        [Obsolete("Use static void newPrint()", false)] //if set as true will compile with errors
        static void oldPrint()
        {
            Console.WriteLine("Print from OLD");
        }

        static void newPrint()
        {
            Console.WriteLine("Print from NEW");
        }
        #endregion

        #region REFLECTION
        static void Reflections()
        {
            //Type T = Type.GetType("NetCoreCsharpConsoleApp.SampleReflectionClass");   //Method 1

            Type T = typeof(SampleReflectionClass);                                     //Method 2

            //SampleReflectionClass obj = new SampleReflectionClass();                  //Method 3
            //Type T = obj.GetType();

            Console.WriteLine(T.FullName);
            Console.WriteLine("------------PROPERTIES-----------------");
            PropertyInfo[] properties = T.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine(property.PropertyType + " " + property.Name);
            }

            Console.WriteLine("-------------METHODS-----------------");
            MethodInfo[] methods = T.GetMethods();
            foreach (MethodInfo method in methods)
            {
                Console.WriteLine(method.Name);
                if (method.Name == "Print")
                {
                    object result = null;
                    object classInstance = Activator.CreateInstance(T, null);
                    result = method.Invoke(classInstance, null);
                }
            }
        }

        #endregion

        #region EarlyVsLateBinding
        static void EarlyVsLateBinding()
        {
            //Early Binding
            BindingCustomer bc = new BindingCustomer();
            string sFullName = bc.PrintCustomer("Xavier", "Fernando");
            Console.WriteLine(sFullName);

            //Late Binding (NOT ADVISABLE considering the complexity & performance)
            Assembly executingassembly = Assembly.GetExecutingAssembly();
            Type customerType = executingassembly.GetType("NetCoreCsharpConsoleApp.BindingCustomer");
            object customerInstance = Activator.CreateInstance(customerType);
            MethodInfo getFullNameMethod = customerType.GetMethod("PrintCustomer");

            string[] parameters = new string[2];
            parameters[0] = "Xavier";
            parameters[1] = "Fernando";
            string sFullName2 = (string)getFullNameMethod.Invoke(customerInstance, parameters);
            Console.WriteLine(sFullName2);
        }
        #endregion

        #region GENERICS
        static void Generics()
        {
            SampleGenerics obj = new SampleGenerics();
            //bool result = obj.AreInputsEqual(1, "A"); //gives compilation error
            bool result = obj.AreInputsEqual("A", "A");

            if (result)
            {
                Console.WriteLine("Equal");
            }
            else
            {
                Console.WriteLine("Not Equal");

            }
        }
        #endregion

        #region ProvideImplementionForToString

        static void ProvideImplementionForToString()
        {
            CustomerToString cts = new CustomerToString();
            cts.FirstName = "Xavier";
            cts.LastName = "Fernando";

            CustomerToString cts2 = new CustomerToString();
            cts2.FirstName = "Calvyn";
            cts2.LastName = "Fernando";

            Console.WriteLine(cts.ToString());
            Console.WriteLine(cts.Equals(cts2));
            Console.WriteLine(cts.GetHashCode());
        }

        #endregion

        #region DifferenceBetweenConvertToStringAndToString
        static void DifferenceBetweenConvertToStringAndToString()
        {
            string s = "TestString";
            s = null;
            //Console.WriteLine(s.ToString()); // WILL THROW ERROR
            Console.WriteLine(Convert.ToString(s));
        }
        #endregion

        #region INDEXERS, OVERLOADING INDEXERS

        static void SampleIndexeres()
        {
            IndexerCompany company = new IndexerCompany();
            Console.WriteLine(company[1]); //before change
            company[1] = "Ruben";
            Console.WriteLine(company[1]); //after change

            IndexerCompany company1 = new IndexerCompany();
            Console.WriteLine(company1[1, "Executive"]);     //calling overloaded indexers

            Console.WriteLine(company1["IT"]);
            company1["IT"] = "Doggies";
            Console.WriteLine(company1["Doggies"]);
        }
        #endregion

        #region SampleMethodParametersOptional

        static void SampleMethodParametersOptional()
        {
            Console.WriteLine(NetCoreCsharpConsoleApp.SampleMethodParametersOptional.MyOptionalMethod1(10, 20, 1, 2, 3, 4, 5));
            Console.WriteLine(NetCoreCsharpConsoleApp.SampleMethodParametersOptional.MyOptionalMethod2(10, 20));
            Console.WriteLine(NetCoreCsharpConsoleApp.SampleMethodParametersOptional.MyOptionalMethod3(10, 20));
            Console.WriteLine(NetCoreCsharpConsoleApp.SampleMethodParametersOptional.MyOptionalMethod4(10, 20));
            Console.WriteLine(NetCoreCsharpConsoleApp.SampleMethodParametersOptional.MyOptionalMethod5(10, c: 50));
        }

        #endregion

        #region DICTIONARY
        static void SampleDictionary()
        {
            //DECLARE
            Dictionary<int, Customr> customrDict = new Dictionary<int, Customr>();

            //INITIALIZE
            customrDict.Add(100, new Customr() { EmpID = 100, Name = "Xavier", Salary = 1000000 });
            customrDict.Add(101, new Customr() { EmpID = 101, Name = "Madhu", Salary = 500000 });
            customrDict.Add(102, new Customr() { EmpID = 102, Name = "Calvyn", Salary = 400000 });

            //READ USING A KEY
            if (customrDict.ContainsKey(100))
            {
                Customr firstcust = customrDict[100];
                Console.WriteLine("Key is {0}", firstcust.EmpID);
            }

            //LOOP THE DICTIONARY USING KEY VALUE PAIR
            foreach (KeyValuePair<int, Customr> custkeyvaluepair in customrDict)
            {
                Console.WriteLine("Key is {0}", custkeyvaluepair.Key);
                Customr eachcustomer = custkeyvaluepair.Value;
                Console.WriteLine("Value is {0}, {1}, {2}", eachcustomer.EmpID, eachcustomer.Name, eachcustomer.Salary);
            }

            //LOOP THE DICTIONARY USING KEY 
            foreach (int custkey in customrDict.Keys)
            {
                Console.WriteLine("Key is {0}", custkey);
            }

            //LOOP THE DICTIONARY USING VALUE
            foreach (Customr cust in customrDict.Values)
            {
                Console.WriteLine("Value is {0}, {1}, {2}", cust.EmpID, cust.Name, cust.Salary);
            }

            //Use TRYGETVALUE & Remove a key
            Customr getcust;
            if (customrDict.TryGetValue(100, out getcust))
            {
                customrDict.Remove(100);
            }

            //GET COUNT & count based on condition of value
            Console.WriteLine("Total Count = {0}", customrDict.Count);
            Console.WriteLine("Salary >400000 count = {0}", customrDict.Count(a => a.Value.Salary > 400000));

            //CLEAR the dictionary
            customrDict.Clear();
            Console.WriteLine("Total Count = {0}", customrDict.Count);

            //Convert ARRAY, LIST into a Dictionary
            Customr[] custarray = new Customr[3];
            custarray[0] = new Customr { EmpID = 1000, Name = "John", Salary = 1000 };
            custarray[1] = new Customr { EmpID = 1001, Name = "Pam", Salary = 2000 };
            custarray[2] = new Customr { EmpID = 1002, Name = "Kim", Salary = 3000 };

            List<Customr> custlist = new List<Customr>();
            custlist.Add(new Customr { EmpID = 1000, Name = "John", Salary = 1000 });
            custlist.Add(new Customr { EmpID = 1001, Name = "Pam", Salary = 2000 });
            custlist.Add(new Customr { EmpID = 1002, Name = "Kim", Salary = 3000 });

            //Dictionary<int, Customr> newdict = custarray.ToDictionary(cust => cust.EmpID, cust => cust);
            Dictionary<int, Customr> newdict = custlist.ToDictionary(cust => cust.EmpID, cust => cust);

            foreach (KeyValuePair<int, Customr> custkeyvaluepair in newdict)
            {
                Console.WriteLine("Key is {0}", custkeyvaluepair.Key);
                Customr eachcustomer = custkeyvaluepair.Value;
                Console.WriteLine("Value is {0}, {1}, {2}", eachcustomer.EmpID, eachcustomer.Name, eachcustomer.Salary);
            }
        }
        #endregion

        #region LIST COLLECTION
        static void SampleList()
        {
            //List index, can include derived classes, Insert, indexof
            List<SampleCustomerList> lstcust = new List<SampleCustomerList>(2);  //capacity is 2 but can overgrow dynamically

            SampleCustomerList customer1 = new SampleCustomerList() { ID = 101, Name = "Xavier", Salary = 1000 };
            SampleCustomerList customer2 = new SampleCustomerList() { ID = 102, Name = "Madhu", Salary = 2000 };
            SampleCustomerList customer3 = new SampleCustomerList() { ID = 103, Name = "Calvyn", Salary = 3000 }; //OVER GROW
            //CustDerived customer4 = new CustDerived() { ID = 104, Name = "Snowy", Salary = 4000, extra_attribute = "YES" }; //DERIVED CLASS

            lstcust.Add(customer1);
            lstcust.Add(customer2);
            lstcust.Add(customer3);

            lstcust.Insert(2, customer1);//INSERT                                                                           

            foreach (SampleCustomerList cust in lstcust)
            {
                Console.WriteLine("{0}, {1}, {2}", cust.ID, cust.Name, cust.Salary);
            }

            //DIFFERENT METHODS USED IN LIST
            Console.WriteLine(lstcust.IndexOf(customer1, 0, 2));

            Console.WriteLine(lstcust.Contains(customer2));

            Console.WriteLine(lstcust.Exists(c => c.Salary > 5000));

            SampleCustomerList result = lstcust.Find(c => c.Salary > 1500);
            Console.WriteLine(result.Name);

            SampleCustomerList result2 = lstcust.FindLast(c => c.Salary > 1500);
            Console.WriteLine(result2.Name);

            List<SampleCustomerList> lstresult = lstcust.FindAll(c => c.Salary > 1500);
            Console.WriteLine(lstresult.Count);

            Console.WriteLine(lstcust.FindIndex(x => x.Salary > 1500));

            Console.WriteLine(lstcust.FindLastIndex(x => x.Salary > 1500));

            //ALSO SUPPORTS:
            /*
            Array to List
            List to Array
            List to Dictionary
            */
        }
        #endregion
    }

    /// <summary>
    /// This is RUBENS CONSTRUCTOR's SAMPLE
    /// </summary>
    /// 
    #region CONSTRUCTOR
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

    #endregion

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

    #region DELEGATES

    public delegate void MyDelegate(string input);

    #endregion

    #region WHY DELEGATES

    //WITHOUT DELEGATES
    public class Employee
    {
        public string Name { get; set; }
        public int Experience { get; set; }

        public void PromoteEmployee(List<Employee> employee)
        {
            foreach (var emp in employee)
            {
                if (emp.Experience > 5)
                {
                    Console.WriteLine("Employee {0} is promoted", emp.Name);
                }
            }
        }
    }

    //WITH DELEGATES
    public delegate bool IsPromotable(Employee1 emp);
    public class Employee1
    {
        public string Name { get; set; }
        public int Experience { get; set; }

        public static void PromoteEmployee(List<Employee1> employee1, IsPromotable isPromotable)
        {
            foreach (var emp1 in employee1)
            {
                if (isPromotable(emp1))
                {
                    Console.WriteLine("Employee {0} is promoted", emp1.Name);
                }
            }
        }
    }

    #endregion

    #region MULTICAST DELEGATES
    public delegate void dMultiCastDelegate();
    public class SampleMultiCastDelegates
    {
        public static void Method1()
        {
            Console.WriteLine("Method 1");
        }

        public static void Method2()
        {
            Console.WriteLine("Method 2");
        }

        public static void Method3()
        {
            Console.WriteLine("Method 3");
        }
    }

    #endregion

    #region CUSTOM EXCEPTION HANDLING

    [Serializable]
    public class MyCustomExcetpion : Exception
    {
        public MyCustomExcetpion() : base()
        {
            Console.WriteLine("Custom exception constructor called");
        }

        public MyCustomExcetpion(string message) : base(message)
        {
        }

        public MyCustomExcetpion(string message, Exception innerexception) : base(message, innerexception)
        {

        }

        public MyCustomExcetpion(SerializationInfo serializeinfo, StreamingContext streamcontext) : base(serializeinfo, streamcontext)
        {

        }
    }

    #endregion

    #region enums,Enums
    public enum Gender
    {
        Male = 5,
        Female = 10,
        TransGender = 15
    }
    #endregion

    #region ACCESS MODIFIERS - Type members
    public class Customer
    {
        private int a = 1;
        public int b = 10;
        protected int c = 100;
        internal int d = 1000;
        //protected internal e;
    }

    //PROTECTED
    public class SampleProtected : Customer
    {
        public void GetSetProtected()
        {
            //Way 1 to access
            SampleProtected obj = new SampleProtected();
            Console.WriteLine(obj.c);//protected
            this.c = 10;
            //Way 2 to access
            Console.WriteLine(this.c);//protected
            //Way 3 to access
            Console.WriteLine(base.c);//protected
        }
    }

    //PROTECTED INTERNAL
    public class SampleProtectedInternal : PAMA.ClassA
    {
        public void GetSetProtectedInternal()
        {
            SampleProtectedInternal obj = new SampleProtectedInternal();
            Console.WriteLine("Protected internal value is {0}", obj.myint);//protected internal
            base.myint = 200;
            Console.WriteLine("Protected internal value now is {0}", this.myint); //different ways of get/set -> obj,base,this
        }
    }
    #endregion

    #region ACCESS MODIFIERS - Types
    public class SampleAMTypes
    {
        public void Print()
        {
            PAMA.ClassB obj = new PAMA.ClassB(); //Class C cannot be accessed because its INTERNAL
            obj.Print();
        }
    }
    #endregion

    #region REFLECTION
    public class SampleReflectionClass
    {
        private int i;
        public string s;

        public int myint { get; set; }
        public string mystring { get; set; }

        public void Print()
        {
            Console.WriteLine("Printed successfully by Reflection");
        }
    }
    #endregion

    #region EarlyVsLateBinding
    public class BindingCustomer
    {
        public string PrintCustomer(string FN, string LN)
        {
            return FN + " " + LN;
        }
    }
    #endregion

    #region GENERICS

    public class SampleGenerics
    {
        public bool AreInputsEqual<T>(T a, T b)
        {
            return a.Equals(b);
        }
    }

    #endregion

    #region ProvideImplementionForToString

    public class CustomerToString
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            //return base.ToString();
            return LastName + " " + FirstName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Customer))
            {
                return false;
            }

            return this.FirstName == ((CustomerToString)obj).FirstName && this.LastName == ((CustomerToString)obj).LastName;
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode() ^ LastName.GetHashCode();
        }
    }

    #endregion

    #region INDEXERS, OVERLOADING INDEXERS

    public class IndexerEmployee
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Dept { get; set; }
    }
    public class IndexerCompany
    {
        private List<IndexerEmployee> lstcustomers;

        public IndexerCompany()
        {
            lstcustomers = new List<IndexerEmployee>();
            lstcustomers.Add(new IndexerEmployee() { EmpId = 1, Name = "Xavier", Dept = "Executive" });
            lstcustomers.Add(new IndexerEmployee() { EmpId = 2, Name = "Madhu", Dept = "HR" });
            lstcustomers.Add(new IndexerEmployee() { EmpId = 3, Name = "Calvyn", Dept = "HR" });
            lstcustomers.Add(new IndexerEmployee() { EmpId = 4, Name = "Snowy", Dept = "IT" });
            lstcustomers.Add(new IndexerEmployee() { EmpId = 5, Name = "Bubble", Dept = "IT" });
            lstcustomers.Add(new IndexerEmployee() { EmpId = 6, Name = "Waffer", Dept = "IT" });
        }

        public string this[int employeeid]
        {
            get
            {
                return lstcustomers.FirstOrDefault(x => x.EmpId == employeeid).Name;
            }
            set
            {
                lstcustomers.FirstOrDefault(x => x.EmpId == employeeid).Name = value;
            }
        }

        //INDEXER OVERLOADING
        public string this[int employeeid, string department]
        {
            get
            {
                return lstcustomers.FirstOrDefault(x => x.EmpId == employeeid && x.Dept == department).Name;
            }
            set
            {
                lstcustomers.FirstOrDefault(x => x.EmpId == employeeid && x.Dept == department).Name = value;
            }
        }

        //GET THE COUNT BEFORE UPDATE & THEN 
        public string this[string department]
        {
            get
            {
                return lstcustomers.Count(x => x.Dept == department).ToString();
            }
            set
            {
                foreach (var item in lstcustomers)
                {
                    if (item.Dept == department)
                    {
                        item.Dept = value;
                    }
                }
            }
        }
    }

    #endregion

    #region SampleMethodParametersOptional

    public class SampleMethodParametersOptional
    {
        public static int MyOptionalMethod1(int a, int b, params object[] restofNumbers)
        {
            int result;
            result = a + b;
            if (restofNumbers != null)
            {
                foreach (object item in restofNumbers)
                {
                    result += (int)item;
                }
            }
            return result;
        }

        public static int MyOptionalMethod2(int a, int b)
        {
            return MyOptionalMethod1(a, b, null);
        }

        public static int MyOptionalMethod3(int a, int b, int[] restofNumbers = null)
        {
            int result;
            result = a + b;
            if (restofNumbers != null)
            {
                foreach (object item in restofNumbers)
                {
                    result += (int)item;
                }
            }
            return result;
        }

        public static int MyOptionalMethod4(int a, int b, [OptionalAttribute] int[] restofNumbers)
        {
            int result;
            result = a + b;
            if (restofNumbers != null)
            {
                foreach (object item in restofNumbers)
                {
                    result += (int)item;
                }
            }
            return result;
        }

        public static int MyOptionalMethod5(int a, int b = 10, int c = 20)
        {
            int result;
            result = a + b + c;
            return result;
        }
    }

    #endregion

    #region DICTIONARY
    public class Customr
    {
        public int EmpID { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
    }
    #endregion

    #region LIST COLLECTION
    public class SampleCustomerList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
    }

    public class CustDerived : SampleCustomerList
    {
        public string extra_attribute { get; set; }
    }
    #endregion
}
