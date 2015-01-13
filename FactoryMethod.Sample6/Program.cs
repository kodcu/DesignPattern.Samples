using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Sample2
{
    class Program
    {
        static void Main(string[] args)
        {
            // 2. kullanım
            PDFReaderFactory pdfFactory = new PDFReaderFactory();
            PDFReader pdfReader2 = (PDFReader)pdfFactory.CreateReader();
            pdfReader2.Read();
            pdfReader2.Extract();

            //3. kullanım

            // 3.0
            DocumentFactory docFac = new DocumentFactory();
            PDFReader pdfReader3 = (PDFReader)docFac.Get(new PDFReaderFactory());
            pdfReader3.Read();
            pdfReader3.Extract();

            // 3.1
            DocumentProcessor pro = new DocumentProcessor();
            pro.Process(new PDFReaderFactory());


            Console.ReadLine();
        }
    }

    public interface IDocumentReader
    {
        void Read();
        void Extract();
    }

    public class PDFReader
        : IDocumentReader
    {
        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Extract()
        {
            throw new NotImplementedException();
        }
    }

    public class MsWordReader
        : IDocumentReader
    {
        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Extract()
        {
            throw new NotImplementedException();
        }
    }
    
    // 2
    public interface IDocumentReaderFactory
    {
        IDocumentReader CreateReader();
    }

    public class PDFReaderFactory
        : IDocumentReaderFactory
    {

        public IDocumentReader CreateReader()
        {
            return new PDFReader();
        }
    }

    public class MsWordReaderFactory
        : IDocumentReaderFactory
    {
        public IDocumentReader CreateReader()
        {
            return new MsWordReader();
        }
    }

    // 3.0
    public class DocumentFactory
    {
        public IDocumentReader Get(IDocumentReaderFactory factory)
        {
            return factory.CreateReader();
        }
    }

    // 3.1
    public class DocumentProcessor
    {
        public void Process(IDocumentReaderFactory factory)
        {
            IDocumentReader reader = factory.CreateReader();
            reader.Read();
            reader.Extract();
        }
    }

}
