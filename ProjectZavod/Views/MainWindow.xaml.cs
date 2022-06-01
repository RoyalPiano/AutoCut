using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using netDxf;
using netDxf.Entities;
using netDxf.Header;
using Ninject;
using ProjectZavod.classes;
using ProjectZavod.Data.orderDBModel;
using ProjectZavod.Interfaces;
using ProjectZavod.ViewModels;

namespace ProjectZavod
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var container = ConfigureContainer();
            DataContext = container.Get<MainWindowVM>();
        }

        public static StandardKernel ConfigureContainer()
        {
            var container = new StandardKernel();
            container.Bind<RootPaths>().ToSelf().InSingletonScope();
            container.Bind<IParamsReader>().To<OrderReader>().InSingletonScope();
            container.Bind<DxfRedactor>().ToSelf().InSingletonScope();
            return container;
        }
    }
}