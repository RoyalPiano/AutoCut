using ProjectZavod.Views;
using ProjectZavod.Views.doorTypesViews;
using ProjectZavod.Views.doorViewRedactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ProjectZavod.ViewModels
{
    public class DoorParamsVM : ICloseWindow
    {
        public DoorParamsVM()
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
            Close?.Invoke();
            new DoorParams1().Close();
        }

        private void AddParams()
        {
            new ViewRedactor().ShowDialog();
        }
    }
}
