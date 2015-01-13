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
            IDocumentReader pdfReader = (PDFReader)readerFac.Get("PDF");
            pdfReader.Read();
            pdfReader.Extract();

            // 1.1 generic version

            IDocumentReader pdfReader1 = (PDFReader)DocumentFactory<PDFReader>.Get();
            IDocumentReader wordReader1 = (MsWordReader)DocumentFactory<MsWordReader>.Get(); 

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

    // 1.1 generic version
    class DocumentFactory<T>
        where T : IDocumentReader, new()
    {
        private static IDocumentReader opr = null;

        public static IDocumentReader Get()
        {
            return opr = new T();
        }
    }    

}
