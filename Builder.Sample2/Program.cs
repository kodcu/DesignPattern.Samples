using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Sample2
{
    // Client invoking class
    public class Client
    {
        public static void Main()
        {
            // Create a PDF report
            ReportBuilder pdfBuilder = new PDFBuilder();
            ReportDirector dir = new ReportDirector();
            Report pdfReport = dir.GenerateReport(pdfBuilder);

            // Print content
            Console.WriteLine(pdfReport.Header);
            Console.WriteLine(pdfReport.Content);
            Console.WriteLine(pdfReport.Footer);

            // Create a Excel report
            ReportBuilder excelBuilder = new ExcelBuilder();
            Report excelReport = dir.GenerateReport(excelBuilder);

            // Print content
            Console.WriteLine(excelReport.Header);
            Console.WriteLine(excelReport.Content);
            Console.WriteLine(excelReport.Footer);

            Console.ReadLine();
        }
    }

    // Report or Product
    public class Report
    {
        public string ReportType;
        public string Header;
        public string Footer;
        public string Content;
    }

    // Report Builder - Builder is responsible for defining
    // the construction process for individual parts. Builder
    // has those individual processes to initialize and
    // configure the report.
    public abstract class ReportBuilder
    {
        public Report report;
        public void CreateReport()
        {
            report = new Report();
        }
        public abstract void SetReportType();
        public abstract void SetHeader();
        public abstract void SetFooter();
        public abstract void SetContent();
        public Report DispatchReport()
        {
            return report;
        }
    }

    // PDF Report class
    public class PDFBuilder : ReportBuilder
    {
        public override void SetReportType()
        {
            report.ReportType = "PDF";
        }
        public override void SetHeader()
        {
            report.Header = "PDF Header";
        }
        public override void SetFooter()
        {
            report.Footer = "PDF Footer";
        }
        public override void SetContent()
        {
            report.Content = "PDF Content";
        }
    }

    // Excel Report class
    public class ExcelBuilder : ReportBuilder
    {
        public override void SetReportType()
        {
            report.ReportType = "Excel";
        }
        public override void SetHeader()
        {
            report.Header = "Excel Header";
        }
        public override void SetFooter()
        {
            report.Footer = "Excel Footer";
        }
        public override void SetContent()
        {
            report.Content = "Excel Content";
        }
    }

    ///
    /// Director takes those individual processes from the builder
    /// and defines the sequence to build the report.
    ///
    public class ReportDirector
    {
        public Report GenerateReport(ReportBuilder reportBuilder)
        {
            reportBuilder.CreateReport();
            reportBuilder.SetReportType();
            reportBuilder.SetHeader();
            reportBuilder.SetContent();
            reportBuilder.SetFooter();
            return reportBuilder.DispatchReport();
        }
    }
}
