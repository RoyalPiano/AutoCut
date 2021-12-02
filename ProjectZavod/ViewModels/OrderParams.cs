using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace ProjectZavod.ViewModels
{
    public class OrderParams
    {
        public OrderParams(string file)
        {
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(file);
            Worksheet excelSheet = wb.ActiveSheet;
            Height = excelSheet.Cells[11, "E"].Value.ToString();
            Width = excelSheet.Cells[16, "C"].Value.ToString();
            NameOfKey1 = excelSheet.Cells[30, "G"].Value.ToString();
            NameOfKey2 = excelSheet.Cells[32, "G"].Value.ToString();
            wb.Close();
        }

        public string Height { get; private set; }
        public string Width { get; private set; }
        public string NameOfKey1 { get; private set; }
        public string NameOfKey2 { get; private set; }
    }
}
