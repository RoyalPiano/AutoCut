using Microsoft.Office.Interop.Excel;
using ProjectZavod.classes.DTOs;
using ProjectZavod.Interfaces;
using ProjectZavod.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.classes
{
    public class OrderReader : IParamsReader
    {
        public OrderParams ReadParams(string file)
        {
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(file);
            Worksheet excelSheet = wb.ActiveSheet;
            double height = excelSheet.Cells[11, "E"].Value;
            double width = excelSheet.Cells[16, "C"].Value;
            KeyLock keyType1 = new KeyLock(excelSheet.Cells[30, "G"].Value.ToString());
            KeyLock keyType2 = new KeyLock(excelSheet.Cells[32, "G"].Value.ToString());
            string doorType = excelSheet.Cells[20, "G"].Value.ToString().Replace("/", "");
            int latchSum = int.Parse(excelSheet.Cells[35, "G"].Value.ToString().Substring(0, 1));
            wb.Close();
            var orderParams = new OrderParams(height, width, keyType1, keyType2, doorType, latchSum);
            return orderParams;
        }
    }
}
