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

namespace ProjectZavod.Views
{
    public partial class CreationWindow : Window
    {
        public CreationWindow()
        {
            InitializeComponent();
            DataContext = new CreationVM(this.gridOrders, this);
            Loaded += CreationWindow_Loaded;
        }

        private void CreationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindow vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }
    }
}
