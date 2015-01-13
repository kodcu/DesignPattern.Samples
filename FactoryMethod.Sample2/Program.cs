using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Sample2
{
    class Program
    {
        static void Main(string[] args)
        {
           // normal kullanım
            PDFReader pdfreader = new PDFReader();
            pdfreader.Read();
            pdfreader.Extract();

            // 1. kullanım
            DocumentReaderFactory readerFac = new DocumentReaderFactory();
            PDFReader pdfReader = (PDFReader)readerFac.Get("PDF");
            pdfReader.Read();
            pdfReader.Extract();

            // 2. kullanım
            PDFReaaderFactory pdfFactory = new PDFReaaderFactory();
            PDFReader pdfReader2 = (PDFReader)pdfFactory.CreateReader();
            pdfReader2.Read();
            pdfReader2.Extract();

            //3. kullanım
            DocumentProcessor pro = new DocumentProcessor();
            pro.Process(new PDFReaaderFactory());
            

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
    
    // 1
    public class DocumentReaderFactory
    {
        // burada string tipi enum da kullanabiliriz.
        public IDocumentReader Get(string readerType)
        {
            switch (readerType)
            {
                case "PDF":
                    return new PDFReader();
                case "MsWord":
                    return new MsWordReader();
                default:
                    return new PDFReader(); 
                // örneğin burası için NULL object deseni kullanılarak nul tanımlı bir sınıf yazılabilir.   
                // veya exception fırlatılır throw new Exception("Invalid DocumentReader type");
            }
        }
    }

    // 2
    public interface IDocumentReaderFactory
    {
        IDocumentReader CreateReader();
    }

    public class PDFReaaderFactory
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
