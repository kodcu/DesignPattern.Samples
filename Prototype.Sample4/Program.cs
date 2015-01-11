using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Sample4
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeManager employee = new EmployeeManager();

            employee["John"] = new Employee("John", "Smith");
            employee["Bill"] = new Employee("Bill", "Jones");

            //now let's clone 'John Smith'
            Employee employee1 = employee["John"].Clone(true) as Employee;


            Console.ReadLine();
        }
    }

    public static class DeepCloneExtension
    {
        /// <summary>
        /// method to perform a deep clone of an arbitrary (unknown at the time of cloning) object
        /// </summary>
        /// <typeparam name="T">type of object being cloned</typeparam>
        /// <param name="obj">object instance being cloned</param>
        /// <returns></returns>
        public static T DoDeepClone<T>(this T obj)
        {
            //make sure the object being passed is serializable, otherwise throw an
            //exception
            if (!obj.GetType().IsSerializable)
                throw new ArgumentException("The object provided is not serializable. Please add the [Serializable()] attribute to your object", "obj");

            // check for a null object, if found the return the defaults
            // for the object
            if (obj == null)
                return default(T);
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, obj);
                    ms.Seek(0, SeekOrigin.Begin);
                    return (T)bf.Deserialize(ms);
                }
            }
        }
    }

    public abstract class EmployeePrototype
    {
        public abstract EmployeePrototype Clone(bool deepClone);
    }

    public class EmployeeManager
    {
        public Dictionary<string, EmployeePrototype> _employees = new Dictionary<string, EmployeePrototype>();

        public EmployeePrototype this[string idx]
        {
            get { return _employees[idx]; }
            set { _employees.Add(idx, value); }
        }
    }

    [Serializable()]
    public class Employee
        : EmployeePrototype
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public Employee(string fName, string lName)
        {
            this._firstName = fName;
            this._lastName = lName;
        }

        public override EmployeePrototype Clone(bool deepClone)
        {
            switch (deepClone)
            {
                case true:
                    return this.DoDeepClone() as EmployeePrototype;
                case false:
                    return this.MemberwiseClone() as EmployeePrototype;
                default:
                    return this.MemberwiseClone() as EmployeePrototype;
            }
        }
    }
}
