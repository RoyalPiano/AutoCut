using ProjectZavod.Data.orderDBModel;
using ProjectZavod.Views;
using ProjectZavod.Views.doorTypesViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectZavod.ViewModels
{
    public class CreationVM : ICloseWindow
    {
        public Action Close { get; set; }
        private ordersDBEntities db;
        public CreationVM(DataGrid dataGrid)
        {
            db = new ordersDBEntities();
            var a = db.Order.Select(w => w.Customer).ToList();
            var list = db.Order.Select(w => new
            {
                Номер = w.OrderNumber,
                ДД = w.NumberDD,
                Заказчик = w.Customer
            }).ToList();
            dataGrid.ItemsSource = null;

            dataGrid.ItemsSource = list.ToList();
            dataGrid.UpdateLayout();
            dataGrid.Items.Refresh();
            dataGrid.UpdateLayout();
        }

        private ICommand _createDoorType1;
        public ICommand CreateDoorType1
        {
            get
            {
                return _createDoorType1 ?? (_createDoorType1 = new CommandHandler(() =>
                OpenDoorType1Window(), () => true));
            }
        }

        private void OpenDoorType1Window()
        {
            new MainDoorParams().Show();
            Close?.Invoke();
        }
    }
}
