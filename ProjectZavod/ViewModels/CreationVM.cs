using ProjectZavod.classes;
using ProjectZavod.classes.DTOs;
using ProjectZavod.Data.orderDBModel;
using ProjectZavod.Views;
using ProjectZavod.Views.doorTypesViews;
using System;
using System.Collections.Generic;
using System.Data;
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
        private DataGrid dataGrid;
        private Window window { get; set; }
        public CreationVM(DataGrid dataGrid, CreationWindow window)
        {
            this.window = window;
            this.dataGrid = dataGrid;
            db = new ordersDBEntities();
            var date = DateTime.Now.Date.ToString().Split()[0];
            var list = db.Order.Select(w => new OrderDTO
            {
                Номер = w.OrderNumber,
                ДД = w.NumberDD,
                Заказчик = w.Customer,
                Дата = date
            }).ToList();
            dataGrid.ItemsSource = list;
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
        private ICommand getExcelOrder;
        public ICommand GetExcelOrder
        {
            get
            {
                return getExcelOrder ?? (getExcelOrder = new CommandHandler(() =>
                CreateExcelOrder(), () => true));
            }
        }

        private void CreateExcelOrder()
        {
            var order = db.Order.First(w => w.OrderNumber == SelectedOrder.Номер);
            var excelRedactor = new ExcelRedactor(order);
            excelRedactor.Do();
        }

        private ICommand deleteOrder;
        public ICommand DeleteOrder
        {
            get
            {
                return deleteOrder ?? (deleteOrder = new CommandHandler(() =>
                Delete(), () => true));
            }
        }

        private void Delete()
        {
            var order = db.Order.First(w => w.OrderNumber == SelectedOrder.Номер);
            db.Order.Remove(order);
            db.SaveChanges();
            var date = DateTime.Now.Date.ToString().Split()[0];
            var list = db.Order.Select(w => new OrderDTO
            {
                Номер = w.OrderNumber,
                ДД = w.NumberDD,
                Заказчик = w.Customer,
                Дата = date
            }).ToList();
            dataGrid.ItemsSource = list;
        }

        public OrderDTO SelectedOrder { get; set; }

        private void OpenDoorType1Window()
        {
            new MainDoorParams().Show();
            Close?.Invoke();
        }
    }
}
