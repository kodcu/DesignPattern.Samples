using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Sample4
{
    class Program
    {
        static void Main(string[] args)
        {
            FileFactory factory0 = new XmlFactory();
            factory0.Parser();
            factory0.Validator();

            FileFactory factory1 = new PdfFactory();
            factory1.Parser();
            factory1.Validator();

            Console.ReadLine();
        }
    }


    abstract class Parser { }
    abstract class Validator { }

    public class XmlParser
        : Parser 
    {
        
    }

    public class XmlValidator
        : Validator
    {

    }

    public class PdfParser
        : Parser 
    {
        
    }

    public class PdfValidator
        : Validator 
    {
    
    }


    public abstract class FileFactory
    {
        public abstract Parser Parser();
        public abstract Validator Validator();
    }

    public class XmlFactory
        : FileFactory
    {
        public override Parser Parser()
        {
            throw new NotImplementedException();
        }

        public override Validator Validator()
        {
            throw new NotImplementedException();
        }
    }

    public class PdfFactory
        : FileFactory
    {
        public override Parser Parser()
        {
            throw new NotImplementedException();
        }

        public override Validator Validator()
        {
            throw new NotImplementedException();
        }
    }



}
