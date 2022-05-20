using ProjectZavod.Views.doorTypesViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectZavod.ViewModels
{
    public class ComboBoxInitVM : ICloseWindow
    {
        public string InputValues { get; set; }
        public string NewParamName { get; set; }

        private ICommand _saveParam;
        public ICommand SaveParam
        {
            get
            {
                return _saveParam ?? (_saveParam = new CommandHandler(() =>
                CreateParam(), () => true));
            }
        }

        public Action Close { get; set; }

        private void CreateParam()
        {
            Close?.Invoke();
            new DoorParams1().UpdateLayout();
        }
    }
}
