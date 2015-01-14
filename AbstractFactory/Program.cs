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
            // doğrudan fabrika sınıflarını kullanmak
            ReportFactory excelReport = new ExcelReportFactory();
            excelReport.Connector();
            excelReport.Formatter();
            excelReport.Writer();

            ReportFactory pdfReport = new PdfReportFactory();
            pdfReport.Connector();
            pdfReport.Formatter();
            pdfReport.Writer();

            // fabrika sınıflarını kullanan genel bir sınıf
            ReportGenerator.Current.Generate(new PdfReportFactory());

            Console.ReadLine();
        }
    }

    // Abstarct Factory
    public abstract class ReportFactory
    {
        public abstract ReportConnector Connector();
        public abstract ReportFormatter Formatter();
        public abstract ReportWriter Writer();
    }

    public class ExcelReportFactory
        : ReportFactory
    {
        public override ReportConnector Connector()
        {
            return new ExcelReportConnector();
        }

        public override ReportFormatter Formatter()
        {
            return new ExcelReportFormatter();
        }

        public override ReportWriter Writer()
        {
            return new ExcelReportWriter();
        }
    }

    public class PdfReportFactory
        : ReportFactory
    {

        public override ReportConnector Connector()
        {
            // nesneler concrete fabrika sınıflarında yaratılır.
            return new PdfReportConnector();
        }

        public override ReportFormatter Formatter()
        {
            return new PdfReportFormatter();
        }

        public override ReportWriter Writer()
        {
            return new PdfReportWriter();
        }
    }

    // fabrika sınıflarını kullanan basit bir istemci kod blogu
    // bu sınıfı singleton tasarladım. 
    public class ReportGenerator
    {
           
        private static ReportGenerator _current = null;
        private static object _lockObj = new object();

        public static ReportGenerator Current
        {
            get
            {
                if (_current == null)
                {
                    lock (_lockObj)
                    {
                        if (_current == null)
                        {
                            _current = new ReportGenerator();
                        }
                    }
                }

                return _current;
            }
        }
        
        private ReportGenerator() { }
        
        // factory kullanımı
        public void Generate(ReportFactory reportFac)
        {
            ReportConnector conn = reportFac.Connector();
            ReportFormatter formatter = reportFac.Formatter();
            ReportWriter writer = reportFac.Writer();
        }
    }


    // another version with delegate
    public delegate ReportConnector CreateReportConnectorDelegate();
    public delegate ReportFormatter CreateReportFormatterDelegate();
    public delegate ReportWriter CreateReportWriterDelegate();

    public class ReportFactory
    {
        public CreateReportConnectorDelegate CreateReportConnector { get; set; }
        public CreateReportFormatterDelegate CreateReportFormatter { get; set; }
        public CreateReportWriterDelegate CreateReportWriter { get; set; }


        public ReportFactory()
        {
            this.CreateReportConnector = new CreateReportConnectorDelegate(delegate { return new ExcelReportConnector(); });
            this.CreateReportFormatter = new CreateReportFormatterDelegate(delegate { return new ExcelReportFormatter(); });
            this.CreateReportWriter = new CreateReportWriterDelegate(delegate { return new ExcelReportWriter(); });

            //******
        }
    }



}
