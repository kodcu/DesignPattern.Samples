using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Sample7
{
    class Program
    {
        static void Main(string[] args)
        {
            DbFactory MySqlFactory = new MySqlDbFactory();
            MySqlFactory.Connection();
            MySqlFactory.Command();
            MySqlFactory.Transaction();


            DbFactory oracleFactory = new OracleDbFactory();
            oracleFactory.Connection();
            oracleFactory.Command();
            oracleFactory.Transaction();

            Console.ReadLine();
        }
    }


    /// <summary>
    /// Abstract Product
    /// </summary>
    public abstract class Command { }
    public abstract class Connection { }
    public abstract class Transaction { }

    /// <summary>
    /// Concrete Product for Oracle
    /// </summary>
    class OracleCommand
        : Command
    {
    }

    public class OracleConnection
        : Connection
    {
        private OracleConnection()
        {
        }

        public static OracleConnection Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly OracleConnection instance = new OracleConnection();
        }
    }

    class OracleTransaction
        : Transaction
    {
    }

    /// <summary>
    /// Concrete Product for MySql
    /// </summary>
    class MySqlCommand
        : Command
    {
    }

    public class MySqlConnection
        :Connection
    {
        private MySqlConnection()
        {
        }

        public static MySqlConnection Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly MySqlConnection instance = new MySqlConnection();
        }
    }

    class MySqlTransaction
        : Transaction
    {
    }

    /// <summary>
    /// Abstarct Factory
    /// </summary>
    abstract class DbFactory
    {
        public abstract Connection Connection();
        public abstract Command Command();
        public abstract Transaction Transaction();
    }

    /// <summary>
    /// Concrete Factory for Oracle
    /// </summary>
    class OracleDbFactory
        : DbFactory
    {
        public override Connection Connection()
        {
            return OracleConnection.Instance;
        }

        public override Command Command()
        {
            return new OracleCommand();
        }

        public override Transaction Transaction()
        {
            return new OracleTransaction();
        }
    }

    /// <summary>
    /// Concrete Factory for MySql
    /// </summary>
    class MySqlDbFactory
        : DbFactory
    {
        public override Connection Connection()
        {
            return MySqlConnection.Instance;
        }

        public override Command Command()
        {
            return new MySqlCommand();
        }

        public override Transaction Transaction()
        {
            return new MySqlTransaction();
        }
    }
}