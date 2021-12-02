using netDxf;
using netDxf.Entities;
using netDxf.Header;
using netDxf.Objects;
using ProjectZavod.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Input;

namespace ProjectZavod.ViewModels
{
    public class MainWindowVM//: INotifyPropertyChanged
    {
        private Programm programm;
        //public string OrdersPath;
        //public string ResultsPath;
        //public static string[] DoorModels { get; set; }
        //public static string[] KeyHoleModels { get; set; }


        //private string doorType;
        //public string DoorType
        //{
        //    get { return doorType; }
        //    set
        //    {
        //        var temp = string.Concat(paths.DoorModelsPath, "\\", value);
        //        if (temp != doorType)
        //        {
        //            doorType = temp;
        //            OnPropertyChanged("DoorType");
        //        }
        //    }
        //}

        //private string keyType1;
        //public string KeyType1
        //{
        //    get { return keyType1; }
        //    set
        //    {
        //        var temp = string.Concat(KeyHoleModelsPath, "\\", value);
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
        //        var temp = string.Concat(paths.KeyHoleModelsPath, "\\", value);
        //        if (temp != keyType2)
        //        {
        //            keyType2 = temp;
        //            OnPropertyChanged("KeyType2");
        //        }
        //    }
        //}

        public MainWindowVM()
        {
            programm = new Programm();
            Service<IPathsService>.RegisterService(new PathsService());
            //DoorModels = paths.DoorModelsPath.GetFoldersFromDirectory().Select(x => x.Split('\\').Last()).ToArray();
            //KeyHoleModels = paths.KeyHoleModelsPath.GetFoldersFromDirectory().Select(x => x.Split('\\').Last()).ToArray();
        }

        private ICommand _startProgrammButton;
        public ICommand StartProgrammButton
        {
            get
            {
                return _startProgrammButton ?? (_startProgrammButton = new CommandHandler(() => programm.Start(), () => CanExecute));
            }
        }

        private ICommand _browseOrdersFolderButton;
        public ICommand BrowseOrdersFolderButton
        {
            get
            {
                return _browseOrdersFolderButton ?? (_browseOrdersFolderButton = new CommandHandler(() => 
                Service<IPathsService>.GetService().AddOrdersPath(BrowseFolder()), () => CanExecute));
            }
        }

        private ICommand _browseResultsFolderButton;
        public ICommand BrowseResultsFolderButton
        {
            get
            {
                return _browseResultsFolderButton ?? (_browseResultsFolderButton = new CommandHandler(() =>
                Service<IPathsService>.GetService().AddResultsPath(BrowseFolder()), () => CanExecute));
            }
        }

        public static string BrowseFolder()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowDialog();
            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                return folderBrowser.SelectedPath;
            }
            else
            {
                throw new Exception("Incorrect path");
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //private string _width;
        //public string Width
        //{
        //    get { return _width; }
        //    set
        //    {
        //        if (value != _width)
        //        {
        //            if (!new Regex("[^0-9.-]+").IsMatch(value))
        //            {
        //                _width = value;
        //                OnPropertyChanged("Width");
        //            }
        //            else
        //            {
        //                InputData_Error();
        //            }
        //        }
        //    }
        //}

        //private string _height;
        //public string Height
        //{
        //    get { return _height; }
        //    set
        //    {
        //        if (value != _height)
        //        {
        //            if(!new Regex("[^0-9.]").IsMatch(value))
        //            {
        //                _height = value;
        //                OnPropertyChanged("Height");
        //            }
        //            else
        //            {
        //                InputData_Error();
        //            }
        //        }
        //    }
        //}

        //private void InputData_Error()
        //{
        //    MessageBox.Show("вводить только положительные числа или числа с точкой");
        //}

        public bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }

        public OrderParams GetOrderParams(string file)
        {
            return new OrderParams(file);
        }

        //public void BrowseFolder(ref string path)
        //{
        //    FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
        //    folderBrowser.ShowDialog();
        //    if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
        //    {
        //        path = folderBrowser.SelectedPath;
        //    }
        //}
    }
}
