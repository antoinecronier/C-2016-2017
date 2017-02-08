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
using wpfzoo.entities.enums;

namespace wpfzoo.views.usercontrols
{
    /// <summary>
    /// Logique d'interaction pour StreetNumberUserControl.xaml
    /// </summary>
    public partial class StreetNumberUserControl : UserControlBase
    {
        private StreetNumber streetNumber;

        public StreetNumber StreetNumber
        {
            get { return streetNumber; }
            set {
                streetNumber = value;
                base.OnPropertyChanged("StreetNumber");
            }
        }

        public StreetNumberUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
           
    }
}

