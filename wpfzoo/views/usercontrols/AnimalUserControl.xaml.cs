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
    public partial class AnimalUserControl : UserControlBase
    {

        private Animal animal;

        public Animal Animal
        {
            get { return animal; }
            set {
                animal = value;
                base.OnPropertyChanged("Animal");
            }
        }

        public AnimalUserControl()
        {
            InitializeComponent();
            base.DataContext = this;
        }
    }
}
