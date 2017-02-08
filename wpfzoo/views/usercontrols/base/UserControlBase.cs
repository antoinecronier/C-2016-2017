using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace wpfzoo.views.usercontrols
{
    public class UserControlBase : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)

            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
