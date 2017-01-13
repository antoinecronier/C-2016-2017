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
using wpfzoo.database;
using wpfzoo.entities;

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Logique d'interaction pour zooAdmin.xaml
    /// </summary>
    public partial class zooAdmin : Page
    {
        public zooAdmin()
        {
            InitializeComponent();
            
            
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
            MySQLManager<Zoo> zooManager = new MySQLManager<Zoo>();
            Task<Zoo> tZoo = zooManager.Update(ucZoo.Zoo);
            Zoo zoo = (Zoo) tZoo.AsyncState;
        }

        private void btnDelZoo_Click(object sender, RoutedEventArgs e)
        {
            MySQLManager<Zoo> zooManager = new MySQLManager<Zoo>();
            Task<Int32> tRes = zooManager.Delete(ucZoo.Zoo);
            Int32 res = (Int32)tRes.AsyncState;
        }
    }
}
