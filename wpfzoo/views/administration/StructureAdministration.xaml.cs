using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.json;

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Logique d'interaction pour StructureAdmin.xaml
    /// </summary>
    public partial class StructureAdmin : Page, INotifyPropertyChanged
    {
        ObservableCollection<Structure> structureList = new ObservableCollection<Structure>();
        MySQLManager<Structure> StructureManager = new MySQLManager<Structure>();
        public StructureAdmin()
        {
            InitializeComponent();
            InitLists();
        }

        private async void InitLists()
        {
            this.UCstructureList.LoadItem((await StructureManager.Get()).ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region clicks
        private void listEmployees_Click(object sender, RoutedEventArgs e)
        {
        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            //this.oneInfos.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void toSchedule_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //StructureManager.Delete()
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
