using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Logique d'interaction pour StructureAdmin.xaml
    /// </summary>
    public partial class StructureAdmin : Page, INotifyPropertyChanged
    {
        public StructureAdmin()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void listdemployees_Click(object sender, RoutedEventArgs e)
        {
        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            this.oneInfos.Visibility = Visibility.Visible;
        }
    }
}
