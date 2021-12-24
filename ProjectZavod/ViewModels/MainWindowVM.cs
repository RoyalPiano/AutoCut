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
using Ninject;

namespace ProjectZavod.ViewModels
{
    public class MainWindowVM
    {
        private DxfRedactor programm;
        private StandardKernel container;

        public MainWindowVM(StandardKernel container)
        {
            programm = new DxfRedactor(container);
            this.container = container;
            //Service<IPathsService>.RegisterService(new PathsService());
        }

        private ICommand _startProgrammButton;
        public ICommand StartProgrammButton
        {
            get
            {
                return _startProgrammButton ?? (_startProgrammButton = new CommandHandler(() => programm.CreateTransformedTemplates(), () => CanExecuteStart()));
            }
        }

        private ICommand _browseOrdersFolderButton;
        public ICommand BrowseOrdersFolderButton
        {
            get
            {
                return _browseOrdersFolderButton ?? (_browseOrdersFolderButton = new CommandHandler(() =>
                container.Get<RootPaths>().OrdersPath = BrowseFolder(), () => true));
            }
        }

        private ICommand _browseResultsFolderButton;
        public ICommand BrowseResultsFolderButton
        {
            get
            {
                return _browseResultsFolderButton ?? (_browseResultsFolderButton = new CommandHandler(() =>
                container.Get<RootPaths>().ResultsPath = BrowseFolder(), () => true));
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
                return null;
            }
        }

        public bool CanExecuteStart()
        {
            return (container.Get<RootPaths>().OrdersPath != null && container.Get<RootPaths>().ResultsPath != null);
        }
    }
}
