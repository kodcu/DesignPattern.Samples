using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Sample4
{
    // Abstract Product
    abstract class ReportConnector { }    
    abstract class ReportFormatter { }
    abstract class ReportWriter { }


    // Concrete Product for Excel Report
    public class ExcelReportConnector
        : ReportConnector
    { 

    }

    public class ExcelReportFormatter
        : ReportFormatter
    {

    }

    public class ExcelReportWriter
        : ReportWriter
    {

    }

    // Concrete Product for PDF Report
    public class PdfReportConnector
        : ReportConnector
    {

    }

    public class PdfReportFormatter
        : ReportFormatter
    {

    }

    public class PdfReportWriter
        : ReportWriter
    {

    }
}
