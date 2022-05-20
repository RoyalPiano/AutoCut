using ProjectZavod.Data.orderDBModel;
using ProjectZavod.Views.doorTypesViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectZavod.ViewModels
{
    public class CreationVM
    {
        public DataGrid DataGrid { get; set; }
        ordersDBEntities db = new ordersDBEntities();
        public CreationVM(DataGrid dataGrid)
        {
            DataGrid = dataGrid;
            DataGrid.ItemsSource = db.Order.Select(w => new { w.order_number, w.number_dd, w.client, w.date }).ToList();
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
            new DoorParams1().ShowDialog();
        }
    }
}
