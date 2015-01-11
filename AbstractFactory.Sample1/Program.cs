using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Sample1
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
    abstract class Command { }
    abstract class Connection { }
    abstract class Transaction { }

    /// <summary>
    /// Concrete Product for Oracle
    /// </summary>
    class OracleCommand
        : Command
    {
    }

    class OracleConnection
        : Connection
    {
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

    class MySqlConnection
        : Connection
    {
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
            return new OracleConnection();
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
            return new MySqlConnection();
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
