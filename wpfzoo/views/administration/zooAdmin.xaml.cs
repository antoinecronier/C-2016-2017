using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Logique d'interaction pour zooAdmin.xaml
    /// </summary>
    public partial class ZooAdmin : Page
    {
        ObservableCollection<Zoo> zooList = new ObservableCollection<Zoo>();
        public ZooAdmin()
        {
            InitializeComponent();
            this.UCZooList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            InitLists();
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Zoo item = (e.AddedItems[0] as Zoo);

            }
        }

        private async void InitLists()
        {
            MySQLManager<Zoo> zooManager = new MySQLManager<Zoo>();
            this.UCZooList.LoadItem((await zooManager.Get()).ToList());
        }

        private void btnNewZoo_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnStructure_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddZoo_Click(object sender, RoutedEventArgs e)
        {
            //tester si id alors update si non insert...
            MySQLManager<Zoo> zooManager = new MySQLManager<Zoo>();
            if (zooManager.Get(ucZoo.Zoo.Id))
            {
                Task<Zoo> tZoo = zooManager.Update(ucZoo.Zoo);
                Zoo zoo = (Zoo)tZoo.Result;
            }
            else
            {
                Task<Zoo> tZoo = zooManager.Insert(ucZoo.Zoo);
                Zoo zoo = (Zoo)tZoo.Result;
            }
        }

        private void btnDelZoo_Click(object sender, RoutedEventArgs e)
        {
            MySQLManager<Zoo> zooManager = new MySQLManager<Zoo>();
            this.UCZooList.Obs.Remove(ucZoo.Zoo);
            Task<Int32> tRes = zooManager.Delete(ucZoo.Zoo);
            Int32 res = (Int32)tRes.AsyncState;
            
        }
    }
}
