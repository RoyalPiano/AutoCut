using Ninject;
using ProjectZavod.Data.orderDBModel;
using ProjectZavod.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectZavod.Views.doorTypesViews
{
    /// <summary>
    /// Логика взаимодействия для CreationWindow.xaml
    /// </summary>
    public partial class MainDoorParams : Window
    {
        public MainDoorParams()
        {
            InitializeComponent();
            DataContext = new MainDoorParamsVM();
            Loaded += DoorParams_Loaded;
        }

        private void DoorParams_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is ICloseWindow vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }
    }
}
