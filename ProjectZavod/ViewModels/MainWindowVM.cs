using netDxf;
using netDxf.Entities;
using netDxf.Header;
using netDxf.Objects;
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
    public class MainWindowVM: INotifyPropertyChanged
    {
        public string OrdersPath;
        public string ResultsPath;
        public static string DoorModelsPath = @"..\..\templates\Doors";
        public static string KeyHoleModelsPath = @"..\..\templates\KeyHoles";
        public static string[] DoorModels { get; set; }
        public static string[] KeyHoleModels { get; set; }


        private string doorType;
        public string DoorType
        {
            get { return doorType; }
            set
            {
                var temp = string.Concat(DoorModelsPath, "\\", value);
                if (temp != doorType)
                {
                    doorType = temp;
                    OnPropertyChanged("DoorType");
                }
            }
        }

        private string keyType1;
        public string KeyType1
        {
            get { return keyType1; }
            set
            {
                var temp = string.Concat(KeyHoleModelsPath, "\\", value);
                if (temp != keyType1)
                {
                    keyType1 = temp;
                    OnPropertyChanged("KeyType1");
                }
            }
        }

        private string keyType2;
        public string KeyType2
        {
            get { return keyType2; }
            set
            {
                var temp = string.Concat(KeyHoleModelsPath, "\\", value);
                if (temp != keyType2)
                {
                    keyType2 = temp;
                    OnPropertyChanged("KeyType2");
                }
            }
        }

        public class CommandHandler : ICommand
        {
            private Action _action;
            private Func<bool> _canExecute;
            /// <summary>
            /// Creates instance of the command handler
            /// </summary>
            /// <param name="action">Action to be executed by the command</param>
            /// <param name="canExecute">A bolean property to containing current permissions to execute the command</param>
            public CommandHandler(Action action, Func<bool> canExecute)
            {
                _action = action;
                _canExecute = canExecute;
            }

            /// <summary>
            /// Wires CanExecuteChanged event 
            /// </summary>
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            /// <summary>
            /// Forcess checking if execute is allowed
            /// </summary>
            /// <param name="parameter"></param>
            /// <returns></returns>
            public bool CanExecute(object parameter)
            {
                return _canExecute.Invoke();
            }

            public void Execute(object parameter)
            {
                _action();
            }
        }

        public string[] GetFilesFromDirectory(string directory) 
        {
            return Directory.GetFiles(directory);
        }

        public MainWindowVM()
        {
            DoorModels = GetFoldersFromDirectory(DoorModelsPath).Select(x => x.Split('\\').Last()).ToArray();
            KeyHoleModels = GetFoldersFromDirectory(KeyHoleModelsPath).Select(x => x.Split('\\').Last()).ToArray();
        }

        private ICommand _startProgrammButton;
        public ICommand StartProgrammButton
        {
            get
            {
                return _startProgrammButton ?? (_startProgrammButton = new CommandHandler(() => MakeCut(), () => CanExecute));
            }
        }

        private ICommand _browseOrdersFolderButton;
        public ICommand BrowseOrdersFolderButton
        {
            get
            {
                return _browseOrdersFolderButton ?? (_browseOrdersFolderButton = new CommandHandler(() => BrowseFolder(ref OrdersPath), () => CanExecute));
            }
        }

        private ICommand _browseResultsFolderButton;
        public ICommand BrowseResultsFolderButton
        {
            get
            {
                return _browseResultsFolderButton ?? (_browseResultsFolderButton = new CommandHandler(() => BrowseFolder(ref ResultsPath), () => CanExecute));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _width;
        public string Width
        {
            get { return _width; }
            set
            {
                if (value != _width)
                {
                    if (!new Regex("[^0-9.-]+").IsMatch(value))
                    {
                        _width = value;
                        OnPropertyChanged("Width");
                    }
                    else
                    {
                        InputData_Error();
                    }
                }
            }
        }

        private string _height;
        public string Height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    if(!new Regex("[^0-9.]").IsMatch(value))
                    {
                        _height = value;
                        OnPropertyChanged("Height");
                    }
                    else
                    {
                        InputData_Error();
                    }
                }
            }
        }

        private void InputData_Error()
        {
            MessageBox.Show("вводить только положительные числа или числа с точкой");
        }

        public bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }

        public void MakeCut()
        {
            if (OrdersPath == null || ResultsPath == null)
            {
                throw new Exception("не указан путь к папке");
            }

            var orderFiles = GetFilesFromDirectory(OrdersPath);
            for (int i=0; i< orderFiles.Length; i++)
            {
                string file = string.Format($"{ResultsPath}\\createdFile{i}.dxf");
                DxfDocument ourFile = DxfDocument.Load(orderFiles[i]);
                if (ourFile == null)
                {
                    throw new Exception(orderFiles[i] + " File is not loaded, incorrect format");
                }

                double width = 880;
                double height = 2050;
                ChangeSize(ourFile, width,height);
                ourFile.Save(file);

                DxfVersion dxfVersion = DxfDocument.CheckDxfFileVersion(file, out _);
                if (dxfVersion < DxfVersion.AutoCad2000)
                    throw new Exception("you are using an old AutoCad Version");
            }
        }

        private DxfDocument ChangeSize(DxfDocument ourFile, double width, double height)
        {
            double newWidth = width - 860;
            double newHeigh = height - 2050; // переменная не используется? 
            foreach (var x in ourFile.Lines)
            {
                if (x.Color.G == 255)
                    x.EndPoint = x.EndPoint + new Vector3(newWidth, 0, 0);
                if (x.Color.B == 255)
                {
                    x.StartPoint = x.StartPoint + new Vector3(newWidth, 0, 0);
                    x.EndPoint = x.EndPoint + new Vector3(newWidth, 0, 0);
                }
                if (x.Color.R == 255)
                    x.EndPoint = x.EndPoint - new Vector3(0, newHeigh, 0);
                if (x.Color.R == 254)
                {
                    x.StartPoint = x.StartPoint - new Vector3(0, newHeigh, 0);
                    x.EndPoint = x.EndPoint - new Vector3(0, newHeigh, 0);
                }
            }
            foreach (var x in ourFile.Circles)
            {
                if (x.Color.B == 255)
                    x.Center = x.Center + new Vector3(newWidth, 0, 0);
            }
            return ourFile;
        }

        private static DxfDocument AddLock(DxfDocument ourFile, DxfDocument lockFile, DxfDocument lockFile2)
        {
            var listEntites = new List<EntityObject>();
            if (lockFile != null)
            foreach (Layout layout in lockFile.Layouts)
            {
                List<DxfObject> entities = lockFile.Layouts.GetReferences(layout);
                foreach (DxfObject o in entities)
                {
                    EntityObject entity = o as EntityObject;
                    listEntites.Add(entity);
                }
            }
            if (lockFile2 != null)
            foreach (Layout layout in lockFile2.Layouts)
            {
                List<DxfObject> entities = lockFile2.Layouts.GetReferences(layout);
                Vector3 lenghtToSecondHole = new Vector3(ourFile.Points.ToArray()[0].Position.X, 0, 0) - new Vector3(ourFile.Points.ToArray()[1].Position.X, 0, 0);
                foreach (DxfObject o in entities)
                {
                    EntityObject entity = o as EntityObject;
                    switch ((int)entity.Type)
                    {
                        case 1:
                            var cir = (Circle)entity;
                            cir.Center = cir.Center + lenghtToSecondHole;
                            break;
                        case 10:
                            var line = (Line)entity;
                            line.StartPoint = line.StartPoint + lenghtToSecondHole;
                            line.EndPoint = line.EndPoint + lenghtToSecondHole;
                            break;
                        case 0:
                            var arc = (Arc)entity;
                            arc.Center = arc.Center + lenghtToSecondHole;
                            break;
                    }
                    listEntites.Add(entity);
                }
            }
            foreach (var y in listEntites)
            {
                var x = y.Clone();
                ourFile.AddEntity((EntityObject)x);
            }
            return ourFile;
        }

        public void BrowseFolder(ref string path)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowDialog();
            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                path = folderBrowser.SelectedPath;
            }
        }

        public string[] GetFoldersFromDirectory(string directory)
        {
            return Directory.GetDirectories(directory);
        }
    }
}
