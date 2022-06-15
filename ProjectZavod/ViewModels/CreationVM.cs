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
            var list = db.Order.Select(w => new OrderDTO
            {
                Номер = w.OrderNumber,
                ДД = w.NumberDD,
                Заказчик = w.Customer,
                Дата = w.Date
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
            if (SelectedOrder == null)
                return;
            var order = db.Order.First(w => w.OrderNumber == SelectedOrder.Номер);
            var excelRedactor = new ExcelRedactor(order);
            excelRedactor.Do();
        }

        private ICommand changeOrder;
        public ICommand ChangeOrder
        {
            get
            {
                return changeOrder ?? (changeOrder = new CommandHandler(() =>
                ChangeOrderMethod(), () => true));
            }
        }

        private void ChangeOrderMethod()
        {
            if (SelectedOrder == null)
                return;
            var selectedOrder = db.Order.First(w => w.OrderNumber == SelectedOrder.Номер);
            var newWindow = new MainDoorParams()
            {
                DataContext = new MainDoorParamsVM()
                {
                    Leaf = selectedOrder.Leaf,
                    SteelThickness = selectedOrder.SteelThickness,
                    DoorWidth = (double)selectedOrder.DoorWidth,
                    DoorHeight = (double)selectedOrder.DoorHeight,
                    DoorOpeningType = selectedOrder.DoorOpeningType,
                    SashWidth = (double)selectedOrder.SashWidth,
                    SashHeight = (double)selectedOrder.SashHeight,
                    JambRight = selectedOrder.JambRight,
                    JambLeft = selectedOrder.JambLeft,
                    JambUp = selectedOrder.JambUp,
                    PolymerCoating = selectedOrder.PolymerCoating,
                    Lock1 = selectedOrder.Lock1,
                    Сylinder1 = selectedOrder.Сylinder1,
                    Lock2 = selectedOrder.Lock2,
                    Сylinder2 = selectedOrder.Сylinder2,
                    Handle = selectedOrder.Handle,
                    HardwareColor = selectedOrder.HardwareColor,
                    LatchesCount = (int)selectedOrder.LatchesCount,
                    Soundproofing = selectedOrder.Soundproofing,
                    Peephole = selectedOrder.Peephole,
                    InteriorDecoration = selectedOrder.InteriorDecoration,
                    Color = selectedOrder.Color,
                    Payment = selectedOrder.Payment,
                    OnWorkingSide = selectedOrder.OnWorkingSide,
                    OnSecondSide = selectedOrder.OnSecondSide,
                    MufflePart = selectedOrder.MufflePart,
                    Side = selectedOrder.Side,
                    SlopeCorner = selectedOrder.SlopeCorner,
                    Сomments = selectedOrder.Сomments,
                    Customer = selectedOrder.Customer,
                    OrderNumber = selectedOrder.OrderNumber,
                    NumberDD = selectedOrder.NumberDD,
                    CustomerAdress = selectedOrder.CustomerAdress,
                    CustomerContact = selectedOrder.CustomerContact,
                    IsEditingMode = true,
                    Id = selectedOrder.Id,
                }
            };
            newWindow.Show();
            Close?.Invoke();
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
            var list = db.Order.Select(w => new OrderDTO
            {
                Номер = w.OrderNumber,
                ДД = w.NumberDD,
                Заказчик = w.Customer,
                Дата = w.Date
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
