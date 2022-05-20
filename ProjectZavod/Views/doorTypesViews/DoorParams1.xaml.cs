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
    public partial class DoorParams1 : Window
    {
        public DoorParams1()
        {
            InitializeComponent();
            StackPanel sp = FindName("listOfOptions") as StackPanel;
            var cb = new ComboBox();
            cb.ItemsSource = new List<string>() { "sdf", "sdfdf" };
            sp.Children.Insert(0, cb);
            DataContext = new DoorParamsVM();
            Loaded += DoorParams1_Loaded;
        }

        private void DoorParams1_Loaded(object sender, RoutedEventArgs e)
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
