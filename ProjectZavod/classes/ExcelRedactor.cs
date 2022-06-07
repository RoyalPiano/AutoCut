using Microsoft.Office.Interop.Excel;
using ProjectZavod.Data.orderDBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZavod.classes
{
    public class ExcelRedactor
    {
        private Order order;
        public ExcelRedactor(Order order)
        {
            this.order = order;
        }
        private void makeItem(int x, string y, Worksheet excelSheet, string rowName, string value)
        {
            excelSheet.Cells[x, y] = rowName;

            var strX = $"{y}{x}";
            var strY = $"F{x}";
            excelSheet.get_Range(strX, strY).Cells.Merge();
            excelSheet.get_Range(strX, strY).Borders.LineStyle = XlLineStyle.xlContinuous;
            excelSheet.get_Range(strX, strY).Font.Size = 13;
            excelSheet.get_Range(strX, strY).Cells.RowHeight = 24;

            excelSheet.Cells[x, "G"] = value;
            var strX2 = $"G{x}";
            var strY2 = $"K{x}";

            excelSheet.get_Range(strX2, strY2).Cells.Merge();
            excelSheet.get_Range(strX2, strY2).Borders.LineStyle = XlLineStyle.xlContinuous;
            excelSheet.get_Range(strX2, strY2).Font.Size = 13;
            excelSheet.get_Range(strX2, strY2).Cells.RowHeight = 24;
        }

        public void Do()
        {
            Application excel = new Application();
            var workbook = excel.Workbooks.Open(@"C:\Users\Oleg\source\repos\autocutNew\ProjectZavod\templates\invoice\Order.xls");
            workbook.Sheets["Заказ"].Delete();
            //Worksheet excelSheet = workbook.ActiveSheet;
            Worksheet excelSheet = workbook.Sheets.Add(Before: workbook.ActiveSheet);
            //var newSheet = workbook.Sheets.Add(After: workbook.ActiveSheet);
            fillDefaultInfo(excelSheet);
            //CreateInvoice(workbook);

            makeItem(19, "A", excelSheet, "Размер двери", $"{order.DoorWidth}x{order.DoorHeight}");
            makeItem(20, "A", excelSheet, "Тип двери \"ЩИТ\"", order.Leaf);
            makeItem(21, "A", excelSheet, "Толщина стали", order.SteelThickness.ToString());
            makeItem(22, "A", excelSheet, "Тип открывания двери", order.DoorOpeningType);
            makeItem(23, "A", excelSheet, "Ширина рабочей створки", order.SashWidth.ToString());
            makeItem(24, "A", excelSheet, "Высота рабочей створки", order.SashHeight.ToString());
            var jamb = new StringBuilder();
            if (order.JambRight)
                jamb.Append("Справа; ");
            if (order.JambLeft)
                jamb.Append("Слева; ");
            if (order.JambUp)
                jamb.Append("Сверху");

            makeItem(25, "A", excelSheet, "Наличник металлический", jamb.ToString());
            makeItem(26, "A", excelSheet, "Полимерно-порошковое покрытие", order.PolymerCoating);
            makeItem(27, "A", excelSheet, "Замок 1", order.Lock1);
            makeItem(28, "A", excelSheet, "Цилиндр 1", order.Сylinder1);
            makeItem(29, "A", excelSheet, "Замок 2", order.Lock2);
            makeItem(30, "A", excelSheet, "Цилиндр 2", order.Сylinder2);
            makeItem(31, "A", excelSheet, "Ручка", order.Handle);
            makeItem(32, "A", excelSheet, "Цвет фурнитуры", order.HardwareColor);
            makeItem(33, "A", excelSheet, "Задвижки", $"{order.LatchesCount} шт.");
            var filling = new StringBuilder();
            if (order.VibroplastFilling)
                filling.Append("Проклеивание \"Вибропласт\" ");
            if (order.CottonFilling)
                filling.Append("Заполнение коробки мин.ватой ");
            if (order.PenoplexFilling)
                filling.Append("Проклеивание \"Пеноплекс\"");
            makeItem(34, "A", excelSheet, "Шумоизоляция", filling.ToString());
            makeItem(35, "A", excelSheet, "Глазок", order.Peephole);
            makeItem(36, "A", excelSheet, "Внутренняя отделка", order.InteriorDecoration);
            makeItem(37, "A", excelSheet, "Цвет внутренней отделки", order.Color);
            makeItem(38, "A", excelSheet, "Оплата", order.Payment);
            var additionalLatch = new StringBuilder();
            if (order.OnWorkingSide)
                additionalLatch.Append("На рабочей створке; ");
            if (order.OnSecondSide)
                additionalLatch.Append("На второй створке");
            makeItem(39, "A", excelSheet, "Доп. петля", additionalLatch.ToString());

            var mufflePart = new StringBuilder();
            if (order.MufflePart)
                mufflePart.Append("Сверху; ");
            mufflePart.Append($"Сбоку: {order.Side}");
            makeItem(40, "A", excelSheet, "Глухая часть", mufflePart.ToString());
            makeItem(41, "A", excelSheet, "Уголок для откосов", order.SlopeCorner);
            makeItem(42, "A", excelSheet, "Примечания", order.Сomments);
            makeItem(43, "A", excelSheet, "Заказчик", order.Customer);
            makeItem(44, "A", excelSheet, "Номер заказа", order.OrderNumber);
            makeItem(45, "A", excelSheet, "Номер ДД", order.NumberDD);
            makeItem(46, "A", excelSheet, "Стоимость", "Не указано");
            makeItem(47, "A", excelSheet, "Скидка 0%", "Не указано");
            makeItem(48, "A", excelSheet, "ИТОГО", "Не указано");

            excel.Visible = true;

        }

        private void fillDefaultInfo(Worksheet excelSheet)
        {
            excelSheet.Name = "Заказ";
            excelSheet.Cells[1, 1] = "Завод профильных конструкций";
            excelSheet.Cells[1, 7] = "Факт. адрес: обл.Свердловская,  г. Каменск-Уральский, ул. Травянская,1\"Б\"";
            excelSheet.Cells[2, 7] = "конт. тел. 8-912-21-00-901";
            excelSheet.Cells[3, 7] = "8(343-9)39-97-97";
            excelSheet.Cells[4, 7] = "эл. адрес: opt89122100901@ya.ru";
            excelSheet.Cells[14, "F"] = $"БЛАНК ОФОРМЛЕНИЯ ЗАКАЗА:     {order.OrderNumber}";
            excelSheet.Cells[49, "A"] = "Размеры указываются по коробке";
            excelSheet.Cells[50, "A"] = "Со способом открывания двери согласен  _________________________";
            excelSheet.Cells[52, "A"] = "Исполнитель_______________________";
            excelSheet.Cells[52, "H"] = "Заказчик_____________________";
            excelSheet.Cells[54, "A"] = "*Все цены в рублях";

            excelSheet.Cells[8, "F"] = $"Заказчик:     {order.Customer}";
            excelSheet.get_Range("F8", "L8").Cells.Merge();
            excelSheet.get_Range("F8", "L8").Borders.LineStyle = XlLineStyle.xlContinuous;
            excelSheet.get_Range("F8", "L8").Font.Size = 13;
            excelSheet.get_Range("F8", "L8").Cells.RowHeight = 24;
            excelSheet.get_Range("F8", "L8").Font.Size = 18;
            excelSheet.get_Range("F8", "L8").Font.Bold = 10;

            excelSheet.get_Range("a1", "e1").Font.Size = 18;
            excelSheet.get_Range("a1", "e1").Font.Bold = 10;

            excelSheet.get_Range("g2", "i2").Font.Size = 18;
            excelSheet.get_Range("g2", "i2").Font.Bold = 10;

            excelSheet.get_Range("g3", "i3").Font.Size = 18;
            excelSheet.get_Range("g3", "i3").Font.Bold = 10;

            excelSheet.get_Range("g4", "i4").Font.Size = 18;

            excelSheet.get_Range("f14", "j14").Font.Size = 18;
            excelSheet.get_Range("f14", "j14").Font.Bold = 10;

            excelSheet.get_Range("f49", "j49").Font.Size = 18;
            excelSheet.get_Range("f49", "j49").Font.Bold = 10;

            excelSheet.get_Range("a50", "g50").Font.Size = 18;
            excelSheet.get_Range("a50", "g50").Font.Bold = 10;

            excelSheet.get_Range("a52", "g52").Font.Size = 18;
            excelSheet.get_Range("a52", "g52").Font.Bold = 10;

            excelSheet.get_Range("h52", "g52").Font.Size = 18;
            excelSheet.get_Range("h52", "g52").Font.Bold = 10;

            excelSheet.get_Range("h54", "k54").Font.Size = 18;
            excelSheet.get_Range("h54", "k54").Font.Bold = 10;
        }

        private void CreateInvoice(Workbook workbook)
        {
            Application excel = new Application();
            var workbook2 = excel.Workbooks.Open(@"C:\Users\Oleg\source\repos\autocutNew\ProjectZavod\templates\invoice\invoice.xlsx");
            //var workbook = excel.Workbooks.Add(Type.Missing);
            //Worksheet excelSheet = workbook2.ActiveSheet;
            Worksheet excelSheet = workbook2.Worksheets["Лист1"];
            excelSheet.Copy(After: workbook.Worksheets["Заказ"]);
            workbook2.Close();

        }
    }
}
