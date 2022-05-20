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
    public class DoorParamsVM
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

        private void SaveOrder()
        {
            new DoorParams1().Close();
            new CreationWindow().Show();
        }

        private void AddParams()
        {
            new ViewRedactor().Show();
        }
    }
}
