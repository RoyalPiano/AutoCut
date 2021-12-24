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
        //public static OrderParams ReadParams(string file)
        //{
        //    Application excel = new Application();
        //    Workbook wb = excel.Workbooks.Open(file);
        //    Worksheet excelSheet = wb.ActiveSheet;
        //    var dto = new OrderParamsDTO()
        //    {
        //        Height = excelSheet.Cells[11, "E"].Value,
        //        Width = excelSheet.Cells[16, "C"].Value,
        //        KeyType1 = excelSheet.Cells[30, "G"].Value.ToString(),
        //        KeyType2 = excelSheet.Cells[32, "G"].Value.ToString(),
        //        DoorType = excelSheet.Cells[20, "G"].Value.ToString().Replace("/", ""),
        //        LatchSum = int.Parse(excelSheet.Cells[35, "G"].Value.ToString().Substring(0, 1)),
        //    };
        //    wb.Close();
        //    return new OrderParams(dto);
        //}
        public OrderParams ReadParams(string file)
        {
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(file);
            Worksheet excelSheet = wb.ActiveSheet;
            var dto = new OrderParamsDTO()
            {
                Height = excelSheet.Cells[11, "E"].Value,
                Width = excelSheet.Cells[16, "C"].Value,
                KeyType1 = excelSheet.Cells[30, "G"].Value.ToString(),
                KeyType2 = excelSheet.Cells[32, "G"].Value.ToString(),
                DoorType = excelSheet.Cells[20, "G"].Value.ToString().Replace("/", ""),
                LatchSum = int.Parse(excelSheet.Cells[35, "G"].Value.ToString().Substring(0, 1)),
            };
            wb.Close();
            return new OrderParams(dto);
        }
    }
}
