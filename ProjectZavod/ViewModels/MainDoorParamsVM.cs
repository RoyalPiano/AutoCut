using Ninject;
using ProjectZavod.Data.orderDBModel;
using ProjectZavod.Views;
using ProjectZavod.Views.doorTypesViews;
using ProjectZavod.Views.doorViewRedactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectZavod.ViewModels
{
    public class MainDoorParamsVM : ICloseWindow
    {
        public string Leaf { get; set; }
        public double SteelThickness { get; set; }
        public double DoorWidth { get; set; }
        public double DoorHeight { get; set; }
        public string DoorOpeningType { get; set; }
        public double SashWidth { get; set; }
        public double SashHeight { get; set; }
        public bool JambRight { get; set; }
        public bool JambLeft { get; set; }
        public bool JambUp { get; set; }
        public string PolymerCoating { get; set; }
        public string Lock1 { get; set; }
        public string Сylinder1 { get; set; }
        public string Lock2 { get; set; }
        public string Сylinder2 { get; set; }
        public string Handle { get; set; }
        public string HardwareColor { get; set; }
        public int LatchesCount { get; set; }
        public bool VibroplastFilling { get; set; }
        public bool CottonFilling { get; set; }
        public bool PenoplexFilling { get; set; }
        public string Peephole { get; set; }
        public string InteriorDecoration { get; set; }
        public string Color { get; set; }
        public string Payment { get; set; }
        public bool OnWorkingSide { get; set; }
        public bool OnSecondSide { get; set; }
        public bool MufflePart { get; set; }
        public string Side { get; set; }
        public string SlopeCorner { get; set; }
        public string Сomments { get; set; }
        public string Customer { get; set; }
        public string OrderNumber { get; set; }
        public string NumberDD { get; set; }
        private ordersDBEntities db = new ordersDBEntities();
        public MainDoorParamsVM()
        {
        }

        private ICommand _saveOrderBtn;
        public ICommand SaveOrderBtn
        {
            get
            {
                return _saveOrderBtn ?? (_saveOrderBtn = new CommandHandler(() =>
                SaveOrder(), () => true));
            }
        }

        private ICommand _addOrderParam;
        public ICommand AddOrderParam
        {
            get
            {
                return _addOrderParam ?? (_addOrderParam = new CommandHandler(() =>
                AddParams(), () => true));
            }
        }

        public Action Close { get; set; }

        private void SaveOrder()
        {
            var doorParams = new Order()
            {
                Leaf = Leaf,
                SteelThickness = SteelThickness,
                DoorWidth = DoorWidth,
                DoorHeight = DoorHeight,
                DoorOpeningType = DoorOpeningType,
                SashWidth = SashWidth,
                SashHeight = SashHeight,
                JambRight = JambRight,
                JambLeft = JambLeft,
                JambUp = JambUp,
                PolymerCoating = PolymerCoating,
                Lock1 = Lock1,
                Сylinder1 = Сylinder1,
                Lock2 = Lock2,
                Сylinder2 = Сylinder2,
                Handle = Handle,
                HardwareColor = HardwareColor,
                LatchesCount = LatchesCount,
                VibroplastFilling = VibroplastFilling,
                CottonFilling = CottonFilling,
                PenoplexFilling = PenoplexFilling,
                Peephole = Peephole,
                InteriorDecoration = InteriorDecoration,
                Color = Color,
                Payment = Payment,
                OnWorkingSide = OnWorkingSide,
                OnSecondSide = OnSecondSide,
                MufflePart = MufflePart,
                Side = Side,
                SlopeCorner = SlopeCorner,
                Сomments = Сomments,
                Customer = Customer,
                OrderNumber = OrderNumber,
                NumberDD = NumberDD,
            };
            db.Order.Add(doorParams);
            db.SaveChanges();
            new CreationWindow().Show();
            Close?.Invoke();
        }

        private void AddParams()
        {
            new ViewRedactor().ShowDialog();
        }
    }
}
