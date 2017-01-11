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
        public AddressUserControl()
        {
            InitializeComponent();
            Address address = new Address();
            address.City = "Rennes";
            address.PostalCode = "35000";
            address.Street = "Ruduzo";

            this.label.Content = "City";
            this.textBox.Text = address.City;

            this.label2.Content = "PostalCode";
            this.textBox2.Text = address.PostalCode.ToString();

            this.label3.Content = "Street";
            this.textBox3.Text = address.Street.ToString();
        }
    }
}
