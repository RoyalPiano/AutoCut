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
    public class ViewRedactorVM : ICloseWindow
    {
        private ComboBox comboBox { get; set; }
        private TextBox textBox { get; set; }

        private ICommand _chooseComboBox;
        public ICommand ChooseComboBox
        {
            get
            {
                return _chooseComboBox ?? (_chooseComboBox = new CommandHandler(() =>
                Open_cb_window(), () => true));
            }
        }

        private ICommand _chooseTextBox;
        public ICommand ChooseTextBox
        {
            get
            {
                return _chooseTextBox ?? (_chooseTextBox = new CommandHandler(() =>
                textBox = new TextBox(), () => true));
            }
        }

        public Action Close { get; set; }

        private void Open_cb_window()
        {
            Close?.Invoke();
            comboBox = new ComboBox();
            new ComboBoxInit().Show();
        }
    }
}
