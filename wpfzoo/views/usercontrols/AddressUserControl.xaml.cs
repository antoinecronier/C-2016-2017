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
    public partial class AddressUserControl : UserControl
    {
        private Address address;

        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        public AddressUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            this.ucStreetNumber.StreetNumber = Address.StreetNumber;
        }
    }
}
