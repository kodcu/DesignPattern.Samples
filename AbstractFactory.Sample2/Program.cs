using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Sample2
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.ReadLine();
        }
    }

    // 1
    public interface IAgentA { }
    public interface IAgentB { }

    public interface IAgentFactory
    {
        IAgentA CreateAgentA();
        IAgentB CreateAgentB();
    }


    //2
    public class AgentA_Xml : IAgentA
    {
        internal AgentA_Xml()
        {

        }
    }

    public class AgentB_Xml : IAgentB
    {
        internal AgentB_Xml()
        {

        }
    }

    public class AgentA_Database : IAgentA
    {
        internal AgentA_Database()
        {

        }
    }

    public class AgentB_Database : IAgentB
    {
        internal AgentB_Database()
        {

        }
    }

    // 3

    public class XMLAgentFactory : IAgentFactory
    {
        public IAgentA CreateAgentA()
        {
            return new AgentA_Xml();
        }

        public IAgentB CreateAgentB()
        {
            return new AgentB_Xml();
        }
    }

    public class DatabaseAgentFactory : IAgentFactory
    {
        public IAgentA CreateAgentA()
        {
            return new AgentA_Database();
        }

        public IAgentB CreateAgentB()
        {
            return new AgentB_Database();
        }
    }
}
