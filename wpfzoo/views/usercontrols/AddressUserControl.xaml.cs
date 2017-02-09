using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpfzoo.entities;

namespace wpfzoo.views.usercontrols
{
    /// <summary>
    /// Logique d'interaction pour AddressUserControl.xaml
    /// </summary>
    public partial class AddressUserControl : UserControlBase
    {
        private Address address;

        public Address Address
        {
            get { return address; }
            set {
                address = value;
                base.OnPropertyChanged("Address");
            }
        }

        public AddressUserControl()
        {
            InitializeComponent();
            base.DataContext = this;
            if (Address != null)
            {
                this.UCStreetNumber.StreetNumber = Address.StreetNumber;
            }
            
        }
    }
}
