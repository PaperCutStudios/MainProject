using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Xml;

namespace RulesReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel.Workbook MyBook = null; 
            Excel.Application MyApp = null;
            Excel.Worksheet MySheet = null;

            string XmlEncoding = "<?xml version=\"1.0\" encoding=\"US-ASCII\"?>\n";

            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "/SpeechRulesWeight.xlsx"; 

            MyApp = new Excel.Application();
            MyApp.Visible = false;
            MyBook = MyApp.Workbooks.Open(filePath);
            MySheet = (Excel.Worksheet)MyBook.Sheets[1];

            //string filePath = "C:/Users/mallanson/Documents/Visual Studio 2013/Projects/ExcelReaderApp/SpeechRulesWeight.xlsx";
            //var excelApp = new Excel.Application();
            //excelApp.Visible = true;

            //Microsoft.Office.Interop.Excel.Workbooks wb = MyApp.Workbooks.Open(filePath);
            //Microsoft.Office.Interop.Excel.Worksheet sh = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets["Sheet1"];

            //excelApp.Workbooks.Open("SpeechRulesWeight.xlsx");
            Excel._Worksheet workSheet = (Excel.Worksheet)MyApp.ActiveSheet;

            var range = MySheet.UsedRange;

            int rowsCnt = range.Rows.Count;

            Console.WriteLine(rowsCnt.ToString());


            using (XmlWriter xmlWriter = XmlWriter.Create("Rules.xml"))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;

                xmlWriter.WriteRaw(XmlEncoding);

                
                //Console.WriteLine();

                xmlWriter.WriteStartElement("Rules");
                xmlWriter.WriteStartElement("Rule");

                xmlWriter.WriteStartElement("Title");

                xmlWriter.WriteElementString("price", "19.95");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.Flush();
                //for (int i = 0; i < workSheet.UsedRange.Rows.Count; i++)
                //{
                //    xmlwriter.write
                //}
            }

            //Console.WriteLine(sh.Cells[2, "B"].Value.ToString);
            //Console.WriteLine(sh.Cells[2, "B"]);
            //string myString = ((Excel.Range)workSheet.Cells[2, "B"]).Value2.ToString();
            //Console.WriteLine(myString);
            MyApp.Quit();
            Console.ReadLine();
           
        }
    }
    
}
