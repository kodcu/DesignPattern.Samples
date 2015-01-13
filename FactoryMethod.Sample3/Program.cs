using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Sample3
{
    class Program
    {
        static void Main(string[] args)
        {
            ITextProcessor processor;
            if (ConfigurationManager.AppSettings["division"] == "US")
            {
                processor = new AzureProcessor();
            }
            else
            {
                processor = new FTPSiteProcessor();
            }

            // 2.kullanım

            MainProcessor pro = new MainProcessor();
            pro.Process(new FileProcessorFactory());

            Console.ReadLine();
        }
    }

    // sample client
    public class MainProcessor
    {
        public void Process(ITextProcessorFactory readerFactory)
        {
            ITextProcessor reader = readerFactory.CreateProcessor();
            var text = reader.ReadText();           
        }
    }


    public interface ITextProcessorFactory
    {
        ITextProcessor CreateProcessor();
    }

    class FileProcessorFactory : ITextProcessorFactory
    {
        public ITextProcessor CreateProcessor()
        {
            return new FileProcessor();
        }
    }

    class FTPSiteProcessorFactory : ITextProcessorFactory
    {
        public ITextProcessor CreateProcessor()
        {
            return new FileProcessor();
        }
    }

    class AzureProcessorFactory : ITextProcessorFactory
    {
        public ITextProcessor CreateProcessor()
        {
            return new FileProcessor();
        }
    }



    public interface ITextProcessor
    {
        string ReadText();
        void SaveText(string processedText);
    }

    class FileProcessor : ITextProcessor
    {
        public string ReadText()
        {
            // read in text from a
            // file and return it
            return null;
        }

        public void SaveText(string processedText)
        {
            // write processedText out to file.
        }
    }

    class FTPSiteProcessor : ITextProcessor
    {
        public string ReadText()
        {
            // connect to FTP site
            // download file contents
            // return file contents as a string
            return null;
        }

        public void SaveText(string processedText)
        {
            // create a file of the processedText
            // connect to FTP site
            // upload the file.
        }
    }

    class AzureProcessor : ITextProcessor
    {
        public string ReadText()
        {
            // read in text from a
            // file and return it
            return null;
        }

        public void SaveText(string processedText)
        {
            // write processedText out to file.
        }
    }
}
