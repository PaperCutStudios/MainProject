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

            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "/SpeechRulesWeight.xlsx";

            MyApp = new Excel.Application();
            MyApp.Visible = false;
            MyBook = MyApp.Workbooks.Open(filePath);
            Excel._Worksheet workSheet = (Excel.Worksheet)MyApp.ActiveSheet;

            //string filePath = "C:/Users/mallanson/Documents/Visual Studio 2013/Projects/ExcelReaderApp/SpeechRulesWeight.xlsx";
            //var excelApp = new Excel.Application();
            //excelApp.Visible = true;

            //Microsoft.Office.Interop.Excel.Workbooks wb = MyApp.Workbooks.Open(filePath);
            //Microsoft.Office.Interop.Excel.Worksheet sh = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets["Sheet1"];

            //excelApp.Workbooks.Open("SpeechRulesWeight.xlsx");

            var range = workSheet.UsedRange;

            int rowsCnt = range.Rows.Count + 1;

            Console.WriteLine(rowsCnt.ToString());

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.ASCII;

            using (XmlWriter xmlWriter = XmlWriter.Create("Rules.xml", settings))
            {
                xmlWriter.WriteStartElement("Rules");
                for (int i = 2; i < rowsCnt; i++)
                {
                    xmlWriter.WriteStartElement("Rule");
                    xmlWriter.WriteAttributeString("text", workSheet.Cells[i,"B"].Value2.ToString());
                    xmlWriter.WriteAttributeString("id", workSheet.Cells[i, "A"].Value2.ToString());
                    xmlWriter.WriteAttributeString("weight", workSheet.Cells[i, "C"].Value2.ToString());
                    xmlWriter.WriteStartElement("Clashes");
                    string toSplit = workSheet.Cells[i, "F"].Value2.ToString();
                    string[] split = toSplit.Split(new Char [] {','});
                    for (int j = 0; j < split.Length; j++)
                    {
                        xmlWriter.WriteStartElement("Clash");
                        xmlWriter.WriteAttributeString("id", split[j]);
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement(); 
                xmlWriter.Flush();
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

