using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using ProjectZavod.Services;

namespace ProjectZavod.ViewModels
{
    public class OrderParams
    {
        public OrderParams(string file)
        {
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(file);
            Worksheet excelSheet = wb.ActiveSheet;
            Height = excelSheet.Cells[11, "E"].Value;
            Width = excelSheet.Cells[16, "C"].Value;
            KeyType1 = excelSheet.Cells[30, "G"].Value.ToString();
            KeyType2 = excelSheet.Cells[32, "G"].Value.ToString();
            DoorType = excelSheet.Cells[20, "G"].Value.ToString().Replace("/", "");
            wb.Close();
        }

        public double Height { get; private set; }
        public double Width { get; private set; }
        public string KeyType1 { get; private set; }
        public string KeyType2 { get; private set; }
        public string DoorType { get; private set; }

        //public static string[] DoorModels = paths.DoorModelsPath.GetFoldersFromDirectory().Select(x => x.Split('\\').Last()).ToArray();
        //public static string[] KeyHoleModels = paths.KeyHoleModelsPath.GetFoldersFromDirectory().Select(x => x.Split('\\').Last()).ToArray();

        //private string keyType1;
        //public string KeyType1
        //{
        //    get { return keyType1; }
        //    set
        //    {
        //        var temp = string.Concat(Service<IPathsService>.GetService().LoadPaths().KeyHoleModelsPath, "\\", value);
        //        if (temp != keyType1)
        //        {
        //            keyType1 = temp;
        //            OnPropertyChanged("KeyType1");
        //        }
        //    }
        //}

        //private string keyType2;
        //public string KeyType2
        //{
        //    get { return keyType2; }
        //    set
        //    {
        //        var temp = string.Concat(Service<IPathsService>.GetService().LoadPaths().KeyHoleModelsPath, "\\", value);
        //        if (temp != keyType2)
        //        {
        //            keyType2 = temp;
        //            OnPropertyChanged("KeyType2");
        //        }
        //    }
        //}

        //private string doorType;
        //public string DoorType
        //{
        //    get { return doorType; }
        //    set
        //    {
        //        var temp = string.Concat(Service<IPathsService>.GetService().LoadPaths().DoorModelsPath, "\\", value);
        //        if (temp != doorType)
        //        {
        //            doorType = temp;
        //            OnPropertyChanged("DoorType");
        //        }
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}