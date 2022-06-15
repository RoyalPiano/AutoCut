using Microsoft.Office.Interop.Excel;
using ProjectZavod.classes.DTOs;
using ProjectZavod.Interfaces;
using ProjectZavod.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.classes
{
    public class OrderReader : IParamsReader
    {
        public OrderParamsDTO ReadParams(FileInfo file)
        {
            if (file.Extension != ".xls" && file.Extension != ".xlsx")
                return null;

            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(file.FullName);
            Worksheet excelSheet = wb.ActiveSheet;
            double? height = excelSheet.Cells[11, "E"].Value;
            double? width = excelSheet.Cells[16, "C"].Value;
            var keyType1 = excelSheet.Cells[30, "G"].Value;
            var keyType2 = excelSheet.Cells[32, "G"].Value;
            var doorType = excelSheet.Cells[20, "G"].Value;
            var latchSum = excelSheet.Cells[35, "G"].Value;
            wb.Close();
            if (height == null || width == null || keyType1 == null || keyType2 == null || doorType == null || latchSum == null)
                return null;
            latchSum = int.Parse(latchSum.ToString().Substring(0, 1));
            doorType = doorType.ToString().Replace("/", "");
            KeyLock keyLock1 = new KeyLock(keyType1);
            KeyLock keyLock2 = new KeyLock(keyType2);
            var orderParams = new OrderParamsDTO(height.Value, width.Value, keyLock1, keyLock2, doorType, latchSum);
            return orderParams;
        }
    }
}
