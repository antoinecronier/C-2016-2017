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
    /// Logique d'interaction pour AnimalUserControl.xaml
    /// </summary>
    
    public partial class AnimalUserControl : UserControl
    {
        private Animal animal;

        public Animal Animal
        {
            get { return animal; }
            set {
                animal = value;
                this.OnPropertyChanged("Animal");
                  }
        }

        public object Obs { get; internal set; }

        public AnimalUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
