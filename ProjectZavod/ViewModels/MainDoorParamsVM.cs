using Ninject;
using ProjectZavod.Data.orderDBModel;
using ProjectZavod.Views;
using ProjectZavod.Views.doorTypesViews;
using ProjectZavod.Views.doorViewRedactor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace ProjectZavod.ViewModels
{
    public class MainDoorParamsVM : ICloseWindow
    {
        public string Leaf { get; set; }
        public string SteelThickness { get; set; }
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
        public string Soundproofing { get; set; }
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
        public string CustomerContact { get; set; }
        public string CustomerAdress { get; set; }
        public int? Price = 0;

        public bool VibroplastFillingCheck { get; set; }
        public bool CottonFillingCheck { get; set; }
        public bool PenoplexFillingCheck { get; set; }


        private ordersDBEntities db;
        private List<OrderParams> OrderParams;
        public List<string> LeafList { get; set; }
        public List<string> SteelThicknessList { get; set; }
        public List<string> DoorOpeningTypeList { get; set; }
        public List<string> PolymerCoatingList { get; set; }
        public List<string> Lock1List { get; set; }
        public List<string> Сylinder1List { get; set; }
        public List<string> HandleList { get; set; }
        public List<string> HardwareColorList { get; set; }
        public List<ChechBoxItem> SoundproofingList { get; set; }
        public List<string> PeepholeList { get; set; }
        public List<string> InteriorDecorationList { get; set; }
        public List<string> PaymentList { get; set; }
        public List<string> SlopeCornerList { get; set; }
        public List<string> SideList { get; set; }
        public bool IsEditingMode { get; set; }
        public int Id { get; set; }

        public MainDoorParamsVM()
        {
            db = new ordersDBEntities();
            OrderParams = db.OrderParams.ToList().FindAll(w => w.DoorType == "Driveways");
            BindComboboxList(nameof(Leaf));
            BindComboboxList(nameof(SteelThickness));
            BindComboboxList(nameof(DoorOpeningType));
            BindComboboxList(nameof(PolymerCoating));
            BindComboboxList(nameof(Lock1));
            BindComboboxList(nameof(Сylinder1));
            BindComboboxList(nameof(Handle));
            BindComboboxList(nameof(HardwareColor));
            BindCheckBoxList("Soundproofing");
            BindComboboxList(nameof(Peephole));
            BindComboboxList(nameof(InteriorDecoration));
            BindComboboxList(nameof(Payment));
            BindComboboxList(nameof(SlopeCorner));
            BindComboboxList(nameof(Side));
        }

        public class ChechBoxItem
        {
            public bool IsChecked { get; set; }
            public string Value { get; set; }
        }

        private void BindCheckBoxList(string propertyName)
        {
            this.GetType().GetProperty($"{propertyName}List").SetValue(this,
                OrderParams
                .FindAll(w => w.ParamName == propertyName)
                .OrderBy(w => w.Pos)
                .Select(w => new ChechBoxItem() { Value = w.Value })
                .ToList());
        }

        private void BindComboboxList(string propertyName)
        {
            this.GetType().GetProperty($"{propertyName}List").SetValue(this,
                OrderParams
                .FindAll(w => w.ParamName == propertyName)
                .OrderBy(w => w.Pos)
                .Select(w => w.Value)
                .ToList());
        }

        private ICommand _saveOrderBtn;
        public ICommand SaveOrderBtn
        {
            get
            {
                return _saveOrderBtn ?? (_saveOrderBtn = new CommandHandler(() =>
                {
                    if(!SaveOrder())
                        System.Windows.Forms.MessageBox.Show("Ошибка, указаны не все параметры");
                }, 
                () => true));
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

        private ICommand backBtn;
        public ICommand BackBtn
        {
            get
            {
                return backBtn ?? (backBtn = new CommandHandler(() =>
                ReturnBack(), () => true));
            }
        }

        private void ReturnBack()
        {
            new CreationWindow().Show();
            Close?.Invoke();
        }

        public Action Close { get; set; }
        private bool SumPrice(string param)
        {
            if (param == null)
            {
                return false;
            }
            var price = OrderParams.Find(w => w.Value == param).Price;
            Price += price;
            return true;
        }

        private bool SaveOrder()
        {
            foreach (var item in SoundproofingList)
            {
                if (item.IsChecked)
                {
                    Soundproofing += $"{item.Value}; ";
                    if (!SumPrice(item.Value))
                        return false;
                }
            }
            if (!SumPrice(Leaf))
                return false;
            if (!SumPrice(SteelThickness))
                return false;
            if (!SumPrice(DoorOpeningType))
                return false;
            if (!SumPrice(PolymerCoating))
                return false;
            if (!SumPrice(Lock1))
                return false;
            if (!SumPrice(Сylinder1))
                return false;
            if (!SumPrice(Lock2))
                return false;
            if (!SumPrice(Сylinder2))
                return false;
            if (!SumPrice(Handle))
                return false;
            if (!SumPrice(HardwareColor))
                return false;
            if (!SumPrice(Peephole))
                return false;
            if (!SumPrice(InteriorDecoration))
                return false;
            if (!SumPrice(SlopeCorner))
                return false;

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
                Soundproofing = Soundproofing,
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
                Date = DateTime.Now.Date.ToString().Split()[0],
                Price = Price.ToString(),
                CustomerContact = CustomerContact,
                CustomerAdress = CustomerAdress,
            };
            if(!IsEditingMode)
                db.Order.Add(doorParams);
            else
            {
                var or = db.Order.First(w => w.Id == Id);
                db.Order.Remove(or);
                doorParams.Id = Id;
                db.Order.Add(doorParams);
            }
            db.SaveChanges();
            new CreationWindow().Show();
            Close?.Invoke();
            return true;
        }

        private void AddParams()
        {
            new ViewRedactor().ShowDialog();
        }
    }
}
